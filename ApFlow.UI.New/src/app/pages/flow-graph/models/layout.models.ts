/**
 * Layout Engine Type Definitions
 *
 * This file contains all TypeScript interfaces, types, and factory functions
 * for the custom layout engine that enforces strict 50px vertical spacing.
 *
 * @module layout.models
 * @version 1.0.0
 * @since 2026-03-04
 */

import { FlowGraph } from './flow-graph';

// ============================================================================
// Type Definitions
// ============================================================================

/**
 * Configuration object for the layout engine with fixed vertical spacing.
 *
 * @remarks
 * The vertical spacing is fixed at 50px per the project constitution (Principle 3).
 * This value is enforced via a literal type to prevent accidental modification.
 *
 * @public
 */
export interface ILayoutConfig {
  /** Fixed vertical spacing between consecutive levels (50px per constitution) */
  readonly verticalSpacing: 50;
  /** Horizontal spacing between nodes in the same level */
  readonly horizontalSpacing?: number;
  /** Base Y offset for the entire graph (allows padding) */
  readonly baseYOffset?: number;
  /** Whether to center the graph horizontally in the viewport */
  readonly centerGraph?: boolean;
}

/**
 * Represents the calculated position of a node in the layout.
 *
 * @remarks
 * Contains both layout coordinates (center point) and final coordinates (top-left corner).
 * The centering offset is applied: x = layoutX - width/2, y = layoutY - height/2.
 *
 * @public
 */
export interface INodePosition {
  /** Unique identifier of the node */
  readonly id: string;
  /** Layout X coordinate (center point) */
  readonly layoutX: number;
  /** Layout Y coordinate (center point) */
  readonly layoutY: number;
  /** Final X coordinate (top-left corner) */
  readonly x: number;
  /** Final Y coordinate (top-left corner) */
  readonly y: number;
  /** Width of the node */
  readonly width: number;
  /** Height of the node */
  readonly height: number;
  /** Level index of the node */
  readonly level: number;
}

/**
 * Represents a horizontal layer in the graph hierarchy containing nodes at the same level.
 *
 * @remarks
 * All nodes in the same level share the same base Y coordinate.
 * The base Y is calculated as: levelIndex * 50 + baseOffset.
 *
 * @public
 */
export interface INodeLevel {
  /** Zero-based index of the level (0 = root level) */
  readonly levelIndex: number;
  /** Array of node IDs at this level */
  readonly nodes: string[];
  /** Y-coordinate baseline for this level (center line) */
  readonly baseY: number;
}

/**
 * Output of the layout calculation containing all node positions and metadata.
 *
 * @remarks
 * This is the primary return type for the layout engine's calculation methods.
 * Contains all information needed to render the graph with correct positioning.
 *
 * @public
 */
export interface ILayoutResult {
  /** Map of node ID to calculated position */
  readonly nodePositions: Map<string, INodePosition>;
  /** Array of levels with their base Y coordinates */
  readonly levels: INodeLevel[];
  /** Total width of the layout (widest level) */
  readonly totalWidth: number;
  /** Total height of the layout (max level * 50) */
  readonly totalHeight: number;
  /** Maximum level index in the graph */
  readonly maxLevel: number;
  /** Configuration used for this layout */
  readonly config: ILayoutConfig;
}

/**
 * Public interface for the layout engine service.
 *
 * @remarks
 * Defines the contract for layout calculation services. All implementations
 * must provide these three core methods.
 *
 * @public
 */
export interface ILayoutEngine {
  /**
   * Calculates layout for the given graph
   * @param graph The flow graph to layout
   * @param config Optional configuration (defaults to DEFAULT_LAYOUT_CONFIG)
   * @returns Layout result with node positions
   * @throws {LayoutError} If graph is invalid or contains cycles
   */
  layout(graph: FlowGraph, config?: ILayoutConfig): ILayoutResult;

  /**
   * Assigns levels to nodes using BFS
   * @param graph The flow graph
   * @returns Map of node ID to level index
   * @throws {LayoutError} If graph is invalid or contains cycles
   */
  assignLevels(graph: FlowGraph): Map<string, number>;

  /**
   * Calculates node positions based on levels and config
   * @param graph The flow graph
   * @param levels Map of node ID to level index
   * @param config Layout configuration
   * @returns Layout result with node positions
   */
  calculatePositions(
    graph: FlowGraph,
    levels: Map<string, number>,
    config: ILayoutConfig,
  ): ILayoutResult;
}

/**
 * Cache interface for storing layout results.
 *
 * @remarks
 * Provides methods for managing cached layout calculations to improve performance.
 *
 * @public
 */
export interface ILayoutCache {
  /**
   * Retrieves cached result
   * @param key Cache key
   * @returns Cached result or undefined if not found
   */
  get(key: string): ILayoutResult | undefined;

  /**
   * Stores result in cache
   * @param key Cache key
   * @param result Layout result to cache
   */
  set(key: string, result: ILayoutResult): void;

  /**
   * Checks if key exists in cache
   * @param key Cache key
   * @returns True if key exists, false otherwise
   */
  has(key: string): boolean;

  /**
   * Clears all cached entries
   */
  clear(): void;

  /**
   * Generates cache key from graph structure
   * @param graph The flow graph
   * @returns Unique cache key
   */
  generateKey(graph: FlowGraph): string;
}

/**
 * Enumeration of all possible layout error codes.
 *
 * @public
 */
export enum LayoutErrorCode {
  /** Graph structure is invalid (null, empty, or missing required data) */
  INVALID_GRAPH = 'INVALID_GRAPH',
  /** Graph contains cycles (not a DAG) */
  CYCLE_DETECTED = 'CYCLE_DETECTED',
  /** Node has no incoming or outgoing edges */
  ORPHAN_NODE = 'ORPHAN_NODE',
  /** Node width or height is invalid (<= 0 or NaN) */
  INVALID_NODE_SIZE = 'INVALID_NODE_SIZE',
  /** Failed to assign levels to nodes */
  LEVEL_ASSIGNMENT_FAILED = 'LEVEL_ASSIGNMENT_FAILED',
  /** Failed to calculate node positions */
  POSITION_CALCULATION_FAILED = 'POSITION_CALCULATION_FAILED',
}

// ============================================================================
// Constants
// ============================================================================

/**
 * Default layout configuration.
 *
 * @remarks
 * Uses the constitution-mandated 50px vertical spacing and sensible defaults
 * for other parameters.
 *
 * @public
 */
export const DEFAULT_LAYOUT_CONFIG: ILayoutConfig = {
  verticalSpacing: 50,
  horizontalSpacing: 75,
  baseYOffset: 0,
  centerGraph: true,
} as const;

// ============================================================================
// Factory Functions
// ============================================================================

/**
 * Creates a node position with validation.
 *
 * @param id - Unique identifier of the node
 * @param layoutX - Layout X coordinate (center point)
 * @param layoutY - Layout Y coordinate (center point)
 * @param width - Width of the node
 * @param height - Height of the node
 * @param level - Level index of the node
 * @returns Validated node position object
 * @throws Error if validation fails
 *
 * @public
 */
export function createNodePosition(
  id: string,
  layoutX: number,
  layoutY: number,
  width: number,
  height: number,
  level: number,
): INodePosition {
  // Validate inputs
  if (!id || id.trim() === '') {
    throw new Error('Node ID must be a non-empty string');
  }
  if (isNaN(layoutX) || isNaN(layoutY)) {
    throw new Error('Layout coordinates must be valid numbers');
  }
  if (width <= 0 || height <= 0 || isNaN(width) || isNaN(height)) {
    throw new Error(`Node dimensions must be positive numbers: width=${width}, height=${height}`);
  }
  if (level < 0) {
    throw new Error('Level must be non-negative');
  }

  // Calculate centering offsets
  const x = layoutX - width / 2;
  const y = layoutY - height / 2;

  return {
    id,
    layoutX,
    layoutY,
    x,
    y,
    width,
    height,
    level,
  };
}

/**
 * Creates a node level with validation.
 *
 * @param levelIndex - Zero-based index of the level
 * @param nodes - Array of node IDs at this level
 * @param baseOffset - Base Y offset for the entire graph
 * @returns Validated node level object
 * @throws Error if validation fails
 *
 * @public
 */
export function createNodeLevel(
  levelIndex: number,
  nodes: string[],
  baseOffset: number = 0,
): INodeLevel {
  // Validate inputs
  if (levelIndex < 0 || !Number.isInteger(levelIndex)) {
    throw new Error('Level index must be a non-negative integer');
  }
  if (!Array.isArray(nodes)) {
    throw new Error('Nodes must be an array');
  }

  // Check for duplicate node IDs
  const nodeSet = new Set(nodes);
  if (nodeSet.size !== nodes.length) {
    throw new Error('Node IDs must be unique within a level');
  }

  // Calculate base Y using fixed 50px spacing
  const baseY = levelIndex * 50 + baseOffset;

  return {
    levelIndex,
    nodes,
    baseY,
  };
}

/**
 * Creates a layout result with validation and automatic dimension calculation.
 *
 * @param nodePositions - Map of node ID to calculated position
 * @param levels - Array of levels with their base Y coordinates
 * @param config - Layout configuration used
 * @returns Validated layout result object
 * @throws Error if validation fails
 *
 * @public
 */
export function createLayoutResult(
  nodePositions: Map<string, INodePosition>,
  levels: INodeLevel[],
  config: ILayoutConfig,
): ILayoutResult {
  // Validate inputs
  if (nodePositions.size === 0) {
    throw new Error('Node positions cannot be empty');
  }
  if (levels.length === 0) {
    throw new Error('Levels cannot be empty');
  }

  // Calculate max level
  const maxLevel = levels.reduce((max, level) => Math.max(max, level.levelIndex), 0);

  // Calculate total height using fixed 50px spacing
  const baseOffset = config.baseYOffset ?? 0;
  const totalHeight = (maxLevel + 1) * config.verticalSpacing + baseOffset;

  // Calculate total width (widest level)
  const levelWidths = levels.map((level) => {
    const nodeWidths = Array.from(nodePositions.values())
      .filter((pos) => level.nodes.includes(pos.id))
      .map((pos) => pos.width);
    const spacing = config.horizontalSpacing ?? DEFAULT_LAYOUT_CONFIG.horizontalSpacing;
    return nodeWidths.reduce((sum, width) => sum + width, 0) + (nodeWidths.length - 1) * spacing;
  });
  const totalWidth = Math.max(...levelWidths, 0);

  return {
    nodePositions,
    levels,
    totalWidth,
    totalHeight,
    maxLevel,
    config,
  };
}

// ============================================================================
// Custom Error Class
// ============================================================================

/**
 * Custom error type for layout calculation failures.
 *
 * @remarks
 * Extends the built-in Error class with an error code for programmatic handling.
 * All layout engine methods throw this error type on failure.
 *
 * @public
 */
export class LayoutError extends Error {
  /** Error type identifier */
  public readonly name = 'LayoutError';

  /**
   * Creates a new LayoutError instance.
   *
   * @param message - Human-readable error message
   * @param code - Error code from LayoutErrorCode enum
   * @param cause - Original error that caused this error (optional)
   */
  constructor(
    message: string,
    public readonly code: LayoutErrorCode,
    cause?: unknown,
  ) {
    super(message);

    // Maintain proper stack trace for where our error was thrown
    if (Error.captureStackTrace) {
      Error.captureStackTrace(this, LayoutError);
    }

    // Attach cause if provided
    if (cause !== undefined) {
      (this as any).cause = cause;
    }
  }
}

// ============================================================================
// Exports
// ============================================================================

// Re-export types for convenience
export type {
  ILayoutConfig,
  INodePosition,
  INodeLevel,
  ILayoutResult,
  ILayoutEngine,
  ILayoutCache,
};

// Export enums
export { LayoutErrorCode };

// Export constants
export { DEFAULT_LAYOUT_CONFIG };

// Export factory functions
export { createNodePosition, createNodeLevel, createLayoutResult };

// Export error class
export { LayoutError };
