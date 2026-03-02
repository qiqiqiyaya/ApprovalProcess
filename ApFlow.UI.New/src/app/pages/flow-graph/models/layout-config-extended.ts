import { LayoutConfig, LayoutResult, NodePosition, Point } from './layout-config';
import { FlowNode } from './flow-node';
import { FlowEdge } from './flow-edge';
import { BranchGroup } from './flow-group';
import { Inject, InjectionToken } from '@angular/core';

/**
 * Layout calculation engine interface
 * Defines the contract for layout calculation implementations
 */
export interface LayoutEngine<TNode = FlowNode, TEdge = FlowEdge> {
  /**
   * Calculates layout positions for a graph
   * @param nodes Array of nodes to layout
   * @param edges Array of edges defining connections
   * @param config Layout configuration parameters
   * @param branchGroups Optional array of parallel branch groups
   * @returns Calculated layout result
   * @throws LayoutError if calculation fails
   */
  layout(
    nodes: TNode[],
    edges: TEdge[],
    config: LayoutConfig,
    branchGroups?: BranchGroup[]
  ): LayoutResult;
}

/**
 * Layout error codes
 */
export enum LayoutErrorCode {
  /**
   * Graph contains circular dependencies
   */
  CIRCULAR_DEPENDENCY = 'CIRCULAR_DEPENDENCY',

  /**
   * Configuration is invalid
   */
  INVALID_CONFIG = 'INVALID_CONFIG',

  /**
   * Graph has no nodes
   */
  EMPTY_GRAPH = 'EMPTY_GRAPH',

  /**
   * Parallel branch structure is invalid
   */
  PARALLEL_BRANCH_ERROR = 'PARALLEL_BRANCH_ERROR',

  /**
   * Node not found in graph
   */
  NODE_NOT_FOUND = 'NODE_NOT_FOUND',

  /**
   * Layout calculation timeout
   */
  TIMEOUT = 'TIMEOUT',
}

/**
 * Layout calculation error
 */
export interface LayoutError {
  /**
   * Human-readable error message
   */
  message: string;

  /**
   * Error code for programmatic handling
   */
  code: LayoutErrorCode;

  /**
   * Additional error details
   */
  details?: unknown;
}

/**
 * Union type for layout calculation result or error
 */
export type LayoutResultOrError = LayoutResult | LayoutError;

/**
 * Type guard for LayoutError
 * @param result Result to check
 * @returns true if result is a LayoutError
 */
export function isLayoutError(result: LayoutResultOrError): result is LayoutError {
  return 'code' in result;
}

/**
 * Type guard for LayoutResult
 * @param result Result to check
 * @returns true if result is a LayoutResult
 */
export function isLayoutResult(result: LayoutResultOrError): result is LayoutResult {
  return !isLayoutError(result);
}

/**
 * Hierarchical node representation
 * Used during layout calculation to build the hierarchy
 */
export interface HierarchicalNode<T = unknown> {
  /**
   * The actual node data
   */
  node: T;

  /**
   * Level in the hierarchy (0 = root level)
   */
  level: number;

  /**
   * Child nodes in the hierarchy
   */
  children: HierarchicalNode<T>[];

  /**
   * Parent node (null for root nodes)
   */
  parent: HierarchicalNode<T> | null;
}

/**
 * Injection token for X6 Graph instance
 * Provides type-safe access to the AntV X6 graph
 */
export const X6_GRAPH = new InjectionToken<any>('X6_GRAPH');
