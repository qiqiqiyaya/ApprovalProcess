# Interface Contract: Layout Engine

**Feature ID**: 002-fixed-vertical-spacing  
**Contract Version**: 1.0.0  
**Date**: 2026-03-04

---

## Contract Overview

This document defines the public interface contract for the custom layout engine service that enforces strict 50px vertical spacing in the ApFlow.UI flow graph editor.

### Purpose

The layout engine is a **service contract** that provides layout calculation capabilities to the `EditorService` and other consumers. It is responsible for:
- Assigning levels to nodes using BFS
- Calculating node positions with strict vertical spacing
- Caching layout results for performance
- Providing type-safe interfaces for all layout operations

### Scope

- **In Scope**: Public API of `FlowLayoutEngine` service, type definitions, error handling
- **Out of Scope**: Internal implementation details (e.g., BFS algorithm specifics), private helper functions

---

## Public API Contract

### Service: FlowLayoutEngine

**Package**: `@app/pages/flow-graph/services`  
**Provider**: `Injectable({ providedIn: 'root' })`  
**Interface**: `ILayoutEngine`

#### Method: `layout()`

Calculates the layout for a given flow graph with strict 50px vertical spacing.

**Signature**:
```typescript
layout(graph: FlowGraph, config?: ILayoutConfig): ILayoutResult
```

**Parameters**:

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `graph` | `FlowGraph` | Yes | The flow graph to layout. Must be a valid Directed Acyclic Graph (DAG). |
| `config` | `ILayoutConfig` | No | Optional layout configuration. Defaults to `DEFAULT_LAYOUT_CONFIG`. |

**Returns**: `ILayoutResult` - The calculated layout with node positions.

**Throws**: `LayoutError` - If the graph is invalid or contains cycles.

**Error Codes**:

| Code | Condition | Message |
|------|-----------|---------|
| `INVALID_GRAPH` | `graph` is null, undefined, or empty | "Graph must be a valid FlowGraph instance with nodes and edges" |
| `CYCLE_DETECTED` | Graph contains cycles | "Graph contains cycles. Layout requires a Directed Acyclic Graph (DAG)" |
| `INVALID_NODE_SIZE` | Node width or height is invalid | "Node {id} has invalid dimensions: width={width}, height={height}" |

**Preconditions**:
1. `graph` must be a valid `FlowGraph` instance
2. `graph.nodes` must not be empty
3. `graph.edges` must be a valid array
4. Graph must not contain cycles

**Postconditions**:
1. All nodes have a valid `INodePosition` in the result
2. All consecutive level pairs maintain exactly 50px vertical spacing
3. Result is cacheable and reproducible for the same input

**Example Usage**:
```typescript
const layoutEngine = new FlowLayoutEngine();
const config: ILayoutConfig = {
  verticalSpacing: 50,
  horizontalSpacing: 75,
  baseYOffset: 20,
  centerGraph: true,
};

try {
  const result = layoutEngine.layout(flowGraph, config);
  console.log(`Layout calculated: ${result.totalWidth}x${result.totalHeight}`);
} catch (error) {
  if (error instanceof LayoutError) {
    console.error(`Layout failed (${error.code}): ${error.message}`);
  }
}
```

---

#### Method: `assignLevels()`

Assigns levels to nodes using Breadth-First Search (BFS).

**Signature**:
```typescript
assignLevels(graph: FlowGraph): Map<string, number>
```

**Parameters**:

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `graph` | `FlowGraph` | Yes | The flow graph to assign levels to. |

**Returns**: `Map<string, number>` - Map of node ID to level index (0-based).

**Throws**: `LayoutError` - If the graph is invalid or contains cycles.

**Error Codes**:

| Code | Condition | Message |
|------|-----------|---------|
| `INVALID_GRAPH` | `graph` is null, undefined, or empty | "Graph must be a valid FlowGraph instance" |
| `CYCLE_DETECTED` | Graph contains cycles | "Graph contains cycles at nodes: {cycleNodes}" |
| `LEVEL_ASSIGNMENT_FAILED` | BFS traversal fails | "Failed to assign levels to all nodes" |

**Preconditions**:
1. `graph` must be a valid `FlowGraph` instance
2. Graph must not contain cycles
3. All nodes must be reachable from root nodes

**Postconditions**:
1. All nodes have a level assignment
2. All children of a node are at level + 1
3. Root nodes are at level 0

**Algorithm Complexity**: O(V + E) where V = number of nodes, E = number of edges

**Example Usage**:
```typescript
const levels = layoutEngine.assignLevels(flowGraph);
console.log(`Node ${nodeId} is at level ${levels.get(nodeId)}`);
```

---

#### Method: `calculatePositions()`

Calculates node positions based on level assignments and configuration.

**Signature**:
```typescript
calculatePositions(
  graph: FlowGraph,
  levels: Map<string, number>,
  config: ILayoutConfig
): ILayoutResult
```

**Parameters**:

| Name | Type | Required | Description |
|------|------|----------|-------------|
| `graph` | `FlowGraph` | Yes | The flow graph with node dimensions. |
| `levels` | `Map<string, number>` | Yes | Map of node ID to level index. |
| `config` | `ILayoutConfig` | Yes | Layout configuration. |

**Returns**: `ILayoutResult` - The calculated layout with node positions.

**Throws**: `LayoutError` - If position calculation fails.

**Error Codes**:

| Code | Condition | Message |
|------|-----------|---------|
| `INVALID_NODE_SIZE` | Node width or height is invalid | "Node {id} has invalid dimensions" |
| `POSITION_CALCULATION_FAILED` | General calculation failure | "Failed to calculate node positions" |

**Preconditions**:
1. `graph.nodes` must have valid `width` and `height` for all nodes
2. `levels` must contain an entry for every node in `graph.nodes`
3. `config.horizontalSpacing` must be > 0

**Postconditions**:
1. All nodes have valid `(x, y)` positions
2. Nodes are centered on their layout coordinates
3. Consecutive levels maintain exactly 50px vertical spacing
4. Same-level nodes are horizontally spaced by `config.horizontalSpacing`

**Example Usage**:
```typescript
const levels = layoutEngine.assignLevels(flowGraph);
const result = layoutEngine.calculatePositions(flowGraph, levels, DEFAULT_LAYOUT_CONFIG);
console.log(`Total width: ${result.totalWidth}, Total height: ${result.totalHeight}`);
```

---

## Type Definitions Contract

### Interface: ILayoutConfig

**Description**: Configuration object for the layout engine.

**Properties**:

| Property | Type | Required | Default | Description |
|----------|------|----------|---------|-------------|
| `verticalSpacing` | `50` | Yes | `50` | Fixed vertical spacing between consecutive levels (immutable per constitution) |
| `horizontalSpacing` | `number` | No | `75` | Horizontal spacing between nodes in the same level |
| `baseYOffset` | `number` | No | `0` | Base Y offset for the entire graph |
| `centerGraph` | `boolean` | No | `true` | Whether to center the graph horizontally |

**Constraints**:
- `verticalSpacing` is a literal type `50` (cannot be changed)
- `horizontalSpacing` must be > 0
- `baseYOffset` must be >= 0

---

### Interface: INodePosition

**Description**: Represents the calculated position of a node.

**Properties**:

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `id` | `string` | Yes | Unique identifier of the node |
| `layoutX` | `number` | Yes | Layout X coordinate (center point) |
| `layoutY` | `number` | Yes | Layout Y coordinate (center point) |
| `x` | `number` | Yes | Final X coordinate (top-left corner) |
| `y` | `number` | Yes | Final Y coordinate (top-left corner) |
| `width` | `number` | Yes | Width of the node |
| `height` | `number` | Yes | Height of the node |
| `level` | `number` | Yes | Level index of the node |

**Constraints**:
- `x === layoutX - width / 2` (centering offset)
- `y === layoutY - height / 2` (centering offset)
- `layoutY === level * 50 + baseOffset` (strict vertical spacing)

---

### Interface: ILayoutResult

**Description**: Output of layout calculation.

**Properties**:

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `nodePositions` | `Map<string, INodePosition>` | Yes | Map of node ID to calculated position |
| `levels` | `INodeLevel[]` | Yes | Array of levels with their base Y coordinates |
| `totalWidth` | `number` | Yes | Total width of the layout (widest level) |
| `totalHeight` | `number` | Yes | Total height of the layout (max level * 50) |
| `maxLevel` | `number` | Yes | Maximum level index in the graph |
| `config` | `ILayoutConfig` | Yes | Configuration used for this layout |

**Constraints**:
- `nodePositions.size === graph.nodes.length`
- `levels.length === maxLevel + 1`
- `totalHeight === (maxLevel + 1) * 50 + baseOffset`

---

### Interface: INodeLevel

**Description**: Represents a horizontal layer in the graph hierarchy.

**Properties**:

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `levelIndex` | `number` | Yes | Zero-based index of the level (0 = root level) |
| `nodes` | `string[]` | Yes | Array of node IDs at this level |
| `baseY` | `number` | Yes | Y-coordinate baseline for this level (center line) |

**Constraints**:
- `levelIndex >= 0`
- `nodes` contains unique node IDs (no duplicates)
- `baseY === levelIndex * 50 + baseOffset`

---

### Interface: ILayoutEngine

**Description**: Public interface for the layout engine service.

**Methods**:

| Method | Signature | Description |
|--------|-----------|-------------|
| `layout()` | `layout(graph: FlowGraph, config?: ILayoutConfig): ILayoutResult` | Calculates layout for the given graph |
| `assignLevels()` | `assignLevels(graph: FlowGraph): Map<string, number>` | Assigns levels to nodes using BFS |
| `calculatePositions()` | `calculatePositions(graph: FlowGraph, levels: Map<string, number>, config: ILayoutConfig): ILayoutResult` | Calculates node positions based on levels and config |

---

### Interface: ILayoutCache

**Description**: Cache interface for storing layout results.

**Methods**:

| Method | Signature | Description |
|--------|-----------|-------------|
| `get()` | `get(key: string): ILayoutResult | undefined` | Retrieves cached result |
| `set()` | `set(key: string, result: ILayoutResult): void` | Stores result in cache |
| `has()` | `has(key: string): boolean` | Checks if key exists in cache |
| `clear()` | `clear(): void` | Clears all cached entries |
| `generateKey()` | `generateKey(graph: FlowGraph): string` | Generates cache key from graph structure |

---

## Error Contract

### Class: LayoutError

**Description**: Custom error type for layout calculation failures.

**Properties**:

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `name` | `'LayoutError'` | Yes | Error type identifier |
| `message` | `string` | Yes | Human-readable error message |
| `code` | `LayoutErrorCode` | Yes | Error code for programmatic handling |
| `cause` | `unknown` | No | Original error that caused this error |

**Methods**:
- Extends `Error` class with stack trace support

---

### Enum: LayoutErrorCode

**Description**: Enumeration of all possible layout error codes.

**Values**:

| Value | Description |
|-------|-------------|
| `INVALID_GRAPH` | Graph structure is invalid |
| `CYCLE_DETECTED` | Graph contains cycles |
| `ORPHAN_NODE` | Node has no incoming or outgoing edges |
| `INVALID_NODE_SIZE` | Node width or height is invalid |
| `LEVEL_ASSIGNMENT_FAILED` | Failed to assign levels to nodes |
| `POSITION_CALCULATION_FAILED` | Failed to calculate node positions |

---

## RxJS Integration Contract

### Observable: Layout Updates

**Description**: Observable stream of layout updates for reactive integration.

**Type**: `Observable<ILayoutResult | null>`

**Behavior**:
- Emits `ILayoutResult` when layout calculation completes
- Emits `null` if graph is null or undefined
- Uses `debounceTime(16)` to prevent excessive recalculations (~60fps)
- Uses `switchMap` to cancel previous layout calculations

**Example Integration**:
```typescript
@Injectable({ providedIn: 'root' })
export class EditorService {
  private graphSubject = new BehaviorSubject<FlowGraph | null>(null);
  private layoutEngine = inject(FlowLayoutEngine);
  
  constructor() {
    this.graphSubject.pipe(
      debounceTime(16), // ~60fps
      switchMap(graph => {
        if (!graph) return of(null);
        return from(this.layoutEngine.layout(graph)).pipe(
          catchError(error => {
            console.error('Layout failed:', error);
            return of(null);
          })
        );
      })
    ).subscribe(result => {
      if (result) {
        this.applyLayout(result);
      }
    });
  }
  
  public triggerLayout(graph: FlowGraph): void {
    this.graphSubject.next(graph);
  }
}
```

---

## Contract Compliance Checklist

### Type Safety (Principle 1)

- ✅ All interfaces have explicit type definitions
- ✅ No `any` types in public API
- ✅ Generic types used where appropriate (e.g., `Map<string, INodePosition>`)
- ✅ Literal types for fixed values (e.g., `verticalSpacing: 50`)

### Component Architecture (Principle 2)

- ✅ Service is provided as singleton via `providedIn: 'root'`
- ✅ Public API is defined in interface (`ILayoutEngine`)
- ✅ Service follows Angular dependency injection patterns

### Layout Performance (Principle 3)

- ✅ Vertical spacing is enforced as literal type `50`
- ✅ Node centering offsets are clearly documented
- ✅ Performance constraints are specified (O(V + E) complexity)

### Code Consistency (Principle 4)

- ✅ All interfaces use `I` prefix
- ✅ Methods have TSDoc comments
- ✅ Error codes are defined in enum

### RxJS Patterns (Principle 6)

- ✅ RxJS integration pattern is documented
- ✅ Observable types are explicitly defined
- ✅ Operators (`debounceTime`, `switchMap`) are specified

---

## Versioning Policy

This contract follows semantic versioning (MAJOR.MINOR.PATCH):

- **MAJOR**: Breaking changes to public API (e.g., removed methods, changed signatures)
- **MINOR**: New features added without breaking existing API
- **PATCH**: Bug fixes, documentation updates, non-breaking changes

**Current Version**: 1.0.0

---

## Testing Contract

### Unit Test Coverage

All public methods MUST have unit tests with the following coverage:

| Method | Coverage Requirement |
|--------|---------------------|
| `layout()` | 95% including error paths |
| `assignLevels()` | 95% including edge cases |
| `calculatePositions()` | 95% including edge cases |
| Error handling | 100% (all error codes tested) |

### Integration Test Coverage

- End-to-end layout with `EditorService`
- RxJS pipeline (debounce, switchMap, caching)
- Integration with `BranchGroupManager`

---

## Migration Guide

### From DagreLayout to FlowLayoutEngine

**Before** (using DagreLayout):
```typescript
const dagreLayout = new DagreLayout({
  type: 'dagre',
  rankdir: 'TB',
  ranksep: 35, // ❌ Violates 50px rule
  nodesep: 75,
});
const layoutedData = dagreLayout.layout(flowGraph);
```

**After** (using FlowLayoutEngine):
```typescript
const layoutEngine = new FlowLayoutEngine();
const result = layoutEngine.layout(flowGraph, {
  verticalSpacing: 50, // ✅ Enforces 50px rule
  horizontalSpacing: 75,
  baseYOffset: 0,
  centerGraph: true,
});
```

**Key Differences**:
1. `ranksep` parameter removed (fixed at 50px)
2. Return type changed from generic object to `ILayoutResult`
3. Explicit error handling with `LayoutError`
4. Type-safe interfaces for all data structures

---

## Deprecation Policy

**Current Status**: No deprecated APIs

**Future Deprecation Process**:
1. Mark API as deprecated in TSDoc comments
2. Add migration guide to documentation
3. Maintain backward compatibility for at least one minor version
4. Remove deprecated API in next MAJOR version

---

## Support and Contact

For questions or issues related to this contract:
- **Feature ID**: 002-fixed-vertical-spacing
- **Maintainer**: ApFlow.UI Development Team
- **Document Version**: 1.0.0
- **Last Updated**: 2026-03-04

---

**End of Interface Contract**
