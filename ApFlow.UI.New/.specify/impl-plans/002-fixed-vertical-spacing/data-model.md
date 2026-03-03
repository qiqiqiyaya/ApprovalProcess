# Data Model: Fixed Vertical Spacing Layout Engine

**Feature ID**: 002-fixed-vertical-spacing  
**Phase**: Phase 1 - Design & Contracts  
**Date**: 2026-03-04

---

## Overview

This document defines the data entities, their attributes, relationships, and validation rules for the custom layout engine that enforces strict 50px vertical spacing.

---

## Entity: LayoutConfig

**Description**: Configuration object for the layout engine with fixed vertical spacing.

### Attributes

| Attribute | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `verticalSpacing` | `number` | Yes | `50` | Fixed vertical spacing between consecutive levels (immutable per constitution) |
| `horizontalSpacing` | `number` | No | `75` | Horizontal spacing between nodes in the same level |
| `baseYOffset` | `number` | No | `0` | Base Y offset for the entire graph (allows padding) |
| `centerGraph` | `boolean` | No | `true` | Whether to center the graph horizontally in the viewport |

### Validation Rules

- `verticalSpacing` MUST be exactly 50 (enforced by type system)
- `horizontalSpacing` MUST be a positive number > 0
- `baseYOffset` MUST be a non-negative number >= 0

### TypeScript Definition

```typescript
export interface ILayoutConfig {
  /** Fixed vertical spacing between consecutive levels (50px per constitution) */
  readonly verticalSpacing: 50;
  /** Horizontal spacing between nodes in the same level */
  readonly horizontalSpacing?: number;
  /** Base Y offset for the entire graph */
  readonly baseYOffset?: number;
  /** Whether to center the graph horizontally in the viewport */
  readonly centerGraph?: boolean;
}

export const DEFAULT_LAYOUT_CONFIG: ILayoutConfig = {
  verticalSpacing: 50,
  horizontalSpacing: 75,
  baseYOffset: 0,
  centerGraph: true,
} as const;
```

---

## Entity: NodeLevel

**Description**: Represents a horizontal layer in the graph hierarchy containing nodes at the same level.

### Attributes

| Attribute | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `levelIndex` | `number` | Yes | - | Zero-based index of the level (0 = root level) |
| `nodes` | `string[]` | Yes | `[]` | Array of node IDs at this level |
| `baseY` | `number` | Yes | - | Y-coordinate baseline for this level (center line) |

### Validation Rules

- `levelIndex` MUST be a non-negative integer >= 0
- `nodes` MUST contain unique node IDs (no duplicates)
- `baseY` MUST be calculated as `levelIndex * 50 + baseOffset`

### TypeScript Definition

```typescript
export interface INodeLevel {
  /** Zero-based index of the level (0 = root level) */
  readonly levelIndex: number;
  /** Array of node IDs at this level */
  readonly nodes: string[];
  /** Y-coordinate baseline for this level (center line) */
  readonly baseY: number;
}

export function createNodeLevel(
  levelIndex: number,
  nodes: string[],
  baseOffset: number = 0
): INodeLevel {
  if (levelIndex < 0) {
    throw new Error('Level index must be non-negative');
  }
  if (new Set(nodes).size !== nodes.length) {
    throw new Error('Node IDs must be unique within a level');
  }
  
  const baseY = levelIndex * 50 + baseOffset;
  
  return {
    levelIndex,
    nodes,
    baseY,
  };
}
```

---

## Entity: NodePosition

**Description**: Represents the calculated position of a node in the layout.

### Attributes

| Attribute | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `id` | `string` | Yes | - | Unique identifier of the node |
| `layoutX` | `number` | Yes | - | Layout X coordinate (center point) |
| `layoutY` | `number` | Yes | - | Layout Y coordinate (center point) |
| `x` | `number` | Yes | - | Final X coordinate (top-left corner) |
| `y` | `number` | Yes | - | Final Y coordinate (top-left corner) |
| `width` | `number` | Yes | - | Width of the node |
| `height` | `number` | Yes | - | Height of the node |
| `level` | `number` | Yes | - | Level index of the node |

### Validation Rules

- `id` MUST be a non-empty string
- `layoutX` and `layoutY` MUST be valid numbers (not NaN)
- `x` MUST equal `layoutX - width / 2` (centering offset)
- `y` MUST equal `layoutY - height / 2` (centering offset)
- `width` and `height` MUST be positive numbers > 0
- `level` MUST be a non-negative integer >= 0
- `layoutY` MUST equal `level * 50 + baseOffset` (strict vertical spacing)

### TypeScript Definition

```typescript
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

export function createNodePosition(
  id: string,
  layoutX: number,
  layoutY: number,
  width: number,
  height: number,
  level: number
): INodePosition {
  if (!id || id.trim() === '') {
    throw new Error('Node ID must be a non-empty string');
  }
  if (isNaN(layoutX) || isNaN(layoutY)) {
    throw new Error('Layout coordinates must be valid numbers');
  }
  if (width <= 0 || height <= 0) {
    throw new Error('Node dimensions must be positive');
  }
  if (level < 0) {
    throw new Error('Level must be non-negative');
  }
  
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
```

---

## Entity: LayoutResult

**Description**: Output of the layout calculation containing all node positions and metadata.

### Attributes

| Attribute | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `nodePositions` | `Map<string, INodePosition>` | Yes | `new Map()` | Map of node ID to calculated position |
| `levels` | `INodeLevel[]` | Yes | `[]` | Array of levels with their base Y coordinates |
| `totalWidth` | `number` | Yes | - | Total width of the layout (widest level) |
| `totalHeight` | `number` | Yes | - | Total height of the layout (max level * 50) |
| `maxLevel` | `number` | Yes | - | Maximum level index in the graph |
| `config` | `ILayoutConfig` | Yes | - | Configuration used for this layout |

### Validation Rules

- `nodePositions` MUST contain an entry for every node in the input graph
- `levels` MUST be ordered by `levelIndex` ascending
- `totalWidth` MUST equal the width of the widest level
- `totalHeight` MUST equal `(maxLevel + 1) * 50 + baseOffset` (strict vertical spacing)
- `maxLevel` MUST be the highest level index among all nodes

### TypeScript Definition

```typescript
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

export function createLayoutResult(
  nodePositions: Map<string, INodePosition>,
  levels: INodeLevel[],
  config: ILayoutConfig
): ILayoutResult {
  if (nodePositions.size === 0) {
    throw new Error('Node positions cannot be empty');
  }
  if (levels.length === 0) {
    throw new Error('Levels cannot be empty');
  }
  
  const maxLevel = levels.reduce((max, level) => Math.max(max, level.levelIndex), 0);
  const totalHeight = (maxLevel + 1) * config.verticalSpacing + (config.baseYOffset ?? 0);
  
  // Calculate total width (widest level)
  const levelWidths = levels.map(level => {
    const nodeWidths = Array.from(nodePositions.values())
      .filter(pos => level.nodes.includes(pos.id))
      .map(pos => pos.width);
    return nodeWidths.reduce((sum, width) => sum + width, 0) +
      (nodeWidths.length - 1) * (config.horizontalSpacing ?? DEFAULT_LAYOUT_CONFIG.horizontalSpacing);
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
```

---

## Entity: LayoutEngine

**Description**: Service responsible for calculating node positions with strict vertical spacing.

### Methods

| Method | Signature | Description |
|--------|-----------|-------------|
| `layout()` | `layout(graph: FlowGraph, config?: ILayoutConfig): ILayoutResult` | Calculates layout for the given graph |
| `assignLevels()` | `assignLevels(graph: FlowGraph): Map<string, number>` | Assigns levels to nodes using BFS |
| `calculatePositions()` | `calculatePositions(graph: FlowGraph, levels: Map<string, number>, config: ILayoutConfig): ILayoutResult` | Calculates node positions based on levels and config |

### TypeScript Definition

```typescript
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
    config: ILayoutConfig
  ): ILayoutResult;
}
```

---

## Entity: LayoutError

**Description**: Custom error type for layout calculation failures.

### Attributes

| Attribute | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `name` | `string` | Yes | `'LayoutError'` | Error type identifier |
| `message` | `string` | Yes | - | Human-readable error message |
| `code` | `string` | Yes | - | Error code for programmatic handling |
| `cause` | `unknown` | No | `undefined` | Original error that caused this error |

### Error Codes

| Code | Description |
|------|-------------|
| `INVALID_GRAPH` | Graph structure is invalid (null, empty, or missing required data) |
| `CYCLE_DETECTED` | Graph contains cycles (not a DAG) |
| `ORPHAN_NODE` | Node has no incoming or outgoing edges |
| `INVALID_NODE_SIZE` | Node width or height is invalid (<= 0 or NaN) |
| `LEVEL_ASSIGNMENT_FAILED` | Failed to assign levels to nodes |
| `POSITION_CALCULATION_FAILED` | Failed to calculate node positions |

### TypeScript Definition

```typescript
export enum LayoutErrorCode {
  INVALID_GRAPH = 'INVALID_GRAPH',
  CYCLE_DETECTED = 'CYCLE_DETECTED',
  ORPHAN_NODE = 'ORPHAN_NODE',
  INVALID_NODE_SIZE = 'INVALID_NODE_SIZE',
  LEVEL_ASSIGNMENT_FAILED = 'LEVEL_ASSIGNMENT_FAILED',
  POSITION_CALCULATION_FAILED = 'POSITION_CALCULATION_FAILED',
}

export class LayoutError extends Error {
  constructor(
    message: string,
    public readonly code: LayoutErrorCode,
    cause?: unknown
  ) {
    super(message);
    this.name = 'LayoutError';
    if (cause) {
      this.cause = cause;
    }
    // Maintains proper stack trace for where our error was thrown
    if (Error.captureStackTrace) {
      Error.captureStackTrace(this, LayoutError);
    }
  }
}
```

---

## Entity: LayoutCache

**Description**: Cache for storing layout results to avoid redundant calculations.

### Attributes

| Attribute | Type | Required | Default | Description |
|-----------|------|----------|---------|-------------|
| `cache` | `Map<string, ILayoutResult>` | Yes | `new Map()` | Internal cache storage |
| `maxSize` | `number` | No | `100` | Maximum number of entries in the cache |

### Methods

| Method | Signature | Description |
|--------|-----------|-------------|
| `get()` | `get(key: string): ILayoutResult \| undefined` | Retrieves cached result |
| `set()` | `set(key: string, result: ILayoutResult): void` | Stores result in cache |
| `has()` | `has(key: string): boolean` | Checks if key exists in cache |
| `clear()` | `clear(): void` | Clears all cached entries |
| `generateKey()` | `generateKey(graph: FlowGraph): string` | Generates cache key from graph structure |

### TypeScript Definition

```typescript
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

export class LayoutCache implements ILayoutCache {
  private cache = new Map<string, ILayoutResult>();
  private readonly maxSize: number;
  
  constructor(maxSize: number = 100) {
    this.maxSize = maxSize;
  }
  
  get(key: string): ILayoutResult | undefined {
    return this.cache.get(key);
  }
  
  set(key: string, result: ILayoutResult): void {
    if (this.cache.size >= this.maxSize) {
      // Simple FIFO: remove first entry
      const firstKey = this.cache.keys().next().value;
      this.cache.delete(firstKey);
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
    const nodeIds = graph.nodes.map(n => n.id).sort().join(',');
    const edgePairs = graph.edges
      .map(e => `${e.source}-${e.target}`)
      .sort()
      .join('|');
    return `${nodeIds}|${edgePairs}`;
  }
}
```

---

## Relationships

### Entity Relationship Diagram (ERD)

```
┌─────────────┐         ┌──────────────┐         ┌─────────────┐
│ LayoutConfig│         │ LayoutEngine │         │ LayoutError │
└──────┬──────┘         └──────┬───────┘         └─────────────┘
       │                       │
       │ uses                  │ uses
       ▼                       ▼
┌─────────────────┐    ┌─────────────┐
│  LayoutResult   │───▶│NodePosition│
└────────┬────────┘    └─────────────┘
         │
         │ contains
         ▼
┌─────────────┐    ┌─────────────┐
│  NodeLevel  │    │LayoutCache  │
└─────────────┘    └─────────────┘
```

### Relationship Descriptions

| Source Entity | Target Entity | Cardinality | Description |
|---------------|---------------|-------------|-------------|
| `LayoutEngine` | `LayoutConfig` | 1:1 | Engine uses one config instance per layout |
| `LayoutEngine` | `LayoutResult` | 1:N | Engine produces one result per graph |
| `LayoutResult` | `NodePosition` | 1:N | Result contains positions for all nodes |
| `LayoutResult` | `NodeLevel` | 1:N | Result contains all levels in the graph |
| `LayoutEngine` | `LayoutError` | 1:1 | Engine throws errors on failure |
| `LayoutCache` | `LayoutResult` | 1:N | Cache stores multiple results |

---

## Validation Rules Summary

### Cross-Entity Validation

1. **Vertical Spacing Enforcement**:
   - For any two consecutive levels `i` and `i+1`:
     - `levels[i+1].baseY - levels[i].baseY === 50`
   - This MUST hold true for ALL level pairs in the layout result

2. **Centering Offset Consistency**:
   - For every node position:
     - `x === layoutX - width / 2`
     - `y === layoutY - height / 2`

3. **Level Assignment Completeness**:
   - Every node in the input graph MUST have a corresponding entry in `nodePositions`
   - Every node MUST be assigned to exactly one level

4. **Cache Key Uniqueness**:
   - Different graph structures MUST generate different cache keys
   - Identical graph structures MUST generate identical cache keys

---

## State Transitions

### Layout Calculation Lifecycle

```
[Graph Input] 
    │
    ▼
[Validate Graph] ──▶ [Throw LayoutError] (if invalid)
    │
    ▼
[Assign Levels (BFS)] ──▶ [Throw LayoutError] (if cycle detected)
    │
    ▼
[Check Cache] ──▶ [Return Cached Result] (if hit)
    │ (miss)
    ▼
[Calculate Positions]
    │
    ▼
[Create LayoutResult]
    │
    ▼
[Store in Cache]
    │
    ▼
[Return LayoutResult]
```

### Cache State Transitions

```
[Empty] ──▶ [Set] ──▶ [Has Entries] ──▶ [Get/Hit] ──▶ [Return Result]
                │
                └───▶ [Clear] ──▶ [Empty]
                │
                └───▶ [Set (full)] ──▶ [Evict FIFO] ──▶ [Set New] ──▶ [Has Entries]
```

---

## Data Model Compliance with Constitution

| Principle | Compliance | Notes |
|-----------|------------|-------|
| **Principle 1: Type Safety First** | ✅ COMPLIANT | All entities have explicit TypeScript interfaces with no `any` types |
| **Principle 2: Component Architecture** | ✅ COMPLIANT | LayoutEngine is a singleton service following Angular architecture |
| **Principle 3: Layout Performance** | ✅ COMPLIANT | `verticalSpacing: 50` is enforced via readonly property and validation |
| **Principle 4: Code Consistency** | ✅ COMPLIANT | All interfaces follow naming convention (`I` prefix for interfaces) |
| **Principle 5: Form Safety** | N/A | Not applicable to layout engine |
| **Principle 6: RxJS Patterns** | ✅ COMPLIANT | LayoutCache and LayoutEngine designed for RxJS integration |

---

## Next Steps

Proceed to create:
1. **Interface Contracts** (`/contracts/`) - Public API contracts for layout engine
2. **Quick Start Guide** (`quickstart.md`) - Developer onboarding documentation
3. **Update Agent Context** - Add new types and services to agent context file

---

**End of Data Model Document**
