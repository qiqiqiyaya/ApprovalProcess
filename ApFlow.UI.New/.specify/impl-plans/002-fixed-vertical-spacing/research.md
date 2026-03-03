# Research: Fixed Vertical Spacing Layout Engine

**Feature ID**: 002-fixed-vertical-spacing  
**Phase**: Phase 0 - Outline & Research  
**Date**: 2026-03-04

---

## Research Overview

This document consolidates research findings and decisions for implementing a custom layout engine that enforces strict 50px vertical spacing in the ApFlow.UI flow graph editor.

---

## Research Tasks & Decisions

### 1. Custom Layout Engine Architecture

**Question**: How to structure the custom layout engine to integrate with AntV X6 while maintaining type safety and modularity?

**Decision**: Create a standalone `FlowLayoutEngine` service that extends or mimics `@antv/layout.BaseLayout` interface.

**Rationale**:
- AntV X6's `graph.layout()` method accepts any object with a `layout()` method
- Creating a separate service aligns with Angular's service-based architecture (Principle 2)
- Enables independent testing and maintenance
- Allows for future extensibility (e.g., user-configurable presets)

**Alternatives Considered**:
- **Extend `@antv/layout.BaseLayout`**: Rejected because the library's TypeScript types may not be stable or compatible with strict mode
- **Modify `editor.service.ts` directly**: Rejected due to violation of single responsibility principle and testing concerns
- **Use DagreLayout with custom post-processing**: Rejected because DagreLayout's internal rank assignment logic cannot be easily overridden for strict spacing

**Implementation Approach**:
```typescript
@Injectable({ providedIn: 'root' })
export class FlowLayoutEngine {
  private readonly VERTICAL_SPACING = 50; // 50px per constitution
  private readonly HORIZONTAL_SPACING = 75; // Default nodesep
  
  layout(graph: FlowGraph): LayoutResult {
    // Phase 1: Assign levels using BFS
    // Phase 2: Calculate positions
    // Phase 3: Apply centering offsets
  }
}
```

---

### 2. Level Assignment Algorithm

**Question**: Which algorithm should be used to assign levels to nodes while handling parallel branches?

**Decision**: Implement Breadth-First Search (BFS) starting from the root node with special handling for parallel branches.

**Rationale**:
- BFS naturally assigns levels by visiting nodes layer by layer
- Guarantees that all children of a node are at level + 1
- Handles parallel branches correctly by treating branch groups as single logical units during level assignment
- Time complexity: O(V + E) where V = nodes, E = edges

**Alternatives Considered**:
- **Depth-First Search (DFS)**: Rejected because DFS does not guarantee correct level assignment for wide graphs
- **DagreLayout's rank assignment**: Rejected because it considers node dimensions when computing ranks, violating strict spacing requirement
- **Longest Path Algorithm**: Rejected because it may assign unnecessary levels for short paths

**Algorithm Details**:
1. Identify root node(s) - nodes with no incoming edges
2. Initialize queue with root nodes at level 0
3. For each node dequeued:
   - Assign current level to node
   - Enqueue all direct children with level + 1
   - Handle parallel branches via `BranchGroupManager`
4. Use `visited` set to avoid re-processing nodes

**Pseudocode**:
```typescript
function assignLevels(graph: FlowGraph): Map<string, number> {
  const levels = new Map<string, number>();
  const visited = new Set<string>();
  const queue: Array<{ nodeId: string; level: number }> = [];
  
  // Find roots (nodes with no incoming edges)
  const roots = findRootNodes(graph);
  roots.forEach(root => {
    queue.push({ nodeId: root.id, level: 0 });
  });
  
  while (queue.length > 0) {
    const { nodeId, level } = queue.shift()!;
    
    if (visited.has(nodeId)) continue;
    visited.add(nodeId);
    levels.set(nodeId, level);
    
    // Get children
    const children = getChildren(nodeId, graph);
    children.forEach(child => {
      if (!visited.has(child.id)) {
        queue.push({ nodeId: child.id, level: level + 1 });
      }
    });
  }
  
  return levels;
}
```

---

### 3. Horizontal Layout Strategy

**Question**: How to calculate horizontal positions (X-coordinates) for nodes while maintaining visual balance?

**Decision**: Use a simple greedy approach with centering offsets, grouped by level.

**Rationale**:
- Simpler than complex crossing minimization algorithms (e.g., barycenter heuristic)
- Sufficient for typical approval flow graphs (limited width per level)
- Can be enhanced later if edge crossings become problematic
- Aligns with current DagreLayout's nodesep: 75 behavior

**Alternatives Considered**:
- **Barycenter heuristic (from Sugiyama algorithm)**: Rejected as overkill for current use case; adds complexity without clear benefit
- **Force-directed layout**: Rejected because it doesn't respect strict vertical spacing
- **User-defined positions**: Out of scope per specification

**Algorithm Details**:
1. Group nodes by level
2. For each level:
   - Calculate total width: `sum(nodeWidth + HORIZONTAL_SPACING)`
   - Calculate starting X position to center the level: `(canvasWidth - totalWidth) / 2`
   - Assign X positions from left to right with spacing between nodes
3. Apply centering offset: `finalX = layoutX - nodeWidth / 2`

---

### 4. RxJS Performance Optimization Pattern

**Question**: How to implement debounce and cancellation for layout calculations using RxJS?

**Decision**: Use `BehaviorSubject` + `debounceTime(16)` + `switchMap` pattern.

**Rationale**:
- `debounceTime(16)` ≈ 60fps, prevents excessive recalculations during rapid user input
- `switchMap` automatically cancels previous layout calculations, ensuring only the latest result is applied
- `BehaviorSubject` maintains the current graph state and triggers recalculations
- Aligns with Principle 6 (RxJS Reactive Patterns)

**Alternatives Considered**:
- **Manual debouncing with setTimeout**: Rejected because it's not type-safe and harder to test
- **debounceTime(100) with distinctUntilChanged**: Too slow for responsive UI
- **No debouncing**: Rejected because rapid node additions/deletions would cause excessive recalculations

**Implementation Pattern**:
```typescript
@Injectable({ providedIn: 'root' })
export class EditorService {
  private graphSubject = new BehaviorSubject<FlowGraph | null>(null);
  private layoutCache = new Map<string, LayoutResult>();
  
  constructor(private layoutEngine: FlowLayoutEngine) {
    this.graphSubject.pipe(
      debounceTime(16), // ~60fps
      switchMap(graph => {
        if (!graph) return of(null);
        const cacheKey = this.generateCacheKey(graph);
        if (this.layoutCache.has(cacheKey)) {
          return of(this.layoutCache.get(cacheKey)!);
        }
        return from(layoutEngine.layout(graph)).pipe(
          tap(result => this.layoutCache.set(cacheKey, result))
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

### 5. TypeScript Type Safety for Layout Engine

**Question**: How to ensure strict type safety for the layout engine interface and data structures?

**Decision**: Define explicit interfaces for all layout-related types with no `any` types.

**Rationale**:
- Aligns with Principle 1 (Type Safety First)
- Enables compile-time error detection
- Improves IDE autocomplete and documentation
- Prevents runtime errors from mismatched data structures

**Type Definitions**:
```typescript
/**
 * Represents the position of a node in the layout
 */
export interface INodePosition {
  /** Node ID */
  id: string;
  /** Layout X coordinate (center point) */
  layoutX: number;
  /** Layout Y coordinate (center point) */
  layoutY: number;
  /** Final X coordinate (top-left corner) */
  x: number;
  /** Final Y coordinate (top-left corner) */
  y: number;
}

/**
 * Result of layout calculation
 */
export interface ILayoutResult {
  /** Calculated positions for all nodes */
  nodePositions: Map<string, INodePosition>;
  /** Total width of the layout */
  totalWidth: number;
  /** Total height of the layout */
  totalHeight: number;
  /** Maximum level in the graph */
  maxLevel: number;
}

/**
 * Configuration for layout engine
 */
export interface ILayoutConfig {
  /** Vertical spacing between levels (fixed at 50px per constitution) */
  readonly verticalSpacing: 50;
  /** Horizontal spacing between nodes in the same level */
  horizontalSpacing?: number;
  /** Base Y offset for the graph */
  baseYOffset?: number;
}

/**
 * Layout engine interface
 */
export interface ILayoutEngine {
  /**
   * Calculates layout for the given graph
   * @param graph The flow graph to layout
   * @param config Optional configuration
   * @returns Layout result with node positions
   */
  layout(graph: FlowGraph, config?: ILayoutConfig): ILayoutResult;
}
```

---

### 6. Integration with BranchGroupManager

**Question**: How should the custom layout engine integrate with the existing `BranchGroupManager`?

**Decision**: Query `BranchGroupManager` during level assignment to treat parallel branch groups as logical units.

**Rationale**:
- `BranchGroupManager` already tracks parallel branch structure
- Treating branch groups as units ensures all nodes in a branch are at the same level
- Minimal code changes to existing functionality
- Maintains backward compatibility

**Integration Points**:
1. During level assignment, check if a node is part of a branch group
2. If a node is a parallel approval node, assign the same level to all nodes in its branches
3. If a node is a merge node, assign the level after the last node in the parallel branches

**Pseudocode**:
```typescript
function assignLevels(graph: FlowGraph, branchManager: BranchGroupManager): Map<string, number> {
  const levels = new Map<string, number>();
  const visited = new Set<string>();
  const queue: Array<{ nodeId: string; level: number }> = [];
  
  const roots = findRootNodes(graph);
  roots.forEach(root => {
    queue.push({ nodeId: root.id, level: 0 });
  });
  
  while (queue.length > 0) {
    const { nodeId, level } = queue.shift()!;
    
    if (visited.has(nodeId)) continue;
    visited.add(nodeId);
    
    // Check if node is part of a parallel branch group
    const branchGroup = branchManager.getBranchGroupByNodeId(nodeId);
    if (branchGroup) {
      // Assign same level to all nodes in the branch
      branchGroup.branches.forEach(branch => {
        branch.nodeIds.forEach(branchNodeId => {
          if (!visited.has(branchNodeId)) {
            visited.add(branchNodeId);
            levels.set(branchNodeId, level);
          }
        });
      });
      
      // Add merge node to next level
      const mergeNodeId = branchGroup.mergeNodeId;
      if (mergeNodeId && !visited.has(mergeNodeId)) {
        queue.push({ nodeId: mergeNodeId, level: level + 1 });
      }
    } else {
      levels.set(nodeId, level);
      const children = getChildren(nodeId, graph);
      children.forEach(child => {
        if (!visited.has(child.id)) {
          queue.push({ nodeId: child.id, level: level + 1 });
        }
      });
    }
  }
  
  return levels;
}
```

---

### 7. Performance Optimization: Caching Strategy

**Question**: How to implement effective caching to avoid redundant layout calculations?

**Decision**: Use a `Map<string, ILayoutResult>` cache keyed by a hash of the graph structure.

**Rationale**:
- Graph layout is deterministic: same input → same output
- Cache eliminates redundant calculations for unchanged graphs
- Simple to implement with TypeScript's `Map` type
- Can be combined with RxJS `distinctUntilChanged` for further optimization

**Cache Key Generation**:
```typescript
private generateCacheKey(graph: FlowGraph): string {
  const nodeIds = graph.nodes.map(n => n.id).sort().join(',');
  const edgePairs = graph.edges
    .map(e => `${e.source}-${e.target}`)
    .sort()
    .join('|');
  return `${nodeIds}|${edgePairs}`;
}
```

**Cache Invalidation**:
- Automatically invalidated when graph structure changes (new key generated)
- Can be manually cleared via a public method if needed
- Consider LRU cache for memory-constrained environments (optional future enhancement)

---

### 8. Error Handling and Validation

**Question**: How should the layout engine handle errors and invalid graph structures?

**Decision**: Implement defensive programming with clear error messages and graceful degradation.

**Rationale**:
- Users should see actionable error messages, not cryptic stack traces
- Graceful degradation prevents UI crashes
- Validation catches errors early in the development cycle

**Error Handling Strategy**:
1. **Input Validation**: Validate graph structure before layout calculation
   - Check for null/undefined graph
   - Check for empty nodes/edges arrays
   - Check for orphan nodes (no incoming or outgoing edges)

2. **Cycle Detection**: Detect cycles and throw descriptive error
   ```typescript
   if (hasCycle(graph)) {
     throw new Error('Graph contains cycles. Layout requires a Directed Acyclic Graph (DAG).');
   }
   ```

3. **Error Types**: Define custom error types for different failure modes
   ```typescript
   export class LayoutError extends Error {
     constructor(message: string, public readonly code: string) {
       super(message);
       this.name = 'LayoutError';
     }
   }
   ```

4. **User-Friendly Messages**: Display errors via ng-zorro-antd's notification service

---

### 9. Testing Strategy

**Question**: How to ensure the layout engine is thoroughly tested?

**Decision**: Implement a comprehensive test suite with unit tests, integration tests, and visual regression tests.

**Rationale**:
- Layout algorithms are complex and error-prone
- Visual correctness is critical for user experience
- Test coverage provides confidence for refactoring

**Test Categories**:
1. **Unit Tests**:
   - `assignLevels()` with various graph topologies
   - `calculatePositions()` with different node sizes
   - Cache key generation and invalidation
   - Edge case handling (single node, empty graph, cycles)

2. **Integration Tests**:
   - End-to-end layout with `BranchGroupManager`
   - RxJS pipeline (debounce, switchMap, caching)
   - Integration with `EditorService` and X6 Graph

3. **Visual Regression Tests**:
   - Compare rendered output before/after changes
   - Test with different node types (small, large, container)
   - Verify 50px vertical spacing with pixel-perfect assertions

**Test Coverage Target**: 95% per Success Criteria

---

### 10. Backward Compatibility

**Question**: How to ensure the custom layout engine doesn't break existing functionality?

**Decision**: Maintain the same public API as `DagreLayout` and preserve event subscriptions.

**Rationale**:
- Existing components depend on `EditorService.renderGraph()`
- Graph interactions (selection, hover) must continue to work
- Migration should be seamless for end users

**Compatibility Checklist**:
- ✅ `EditorService.renderGraph()` signature unchanged
- ✅ `graph.fromJSON()` input format unchanged
- ✅ Node event subscriptions preserved
- ✅ Branch group manager integration maintained
- ✅ Existing node types (info, action, container) supported

---

## Summary of Key Decisions

| # | Decision | Rationale |
|---|----------|-----------|
| 1 | Create standalone `FlowLayoutEngine` service | Modular, testable, follows Angular architecture |
| 2 | Use BFS for level assignment | Simple, correct, handles parallel branches |
| 3 | Use greedy horizontal layout | Sufficient for typical flows, maintainable |
| 4 | RxJS: `debounceTime(16)` + `switchMap` | Performance + responsiveness |
| 5 | Strict TypeScript interfaces | Type safety per Principle 1 |
| 6 | Integrate with `BranchGroupManager` | Minimal changes, parallel support |
| 7 | Implement result caching | Avoid redundant calculations |
| 8 | Defensive error handling | User-friendly, graceful degradation |
| 9 | Comprehensive test suite | 95% coverage target |
| 10 | Maintain backward compatibility | Seamless migration |

---

## Unresolved Questions (NEEDS CLARIFICATION)

None. All research tasks completed.

---

## Next Steps

Proceed to **Phase 1: Design & Contracts** to generate:
- `data-model.md` - Entity definitions and relationships
- `/contracts/` - Interface contracts for layout engine
- `quickstart.md` - Developer quick start guide
- Update agent context with new technology stack

---

**End of Research Document**
