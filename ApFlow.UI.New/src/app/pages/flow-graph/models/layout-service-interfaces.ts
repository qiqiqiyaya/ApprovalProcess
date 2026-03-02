import { Observable } from 'rxjs';
import { LayoutConfig, LayoutResult } from './layout-config';
import { FlowNode} from './flow-node';
import { FlowEdge } from './flow-edge';

/**
 * Layout service interface
 * Provides layout calculation and application capabilities
 */
export interface ILayoutService {
  /**
   * Calculates layout for the current graph state
   * @param config Layout configuration
   * @returns Observable of layout result
   */
  calculateLayout$(config: LayoutConfig): Observable<LayoutResult>;

  /**
   * Applies layout to the graph
   * @param result Layout result to apply
   * @param animate Whether to animate the transition
   */
  applyLayout(result: LayoutResult, animate?: boolean): void;

  /**
   * Triggers layout recalculation
   * Uses current configuration and graph state
   */
  triggerLayout(): void;

  /**
   * Current layout configuration
   */
  readonly config$: Observable<LayoutConfig>;

  /**
   * Current layout result
   */
  readonly layoutResult$: Observable<LayoutResult>;
}

/**
 * Layout configuration service interface
 * Manages layout configuration with persistence
 */
export interface ILayoutConfigService {
  /**
   * Observable of configuration changes
   */
  readonly config$: Observable<LayoutConfig>;

  /**
   * Current configuration snapshot
   */
  readonly config: LayoutConfig;

  /**
   * Updates configuration with partial values
   * @param partialConfig Partial configuration to update
   */
  updateConfig(partialConfig: Partial<LayoutConfig>): void;

  /**
   * Resets configuration to defaults
   */
  resetToDefaults(): void;

  /**
   * Gets current configuration snapshot
   * @returns Copy of current configuration
   */
  getConfigSnapshot(): LayoutConfig;

  /**
   * Applies a configuration preset
   * @param preset Preset to apply
   */
  applyPreset(preset: import('./layout-config').LayoutPreset): void;

  /**
   * Gets all available presets
   * @returns Array of presets
   */
  getPresets(): import('./layout-config').LayoutPreset[];

  /**
   * Gets current preset (if any)
   * @returns Current preset or null
   */
  getCurrentPreset(): import('./layout-config').LayoutPreset | null;
}

/**
 * Vertical spacing calculator interface
 * Calculates uniform vertical spacing for hierarchical graphs
 */
export interface IVerticalSpacingCalculator {
  /**
   * Calculates levels for nodes in a graph
   * @param nodes Array of nodes
   * @param edges Array of edges
   * @returns Map of node ID to level
   */
  calculateLevels(nodes: FlowNode[], edges: FlowEdge[]): Map<string, number>;

  /**
   * Groups nodes by level
   * @param nodes Array of nodes
   * @param levels Map of node ID to level
   * @returns Map of level to array of nodes
   */
  groupNodesByLevel(
    nodes: FlowNode[],
    levels: Map<string, number>
  ): Map<number, FlowNode[]>;

  /**
   * Calculates node positions with uniform vertical spacing
   * @param levelNodes Map of level to nodes
   * @param config Layout configuration
   * @returns Array of node positions
   */
  calculatePositions(
    levelNodes: Map<number, FlowNode[]>,
    config: LayoutConfig
  ): import('./layout-config').NodePosition[];

  /**
   * Handles parallel branches to ensure uniform spacing
   * @param levelNodes Map of level to nodes
   * @param branchGroups Array of branch groups
   * @returns Updated level nodes
   */
  handleParallelBranches(
    levelNodes: Map<number, FlowNode[]>,
    branchGroups: import('./flow-group').BranchGroup[]
  ): Map<number, FlowNode[]>;
}
