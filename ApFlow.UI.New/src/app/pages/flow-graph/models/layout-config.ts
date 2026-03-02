/**
 * Layout configuration interface
 * Defines spacing and animation parameters for flow graph layout
 */
export interface LayoutConfig {
  /**
   * Vertical spacing between adjacent nodes in pixels
   * Minimum: 20, Maximum: 200, Default: 50
   * This spacing is constant regardless of node height
   */
  verticalSpacing: number;

  /**
   * Horizontal spacing between nodes at the same level in pixels
   * Minimum: 50, Maximum: 500, Default: 100
   */
  horizontalSpacing: number;

  /**
   * Minimum node width for layout calculation
   * Optional, used for initial positioning
   */
  minNodeWidth?: number;

  /**
   * Minimum node height for layout calculation
   * Optional, used for initial positioning
   */
  minNodeHeight?: number;

  /**
   * Whether to animate layout transitions
   * Default: true
   */
  animate?: boolean;

  /**
   * Animation duration in milliseconds
   * Only used if animate is true
   * Default: 300
   */
  animationDuration?: number;
}

/**
 * Layout calculation result
 * Contains calculated node positions and metadata
 */
export interface LayoutResult {
  /**
   * Array of calculated node positions
   * Each entry maps a node ID to its (x, y) coordinates
   */
  nodes: NodePosition[];

  /**
   * Optional edge routing information
   * Used for custom edge routing algorithms
   */
  edges?: EdgeRoute[];

  /**
   * Layout metadata and statistics
   */
  metadata: LayoutMetadata;
}

/**
 * Node position in the layout
 */
export interface NodePosition {
  /**
   * Node identifier
   * Must match an existing node in the graph
   */
  id: string;

  /**
   * X coordinate in pixels
   * Represents the left edge of the node
   */
  x: number;

  /**
   * Y coordinate in pixels
   * Represents the top edge of the node
   * All nodes at the same level should have the same Y value
   */
  y: number;

  /**
   * Calculated level in the hierarchy
   * Used for debugging and validation
   * Optional field for debugging purposes
   */
  level?: number;
}

/**
 * Edge routing information
 * Defines how an edge should be drawn between two nodes
 */
export interface EdgeRoute {
  /**
   * Edge identifier
   * Must match an existing edge in the graph
   */
  id: string;

  /**
   * Source node ID
   */
  source: string;

  /**
   * Target node ID
   */
  target: string;

  /**
   * Array of intermediate points for custom routing
   * If not provided, AntV X6 uses default routing
   * Points are in order from source to target
   */
  points?: Point[];
}

/**
 * 2D point in the layout
 */
export interface Point {
  /**
   * X coordinate in pixels
   */
  x: number;

  /**
   * Y coordinate in pixels
   */
  y: number;
}

/**
 * Layout calculation metadata
 * Provides statistics and metrics about the layout
 */
export interface LayoutMetadata {
  /**
   * Total number of levels in the hierarchy
   */
  totalLevels: number;

  /**
   * Maximum number of nodes in any single level
   */
  maxNodesPerLevel: number;

  /**
   * Layout calculation time in milliseconds
   * Used for performance monitoring
   */
  calculationTime: number;

  /**
   * Number of parallel branch groups processed
   */
  branchGroupCount: number;

  /**
   * Total number of nodes in the graph
   */
  nodeCount: number;

  /**
   * Total number of edges in the graph
   */
  edgeCount: number;
}

/**
 * Strict layout configuration with validation
 * Ensures configuration values are valid at runtime
 */
export class StrictLayoutConfig implements LayoutConfig {
  readonly verticalSpacing: number;
  readonly horizontalSpacing: number;
  readonly minNodeWidth?: number;
  readonly minNodeHeight?: number;
  readonly animate?: boolean;
  readonly animationDuration?: number;

  /**
   * Creates a new StrictLayoutConfig with validation
   * @throws Error if configuration is invalid
   */
  constructor(config: LayoutConfig) {
    this.validate(config);
    this.verticalSpacing = config.verticalSpacing;
    this.horizontalSpacing = config.horizontalSpacing;
    this.minNodeWidth = config.minNodeWidth;
    this.minNodeHeight = config.minNodeHeight;
    this.animate = config.animate;
    this.animationDuration = config.animationDuration;
  }

  /**
   * Validates the configuration
   * @throws Error with descriptive message if invalid
   */
  private validate(config: LayoutConfig): void {
    if (config.verticalSpacing < 20 || config.verticalSpacing > 200) {
      throw new Error(
        `verticalSpacing must be between 20 and 200, got ${config.verticalSpacing}`
      );
    }
    if (config.horizontalSpacing < 50 || config.horizontalSpacing > 500) {
      throw new Error(
        `horizontalSpacing must be between 50 and 500, got ${config.horizontalSpacing}`
      );
    }
    if (config.animationDuration !== undefined && config.animationDuration < 0) {
      throw new Error(
        `animationDuration must be non-negative, got ${config.animationDuration}`
      );
    }
  }
}

/**
 * Layout configuration preset
 * Provides predefined configurations for common use cases
 */
export interface LayoutPreset {
  /**
   * Human-readable name of the preset
   */
  name: string;

  /**
   * Description of when to use this preset
   */
  description: string;

  /**
   * The configuration values for this preset
   */
  config: LayoutConfig;
}

/**
 * Predefined layout configuration presets
 */
export const LAYOUT_PRESETS: LayoutPreset[] = [
  {
    name: 'Compact',
    description: 'Dense layout for complex flows with many nodes',
    config: {
      verticalSpacing: 30,
      horizontalSpacing: 80,
      animate: true,
      animationDuration: 200,
    },
  },
  {
    name: 'Standard',
    description: 'Balanced layout for most use cases (default)',
    config: {
      verticalSpacing: 50,
      horizontalSpacing: 100,
      animate: true,
      animationDuration: 300,
    },
  },
  {
    name: 'Spacious',
    description: 'Wide layout for presentations and demonstrations',
    config: {
      verticalSpacing: 80,
      horizontalSpacing: 150,
      animate: true,
      animationDuration: 400,
    },
  },
];
