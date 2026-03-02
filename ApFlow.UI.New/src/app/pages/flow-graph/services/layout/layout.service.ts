import { Injectable, Optional } from '@angular/core';
import { Observable, Subject, BehaviorSubject, debounceTime, switchMap, distinctUntilChanged, filter, catchError, tap } from 'rxjs';
import { Graph } from '@antv/x6';
import { LayoutConfig, LayoutResult } from '../../models/layout-config';
import { ILayoutService } from '../../models/layout-service-interfaces';
import { UniformSpacingLayout } from './uniform-spacing-layout';
import { BranchGroup } from '../../models/flow-group';
import { LayoutConfigService } from './layout-config.service';
import { FlowNode } from '../../models/flow-node';
import { EditorService } from '../editor.service';

/**
 * Main layout service for flow graph
 * Manages layout calculation and application with reactive updates
 *
 * T079: Added comprehensive error handling and logging
 * Provided in FlowGraphModule to ensure EditorService dependency is available
 */
@Injectable()
export class LayoutService implements ILayoutService {
  /**
   * Trigger for layout recalculation
   */
  private layoutTrigger$ = new Subject<void>();

  /**
   * Subject for layout results
   */
  private layoutResultSubject = new BehaviorSubject<LayoutResult | null>(null);

  /**
   * T066: Layout configuration service (optional for backward compatibility)
   */
  private layoutConfigService?: LayoutConfigService;

  /**
   * Editor service for accessing X6 graph instance and FlowGraph
   * Optional to support different initialization patterns
   */
  private editorService?: EditorService;

  /**
   * Observable of layout configuration
   * Uses LayoutConfigService if available, otherwise falls back to internal subject
   */
  readonly config$: Observable<LayoutConfig>;

  /**
   * Observable of layout results
   */
  readonly layoutResult$ = this.layoutResultSubject.asObservable().pipe(
    // Filter out null values to match Observable<LayoutResult>
    // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
    // (if you use strictNullChecks, this is safe because null is only the initial value)
    // You may want to use filter(Boolean) if LayoutResult is always truthy
    // but for clarity, use explicit filter
    // import filter from 'rxjs'
    // import { filter } from 'rxjs';
    // (add import if not present)
    filter((result): result is LayoutResult => result !== null)
  );

  /**
   * Layout engine instance
   */
  private layoutEngine: UniformSpacingLayout;

  /**
   * Current layout result
   */
  private currentResult: LayoutResult | null = null;

  /**
   * Layout cache for performance optimization
   */
  private layoutCache = new Map<string, LayoutResult>();

  constructor(
    @Optional() layoutConfigService?: LayoutConfigService,
    @Optional() editorService?: EditorService
  ) {
    this.layoutEngine = new UniformSpacingLayout();
    this.layoutConfigService = layoutConfigService;
    this.editorService = editorService;

    // T066: Use LayoutConfigService if available, otherwise use internal subject
    const internalConfig$ = new BehaviorSubject<LayoutConfig>({
      verticalSpacing: 50,
      horizontalSpacing: 100,
      animate: true,
      animationDuration: 300,
    });
    this.config$ = layoutConfigService ? layoutConfigService.config$ : internalConfig$.asObservable();

    this.setupLayoutTrigger();
  }

  /**
   * Gets the X6 graph instance from EditorService
   * @returns X6 Graph instance
   * @throws Error if graph is not available
   */
  private getGraph(): Graph {
    if (!this.editorService) {
      throw new Error('EditorService is not available. LayoutService requires EditorService to access the graph.');
    }
    const graph = this.editorService.getGraph();
    if (!graph) {
      throw new Error('Graph is not initialized. Please call editorService.setGraph() first.');
    }
    return graph;
  }

  /**
   * Gets the branch group manager from EditorService
   * @returns BranchGroupManager instance
   * @returns undefined if not available
   */
  private getBranchGroupManager() {
    if (!this.editorService) {
      console.warn('[LayoutService] EditorService is not available, branch groups will not be used');
      return undefined;
    }

    try {
      const manager = this.editorService.getBranchGroupManager();
      if (!manager) {
        console.warn('[LayoutService] BranchGroupManager is not initialized');
      }
      return manager;
    } catch (error) {
      console.warn('[LayoutService] Failed to get BranchGroupManager:', error);
      return undefined;
    }
  }

  /**
   * Calculates layout for the current graph state
   * @param config Layout configuration
   * @returns Observable of layout result
   *
   * T079: Added error handling and logging
   */
  calculateLayout$(config: LayoutConfig): Observable<LayoutResult> {
    return new Observable((subscriber) => {
      try {
        console.log('[LayoutService] Starting layout calculation...', { config });

        // Get graph instance
        const graph = this.getGraph();

        // Extract nodes from X6 graph
        const x6Nodes = graph.getNodes();
        const nodes = x6Nodes.map((node) => {
          const flowNode = new FlowNode();
          flowNode.id = node.id;
          flowNode.x = node.position().x;
          flowNode.y = node.position().y;
          flowNode.width = node.size().width;
          flowNode.height = node.size().height;
          return flowNode;
        });

        console.log(`[LayoutService] Extracted ${nodes.length} nodes`);

        // Extract edges from X6 graph
        const x6Edges = graph.getEdges();
        const edges = x6Edges.map((edge) => ({
          id: edge.id,
          source: edge.getSourceCellId()!,
          target: edge.getTargetCellId()!,
        }));

        console.log(`[LayoutService] Extracted ${edges.length} edges`);

        // T052: Get branch groups if manager is available
        const branchManager = this.getBranchGroupManager();
        const branchGroups = branchManager
          ? branchManager.getAllGroups()
          : [];

        console.log(`[LayoutService] Found ${branchGroups.length} branch groups`);

        // Generate cache key (includes branch groups)
        const cacheKey = this.generateCacheKey(nodes, edges, config, branchGroups);

        // Check cache (T053)
        if (this.layoutCache.has(cacheKey)) {
          console.log('[LayoutService] Using cached layout result');
          subscriber.next(this.layoutCache.get(cacheKey)!);
          subscriber.complete();
          return;
        }

        // Calculate layout with branch groups
        const startTime = performance.now();
        const result = this.layoutEngine.layout(nodes, edges, config, branchGroups);
        const calculationTime = performance.now() - startTime;

        console.log(`[LayoutService] Layout calculated in ${calculationTime.toFixed(2)}ms`, {
          metadata: result.metadata,
        });

        // Cache result
        this.layoutCache.set(cacheKey, result);

        subscriber.next(result);
        subscriber.complete();
      } catch (error) {
        console.error('[LayoutService] Layout calculation failed:', error);
        subscriber.error(error);
      }
    });
  }

  /**
   * Applies layout to the graph
   * @param result Layout result to apply
   * @param animate Whether to animate the transition
   *
   * T079: Added error handling and logging
   */
  applyLayout(result: LayoutResult, animate?: boolean): void {
    try {
      console.log('[LayoutService] Applying layout...', {
        nodeCount: result.nodes.length,
        animate,
      });

      const graph = this.getGraph();
      const config = this.getConfigSnapshot();
      const shouldAnimate = animate ?? config.animate ?? true;
      const duration = config.animationDuration ?? 300;

      result.nodes.forEach((position) => {
        const cell = graph.getCellById(position.id);
        if (cell && cell.isNode()) {
          if (shouldAnimate) {
            // Use animate method for smooth transitions
            const node = cell as any;
            node.animate('position', { x: position.x, y: position.y }, {
              duration,
              easing: 'ease',
            });
          } else {
            cell.position(position.x, position.y);
          }
        } else {
          console.warn(`[LayoutService] Cell not found for node: ${position.id}`);
        }
      });

      // Update current result
      this.currentResult = result;
      this.layoutResultSubject.next(result);

      console.log('[LayoutService] Layout applied successfully');
    } catch (error) {
      console.error('[LayoutService] Failed to apply layout:', error);
      throw error;
    }
  }

  /**
   * Triggers layout recalculation
   * Uses current configuration and graph state
   */
  triggerLayout(): void {
    this.layoutTrigger$.next();
  }

  /**
   * Updates layout configuration
   * @param config New layout configuration
   * @deprecated Use LayoutConfigService.updateConfig() instead for proper validation and persistence
   */
  updateConfig(config: LayoutConfig): void {
    // T066: Delegate to LayoutConfigService if available
    if (this.layoutConfigService) {
      this.layoutConfigService.updateConfig(config);
    }
    // Note: LayoutConfigService will trigger layout automatically via its reactive pipeline
  }

  /**
   * Gets current layout configuration snapshot
   * @returns Current configuration
   */
  getConfigSnapshot(): LayoutConfig {
    // T066: Use LayoutConfigService if available
    if (this.layoutConfigService) {
      return this.layoutConfigService.getConfigSnapshot();
    }

    // Fallback: get latest value from config$ observable
    let config!: LayoutConfig;
    this.config$.subscribe(c => config = c).unsubscribe();
    return config;
  }

  /**
   * Gets current layout result
   * @returns Current result or null
   */
  getCurrentResult(): LayoutResult | null {
    return this.currentResult;
  }

  /**
   * Clears layout cache
   */
  clearCache(): void {
    this.layoutCache.clear();
  }

  /**
   * Sets up reactive layout trigger with debouncing
   * T066: Also triggers layout when configuration changes
   * T079: Added error handling and logging
   */
  private setupLayoutTrigger(): void {
    // T066: Subscribe to configuration changes and trigger layout automatically
    this.config$.pipe(
      debounceTime(16), // Small debounce to avoid rapid changes
      distinctUntilChanged(),
      tap(config => console.log('[LayoutService] Configuration changed:', config))
    ).subscribe(() => {
      this.layoutTrigger$.next();
    });

    // Original trigger pipeline with error handling (T079)
    this.layoutTrigger$
      .pipe(
        debounceTime(16), // ~60fps
        // T054: Add distinctUntilChanged to avoid duplicate layout calculations
        distinctUntilChanged(),
        tap(() => console.log('[LayoutService] Layout triggered')),
        switchMap(() => this.calculateLayout$(this.getConfigSnapshot())),
        catchError(error => {
          console.error('[LayoutService] Layout pipeline error:', error);
          throw error; // Re-throw to maintain error propagation
        })
      )
      .subscribe((result) => {
        this.applyLayout(result);
      });
  }

  /**
   * Generates cache key for layout result
   * @param nodes Array of nodes
   * @param edges Array of edges
   * @param config Layout configuration
   * @param branchGroups Optional branch groups
   * @returns Cache key string
   */
  private generateCacheKey(
    nodes: { id: string }[],
    edges: { source: string; target: string }[],
    config: LayoutConfig,
    branchGroups?: BranchGroup[]
  ): string {
    const nodeIds = nodes.map((n) => n.id).sort().join(',');
    const edgeIds = edges
      .map((e) => `${e.source}-${e.target}`)
      .sort()
      .join(',');
    const configStr = JSON.stringify({
      verticalSpacing: config.verticalSpacing,
      horizontalSpacing: config.horizontalSpacing,
    });

    // Include branch groups in cache key (T053)
    const branchGroupIds = branchGroups
      ? branchGroups.map(g => g.id).sort().join(',')
      : '';

    return `${nodeIds}|${edgeIds}|${configStr}|${branchGroupIds}`;
  }
}
