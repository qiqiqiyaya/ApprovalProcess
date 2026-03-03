/**
 * FlowLayoutEngine Service
 *
 * Custom layout engine that enforces strict 50px vertical spacing between consecutive
 * node levels, regardless of individual node heights. Replaces DagreLayout to comply
 * with Project Constitution Principle 3.
 *
 * @module FlowLayoutEngine
 * @version 1.0.0
 * @since 2026-03-04
 */

import { Injectable } from '@angular/core';
import { FlowGraph } from '../models/flow-graph';
import {
  ILayoutConfig,
  ILayoutResult,
  INodePosition,
  INodeLevel,
  ILayoutEngine,
  ILayoutCache,
  LayoutErrorCode,
  LayoutError,
  DEFAULT_LAYOUT_CONFIG,
  createNodePosition,
  createNodeLevel,
  createLayoutResult,
} from '../models/layout.models';

/**
 * Internal cache implementation for layout results.
 *
 * @private
 */
class LayoutCache implements ILayoutCache {
  private cache = new Map<string, ILayoutResult>();
  private readonly maxSize: number;

  constructor(maxSize: number = 100) {
    this.maxSize = maxSize;
  }

  get(key: string): ILayoutResult | undefined {
    return this.cache.get(key);
  }

  set(key: string, result: ILayoutResult): void {
    // Simple FIFO eviction when cache is full
    if (this.cache.size >= this.maxSize) {
      const firstKey = this.cache.keys().next().value;
      if(firstKey){
        this.cache.delete(firstKey);
      }
    }
    this.cache.set(key, result);
  }

  has(key: string): boolean {
    return this.cache.has(key);
  }

  clear(): void {
    this.cache.clear();
  }

  generateKey(graph: FlowGraph): string {
    // Sort node IDs and edge pairs to ensure consistent keys for identical graphs
    const nodeIds = graph.nodes
      .map((n) => n.id)
      .sort()
      .join(',');
    const edgePairs = graph.edges
      .map((e) => `${e.source}-${e.target}`)
      .sort()
      .join('|');
    return `${nodeIds}|${edgePairs}`;
  }
}

/**
 * Custom layout engine service enforcing strict 50px vertical spacing.
 *
 * @remarks
 * This service replaces the DagreLayout implementation which violated the
 * project constitution's strict spacing requirement. Uses BFS for level
 * assignment and fixed vertical spacing formula: y = levelIndex * 50 + baseOffset.
 *
 * @public
 * @injectable
 */
@Injectable({
  providedIn: 'root',
})
export class FlowLayoutEngine implements ILayoutEngine {
  /**
   * Internal cache for layout results.
   *
   * @private
   */
  private readonly cache = new LayoutCache();

  /**
   * Calculates layout for the given graph with strict 50px vertical spacing.
   *
   * @remarks
   * This is the main entry point for layout calculation. It validates the graph,
   * checks the cache, assigns levels, calculates positions, and returns the result.
   *
   * Algorithm:
   * 1. Validate graph structure (not null, has nodes)
   * 2. Generate cache key and check for cached result
   * 3. Assign levels using BFS (O(V + E))
   * 4. Calculate positions with fixed 50px spacing
   * 5. Store result in cache
   *
   * @param graph - The flow graph to layout. Must be a valid Directed Acyclic Graph (DAG).
   * @param config - Optional configuration. Defaults to DEFAULT_LAYOUT_CONFIG.
   * @returns Layout result with node positions
   * @throws {LayoutError} If graph is invalid or contains cycles
   *
   * @public
   */
  layout(graph: FlowGraph, config?: ILayoutConfig): ILayoutResult {
    // Validate graph
    this.validateGraph(graph);

    // Use default config if not provided
    const layoutConfig = config ?? DEFAULT_LAYOUT_CONFIG;

    // Generate cache key
    const cacheKey = this.cache.generateKey(graph);

    // Check cache
    const cachedResult = this.cache.get(cacheKey);
    if (cachedResult) {
      return cachedResult;
    }

    // Assign levels using BFS
    const levels = this.assignLevels(graph);

    // Calculate positions
    const result = this.calculatePositions(graph, levels, layoutConfig);

    // Cache the result
    this.cache.set(cacheKey, result);

    return result;
  }

  /**
   * Assigns levels to nodes using Breadth-First Search (BFS).
   *
   * @remarks
   * Implements a topological level assignment algorithm:
   * - Root nodes (no incoming edges) are at level 0
   * - Each child node is at level = parentLevel + 1
   * - Uses BFS to ensure breadth-first traversal
   * - Detects cycles and throws LayoutError if found
   *
   * Time Complexity: O(V + E) where V = number of nodes, E = number of edges
   * Space Complexity: O(V) for the visited set and level map
   *
   * @param graph - The flow graph
   * @returns Map of node ID to level index (0-based)
   * @throws {LayoutError} If graph is invalid or contains cycles
   *
   * @public
   */
  assignLevels(graph: FlowGraph): Map<string, number> {
    // Initialize level map
    const levels = new Map<string, number>();

    // Find root nodes (nodes with no incoming edges)
    const incomingEdges = new Map<string, number>();
    graph.nodes.forEach((node) => {
      incomingEdges.set(node.id, 0);
    });
    graph.edges.forEach((edge) => {
      incomingEdges.set(edge.target, (incomingEdges.get(edge.target) || 0) + 1);
    });

    const rootNodes = graph.nodes.filter((node) => (incomingEdges.get(node.id) || 0) === 0);

    if (rootNodes.length === 0) {
      throw new LayoutError(
        'Graph contains cycles. No root nodes found (all nodes have incoming edges).',
        LayoutErrorCode.CYCLE_DETECTED,
      );
    }

    // BFS traversal
    const queue: Array<{ nodeId: string; level: number }> = [];
    const visited = new Set<string>();

    // Initialize queue with root nodes at level 0
    rootNodes.forEach((node) => {
      queue.push({ nodeId: node.id, level: 0 });
      levels.set(node.id, 0);
    });

    let processedCount = 0;

    while (queue.length > 0) {
      const { nodeId, level } = queue.shift()!;

      if (visited.has(nodeId)) {
        continue; // Skip already processed nodes
      }
      visited.add(nodeId);
      processedCount++;

      // Find children (outgoing edges)
      const children = graph.edges.filter((edge) => edge.source === nodeId);

      for (const child of children) {
        const childLevel = level + 1;

        // If child already has a level, verify it's consistent
        if (levels.has(child.target)) {
          const existingLevel = levels.get(child.target)!;
          if (existingLevel < childLevel) {
            // This shouldn't happen with BFS, but indicates a potential issue
            throw new LayoutError(
              `Inconsistent level assignment for node ${child.target}: ${existingLevel} vs ${childLevel}`,
              LayoutErrorCode.LEVEL_ASSIGNMENT_FAILED,
            );
          }
        } else {
          levels.set(child.target, childLevel);
        }

        queue.push({ nodeId: child.target, level: childLevel });
      }
    }

    // Verify all nodes were processed
    if (processedCount !== graph.nodes.length) {
      throw new LayoutError(
        `Failed to assign levels to all nodes. Processed: ${processedCount}, Total: ${graph.nodes.length}`,
        LayoutErrorCode.LEVEL_ASSIGNMENT_FAILED,
      );
    }

    return levels;
  }

  /**
   * Calculates node positions based on level assignments and configuration.
   *
   * @remarks
   * Implements strict 50px vertical spacing formula:
   * - levelY = levelIndex * 50 + baseYOffset
   * - layoutX = horizontal position based on greedy algorithm
   * - centering offset: x = layoutX - width/2, y = layoutY - height/2
   *
   * Horizontal Layout:
   * - Nodes are grouped by level
   * - Each level is laid out horizontally with horizontalSpacing between nodes
   * - Greedy algorithm places nodes left to right
   *
   * @param graph - The flow graph with node dimensions
   * @param levels - Map of node ID to level index
   * @param config - Layout configuration
   * @returns Layout result with node positions
   * @throws {LayoutError} If position calculation fails
   *
   * @public
   */
  calculatePositions(
    graph: FlowGraph,
    levels: Map<string, number>,
    config: ILayoutConfig,
  ): ILayoutResult {
    // Validate node dimensions
    this.validateNodeDimensions(graph);

    // Group nodes by level
    const nodesByLevel = new Map<number, string[]>();
    levels.forEach((level, nodeId) => {
      if (!nodesByLevel.has(level)) {
        nodesByLevel.set(level, []);
      }
      nodesByLevel.get(level)!.push(nodeId);
    });

    // Sort levels by index
    const sortedLevels = Array.from(nodesByLevel.keys()).sort((a, b) => a - b);

    // Calculate base Y offset
    const baseYOffset = config.baseYOffset ?? 0;
    const horizontalSpacing = config.horizontalSpacing ?? DEFAULT_LAYOUT_CONFIG.horizontalSpacing;

    // Create node positions
    const nodePositions = new Map<string, INodePosition>();
    const levelObjects: INodeLevel[] = [];
    let maxWidth = 0;

    for (const levelIndex of sortedLevels) {
      const nodeIds = nodesByLevel.get(levelIndex)!;

      // Calculate base Y for this level (strict 50px spacing)
      const baseY = levelIndex * config.verticalSpacing + baseYOffset;

      // Calculate horizontal positions for nodes in this level
      let currentX = 0;
      let levelWidth = 0;

      for (const nodeId of nodeIds) {
        const node = graph.nodes.find((n) => n.id === nodeId);
        if (!node) {
          throw new LayoutError(
            `Node ${nodeId} not found in graph`,
            LayoutErrorCode.POSITION_CALCULATION_FAILED,
          );
        }

        const width = node.width ?? DEFAULT_LAYOUT_CONFIG.horizontalSpacing;
        const height = node.height ?? DEFAULT_LAYOUT_CONFIG.verticalSpacing;

        // Calculate layout position (center point)
        const layoutX = currentX + width! / 2;
        const layoutY = baseY;

        // Create node position with centering offset
        const position = createNodePosition(nodeId, layoutX, layoutY, width!, height, levelIndex);

        nodePositions.set(nodeId, position);

        // Move to next node position
        currentX += width! + horizontalSpacing!;
      }

      // Calculate level width (subtract last spacing)
      levelWidth = currentX - horizontalSpacing!;
      maxWidth = Math.max(maxWidth, levelWidth);

      // Create level object
      const levelObject = createNodeLevel(levelIndex, nodeIds, baseYOffset);
      levelObjects.push(levelObject);
    }

    // Center graph horizontally if requested
    if (config.centerGraph) {
      const offsetX = maxWidth / 2;
      const centeredPositions = new Map<string, INodePosition>();

      nodePositions.forEach((position, nodeId) => {
        const centeredPosition = createNodePosition(
          nodeId,
          position.layoutX + offsetX,
          position.layoutY,
          position.width,
          position.height,
          position.level,
        );
        centeredPositions.set(nodeId, centeredPosition);
      });

      // Create result with centered positions
      return createLayoutResult(centeredPositions, levelObjects, config);
    }

    // Create result
    return createLayoutResult(nodePositions, levelObjects, config);
  }

  /**
   * Validates graph structure.
   *
   * @param graph - The graph to validate
   * @throws {LayoutError} If graph is invalid
   *
   * @private
   */
  private validateGraph(graph: FlowGraph): void {
    if (!graph) {
      throw new LayoutError(
        'Graph must be a valid FlowGraph instance',
        LayoutErrorCode.INVALID_GRAPH,
      );
    }

    if (!graph.nodes || graph.nodes.length === 0) {
      throw new LayoutError('Graph must have at least one node', LayoutErrorCode.INVALID_GRAPH);
    }

    if (!graph.edges) {
      throw new LayoutError('Graph must have a valid edges array', LayoutErrorCode.INVALID_GRAPH);
    }
  }

  /**
   * Validates node dimensions.
   *
   * @param graph - The graph with nodes to validate
   * @throws {LayoutError} If any node has invalid dimensions
   *
   * @private
   */
  private validateNodeDimensions(graph: FlowGraph): void {
    for (const node of graph.nodes) {
      const width = node.width;
      const height = node.height;

      if (width === undefined || width === null || isNaN(width) || width <= 0) {
        throw new LayoutError(
          `Node ${node.id} has invalid width: ${width}`,
          LayoutErrorCode.INVALID_NODE_SIZE,
        );
      }

      if (height === undefined || height === null || isNaN(height) || height <= 0) {
        throw new LayoutError(
          `Node ${node.id} has invalid height: ${height}`,
          LayoutErrorCode.INVALID_NODE_SIZE,
        );
      }
    }
  }

  /**
   * Clears the layout cache.
   *
   * @public
   */
  public clearCache(): void {
    this.cache.clear();
  }

  /**
   * Gets cache statistics.
   *
   * @returns Object containing cache size and keys
   *
   * @public
   */
  public getCacheStats(): { size: number; keys: string[] } {
    return {
      size: (this.cache as any).cache.size,
      keys: Array.from((this.cache as any).cache.keys()),
    };
  }
}

