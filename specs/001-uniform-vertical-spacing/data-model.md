# Data Model: Uniform Vertical Spacing for Flow Graph Nodes

**Feature Branch**: `001-uniform-vertical-spacing`  
**Date**: 2026-03-03  
**Status**: Final Design

This document defines the data models, entities, and interfaces for implementing uniform vertical spacing in the ApFlow.UI flow graph visualization system.

---

## Table of Contents

1. [Core Entities](#core-entities)
2. [Configuration Models](#configuration-models)
3. [Layout Calculation Models](#layout-calculation-models)
4. [Service Interfaces](#service-interfaces)
5. [Entity Relationships](#entity-relationships)
6. [Validation Rules](#validation-rules)
7. [State Transitions](#state-transitions)

---

## Core Entities

### LayoutConfig

Represents the configuration parameters for layout calculation, including spacing values and animation settings.

```typescript
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
```

**Validation Rules**:
- `verticalSpacing` must be between 20 and 200 pixels
- `horizontalSpacing` must be between 50 and 500 pixels
- `animationDuration` must be non-negative if provided

---

### LayoutResult

Represents the output of a layout calculation, containing calculated node positions and metadata.

```typescript
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
```

---

### NodePosition

Represents the position of a single node in the layout.

```typescript
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
```

**Invariants**:
- `x` and `y` must be non-negative
- `id` must reference an existing node in the graph
- Nodes at the same hierarchy level should have identical `y` values

---

### EdgeRoute

Represents routing information for a single edge in the layout.

```typescript
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
```

---

### Point

Represents a 2D point in the layout.

```typescript
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
```

---

### LayoutMetadata

Contains metadata about the layout calculation, including performance metrics and statistics.

```typescript
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
```

---

## Configuration Models

### StrictLayoutConfig

A strict implementation of `LayoutConfig` with runtime validation.

```typescript
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
```

---

### LayoutPreset

Represents a predefined layout configuration preset.

```typescript
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
```

**Presets**:

```typescript
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
```

---

## Layout Calculation Models

### LayoutEngine

Generic interface for layout calculation engines.

```typescript
/**
 * Layout calculation engine interface
 * Defines the contract for layout calculation implementations
 */
export interface LayoutEngine<TNode = Node, TEdge = Edge> {
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
```

---

### LayoutError

Represents an error that occurred during layout calculation.

```typescript
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
```

---

### LayoutResultOrError

Union type for successful layout result or error.

```typescript
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
```

---

### HierarchicalNode

Represents a node in a hierarchical tree structure for layout calculation.

```typescript
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
```

---

## Service Interfaces

### LayoutService

Main service for layout calculation and application.

```typescript
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Graph } from '@antv/x6';

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
 * Layout service implementation
 */
@Injectable({ providedIn: 'root' })
export class LayoutService implements ILayoutService {
  constructor(
    @Inject(X6_GRAPH) private graph: Graph,
    private layoutEngine: LayoutEngine,
    private configService: LayoutConfigService
  ) {}

  // Implementation methods...
}
```

---

### LayoutConfigService

Service for managing layout configuration.

```typescript
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

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
  applyPreset(preset: LayoutPreset): void;

  /**
   * Gets all available presets
   * @returns Array of presets
   */
  getPresets(): LayoutPreset[];

  /**
   * Gets current preset (if any)
   * @returns Current preset or null
   */
  getCurrentPreset(): LayoutPreset | null;
}

/**
 * Layout configuration service implementation
 */
@Injectable({ providedIn: 'root' })
export class LayoutConfigService implements ILayoutConfigService {
  private readonly STORAGE_KEY = 'apflow.layout.config';
  private configSubject: BehaviorSubject<LayoutConfig>;

  constructor() {
    this.configSubject = new BehaviorSubject<LayoutConfig>(this.loadDefaults());
    this.loadFromStorage();
  }

  // Implementation methods...
}
```

---

### VerticalSpacingCalculator

Service for calculating vertical spacing in hierarchical graphs.

```typescript
import { Injectable } from '@angular/core';

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
  calculateLevels(nodes: Node[], edges: Edge[]): Map<string, number>;

  /**
   * Groups nodes by level
   * @param nodes Array of nodes
   * @param levels Map of node ID to level
   * @returns Map of level to array of nodes
   */
  groupNodesByLevel(nodes: Node[], levels: Map<string, number>): Map<number, Node[]>;

  /**
   * Calculates node positions with uniform vertical spacing
   * @param levelNodes Map of level to nodes
   * @param config Layout configuration
   * @returns Array of node positions
   */
  calculatePositions(
    levelNodes: Map<number, Node[]>,
    config: LayoutConfig
  ): NodePosition[];

  /**
   * Handles parallel branches to ensure uniform spacing
   * @param levelNodes Map of level to nodes
   * @param branchGroups Array of branch groups
   * @returns Updated level nodes
   */
  handleParallelBranches(
    levelNodes: Map<number, Node[]>,
    branchGroups: BranchGroup[]
  ): Map<number, Node[]>;
}

/**
 * Vertical spacing calculator implementation
 */
@Injectable({ providedIn: 'root' })
export class VerticalSpacingCalculator implements IVerticalSpacingCalculator {
  // Implementation methods...
}
```

---

## Entity Relationships

### FlowGraph Integration

The layout system integrates with existing `FlowGraph` model:

```typescript
export class FlowGraph {
  nodes: FlowNode[];
  edges: FlowEdge[];
  private groups: Map<string, BranchGroup> = new Map();

  /**
   * Applies layout result to the graph
   * @param result Layout result to apply
   */
  applyLayout(result: LayoutResult): void {
    result.nodes.forEach(position => {
      const node = this.findNodeById(position.id);
      if (node) {
        node.x = position.x;
        node.y = position.y;
      }
    });
  }

  /**
   * Converts to format suitable for layout engine
   * @returns Array of nodes for layout calculation
   */
  toLayoutNodes(): Node[] {
    return this.nodes.map(node => ({
      id: node.id,
      width: node.width,
      height: node.height,
      // ... other properties
    }));
  }

  /**
   * Converts to format suitable for layout engine
   * @returns Array of edges for layout calculation
   */
  toLayoutEdges(): Edge[] {
    return this.edges.map(edge => ({
      id: edge.id,
      source: edge.source,
      target: edge.target,
    }));
  }
}
```

### BranchGroup Integration

The layout system respects existing `BranchGroup` structure:

```typescript
export class BranchGroup {
  id: string;
  private branches: Map<number, string[]>; // branch index -> node IDs
  private mergeNodeId?: string;

  /**
   * Gets all nodes in this branch group
   * @returns Array of node IDs
   */
  getAllNodes(): string[] {
    const nodes: string[] = [];
    this.branches.forEach(branchNodes => {
      nodes.push(...branchNodes);
    });
    if (this.mergeNodeId) {
      nodes.push(this.mergeNodeId);
    }
    return nodes;
  }

  /**
   * Validates uniform spacing for this branch group
   * @param levels Map of node ID to level
   * @param spacing Vertical spacing to validate
   * @returns true if spacing is uniform
   */
  validateUniformSpacing(levels: Map<string, number>, spacing: number): boolean {
    const nodes = this.getAllNodes();
    const nodeLevels = nodes.map(id => levels.get(id) ?? -1);
    const uniqueLevels = new Set(nodeLevels);
    return uniqueLevels.size === 1; // All nodes at same level
  }
}
```

### Relationship Diagram

```
┌─────────────────┐
│   LayoutConfig  │
└────────┬────────┘
         │ manages
         ▼
┌─────────────────────────────┐
│  LayoutConfigService        │
└────────┬────────────────────┘
         │ observes
         ▼
┌─────────────────────────────┐
│      LayoutService          │
└────────┬────────────────────┘
         │ uses
         ▼
┌─────────────────────────────┐
│    LayoutEngine             │
└────────┬────────────────────┘
         │ uses
         ▼
┌─────────────────────────────┐
│  VerticalSpacingCalculator   │
└────────┬────────────────────┘
         │ operates on
         ▼
┌─────────────────────────────┐
│       FlowGraph             │
└─────────────────────────────┘
```

---

## Validation Rules

### LayoutConfig Validation

| Field | Rule | Error Message |
|-------|------|---------------|
| `verticalSpacing` | Must be ≥ 20 and ≤ 200 | `verticalSpacing must be between 20 and 200` |
| `horizontalSpacing` | Must be ≥ 50 and ≤ 500 | `horizontalSpacing must be between 50 and 500` |
| `animationDuration` | Must be ≥ 0 (if provided) | `animationDuration must be non-negative` |

### LayoutResult Validation

| Field | Rule | Error Message |
|-------|------|---------------|
| `nodes` | Must have at least one node | `Layout result must contain at least one node` |
| `nodes[].id` | Must reference existing node | `Node {id} not found in graph` |
| `nodes[].x` | Must be ≥ 0 | `Node x position must be non-negative` |
| `nodes[].y` | Must be ≥ 0 | `Node y position must be non-negative` |

### Uniform Spacing Validation

**Rule**: All nodes at the same hierarchy level must have the same Y position.

```typescript
function validateUniformSpacing(result: LayoutResult): boolean {
  const levelGroups = new Map<number, number[]>();
  
  result.nodes.forEach(node => {
    const level = node.level ?? 0;
    if (!levelGroups.has(level)) {
      levelGroups.set(level, []);
    }
    levelGroups.get(level)!.push(node.y);
  });
  
  // Check all nodes at same level have same Y
  for (const [level, yPositions] of levelGroups) {
    const uniqueY = new Set(yPositions);
    if (uniqueY.size > 1) {
      console.error(`Level ${level} has varying Y positions:`, Array.from(uniqueY));
      return false;
    }
  }
  
  return true;
}
```

### Circular Dependency Validation

**Rule**: Graph must not contain circular dependencies.

```typescript
function validateNoCircularDependencies(nodes: Node[], edges: Edge[]): void {
  const visited = new Set<string>();
  const recursionStack = new Set<string>();
  const adjacencyList = buildAdjacencyList(nodes, edges);

  function hasCycle(nodeId: string): boolean {
    visited.add(nodeId);
    recursionStack.add(nodeId);

    const neighbors = adjacencyList.get(nodeId) ?? [];
    for (const neighbor of neighbors) {
      if (!visited.has(neighbor)) {
        if (hasCycle(neighbor)) {
          return true;
        }
      } else if (recursionStack.has(neighbor)) {
        return true;
      }
    }

    recursionStack.delete(nodeId);
    return false;
  }

  for (const node of nodes) {
    if (!visited.has(node.id)) {
      if (hasCycle(node.id)) {
        throw new LayoutError({
          message: 'Graph contains circular dependencies',
          code: LayoutErrorCode.CIRCULAR_DEPENDENCY,
          details: { nodes, edges },
        });
      }
    }
  }
}
```

---

## State Transitions

### Layout Calculation State Machine

```
┌─────────────┐
│    Idle     │
└──────┬──────┘
       │ triggerLayout()
       ▼
┌─────────────┐
│  Calculating│
└──────┬──────┘
       │ success
       ▼
┌─────────────┐
│    Ready    │
└──────┬──────┘
       │ applyLayout()
       ▼
┌─────────────┐
│   Applying  │
└──────┬──────┘
       │ complete
       ▼
┌─────────────┐
│  Complete   │
└──────┬──────┘
       │
       ▼
   ┌─────┐
   │ Idle │
   └─────┘
```

### Configuration State Transitions

```
┌────────────────┐
│  Initial (50px)│
└───────┬────────┘
        │ updateConfig()
        ▼
┌────────────────┐
│  Updating      │
└───────┬────────┘
        │ validation
        ▼
   ┌────────┐
   │ Valid  │────┐
   └────────┘    │
        │        │ invalid
        │        ▼
        │   ┌──────────┐
        │   │  Error   │
        │   └──────────┘
        │
        │ saveToStorage()
        ▼
┌────────────────┐
│  Saved         │
└────────────────┘
```

### Error Recovery

```
┌─────────────┐
│   Error     │
└──────┬──────┘
       │
       ├─► CIRCULAR_DEPENDENCY → Notify user, disable layout
       ├─► INVALID_CONFIG → Revert to previous config
       ├─► EMPTY_GRAPH → Do nothing (valid state)
       ├─► PARALLEL_BRANCH_ERROR → Log error, skip affected groups
       └─► TIMEOUT → Retry with timeout, fallback to simple layout
```

---

## TypeScript Type Summary

### Key Interfaces

| Interface | Purpose |
|-----------|---------|
| `LayoutConfig` | Configuration parameters |
| `LayoutResult` | Layout calculation output |
| `NodePosition` | Single node position |
| `LayoutMetadata` | Layout statistics |
| `LayoutEngine<TNode, TEdge>` | Generic layout engine contract |
| `ILayoutService` | Layout service contract |
| `ILayoutConfigService` | Configuration service contract |

### Key Classes

| Class | Purpose |
|-------|---------|
| `StrictLayoutConfig` | Validated configuration |
| `LayoutService` | Main layout service |
| `LayoutConfigService` | Configuration management |
| `VerticalSpacingCalculator` | Spacing calculation logic |

### Utility Types

| Type | Purpose |
|------|---------|
| `LayoutResultOrError` | Result or error union |
| `LayoutPreset` | Preset configuration |
| `HierarchicalNode<T>` | Hierarchical tree node |

---

## Implementation Notes

### Integration Points

1. **FlowGraph**: Add `applyLayout()` method
2. **BranchGroup**: Add `validateUniformSpacing()` method
3. **EditorService**: Integrate with LayoutService
4. **EditorComponent**: Subscribe to layout updates

### Performance Considerations

1. Cache layout results for identical configurations
2. Use RxJS `debounceTime(16)` for 60fps performance
3. Implement incremental updates for large graphs
4. Benchmark with 100+ nodes

### Type Safety

1. No `any` types allowed
2. All interfaces must have TSDoc comments
3. Use `strict` TypeScript compiler options
4. Validate all inputs at service boundaries

---

**Data Model Completed**: 2026-03-03  
**Status**: ✅ Ready for Implementation  
**Next Step**: Generate `quickstart.md` and update agent context
