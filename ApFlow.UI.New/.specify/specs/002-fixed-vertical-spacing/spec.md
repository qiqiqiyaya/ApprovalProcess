# Feature Specification: Fixed Vertical Spacing for Flow Graph Layout

## Metadata

- **Feature ID**: 002-fixed-vertical-spacing
- **Version**: 1.0.0
- **Created**: 2026-03-04
- **Status**: Draft
- **Priority**: High
- **Author**: System
- **Branch**: 002-fixed-vertical-spacing

---

## Overview

This feature addresses a critical visual inconsistency in the flow graph layout where the vertical spacing between nodes varies based on node heights. The current implementation uses `DagreLayout` with a `ranksep` parameter that does not enforce fixed spacing. This feature ensures that all nodes maintain a consistent 50px vertical spacing regardless of their individual heights, improving visual hierarchy and user experience.

### Problem Statement

Users currently experience irregular vertical spacing in flow graphs where:
- Large nodes (e.g., container nodes) create wider gaps than small nodes
- The visual flow becomes difficult to follow due to inconsistent spacing
- The layout does not meet the project's strict 50px vertical spacing requirement defined in the constitution

### Solution Summary

Replace the current `DagreLayout` implementation with a custom layout engine that enforces fixed 50px vertical spacing between all consecutive node levels, regardless of node dimensions.

---

## User Scenarios & Testing

### Primary User Flow

1. **User opens a flow graph editor**
   - User navigates to the flow graph page
   - Flow graph loads with approval nodes displayed
   - **Expected**: All nodes are displayed with uniform vertical spacing of 50px between consecutive levels

2. **User adds new nodes of varying sizes**
   - User adds a small action node (height: 40px)
   - User adds a large container node (height: 120px)
   - **Expected**: The vertical spacing between the small node and its parent is 50px; the vertical spacing between the large node and its parent is also 50px

3. **User renders the flow graph**
   - User triggers graph rendering
   - Layout engine calculates positions
   - **Expected**: Node Y-coordinates are calculated as `y = levelIndex * 50 + baseOffset`, ensuring consistent spacing

4. **User views parallel branches**
   - User creates parallel approval branches
   - All nodes in parallel branches are at the same level
   - **Expected**: Vertical spacing is maintained at 50px for all parallel branch nodes relative to their parent level

### Edge Cases & Negative Flows

1. **Nodes with extreme height variations**
   - Node A: height 20px, Node B: height 200px
   - **Expected**: Vertical spacing between A's level and B's level remains 50px

2. **Empty levels (gaps in hierarchy)**
   - User deletes intermediate nodes creating gaps
   - **Expected**: Remaining levels maintain 50px spacing without collapsing

3. **Single-level graphs**
   - Graph with only start and end nodes
   - **Expected**: Both nodes are positioned with correct horizontal layout; vertical spacing rule still applies (no vertical displacement if same level)

4. **Deeply nested hierarchies**
   - Graph with 10+ levels
   - **Expected**: Every consecutive level pair maintains exactly 50px vertical spacing

### Performance Scenarios

1. **Large graphs with 100+ nodes**
   - **Expected**: Layout calculation completes in under 100ms
   - **Expected**: Graph renders smoothly without UI freezing

2. **Rapid consecutive layout updates**
   - User adds/deletes multiple nodes quickly
   - **Expected**: Debounce mechanism (16ms) prevents excessive recalculations
   - **Expected**: Only the latest layout request is processed

---

## Functional Requirements

### FR-1: Fixed Vertical Spacing Enforcement
- **MUST**: Calculate node Y-coordinates using the formula `y = levelIndex * 50 + baseOffset`
- **MUST**: Ensure vertical spacing between consecutive levels is exactly 50px
- **MUST NOT**: Use `ranksep` or similar parameters that vary spacing based on node dimensions
- **MUST**: Apply this rule regardless of individual node heights (small, medium, or large nodes)

### FR-2: Level Assignment Algorithm
- **MUST**: Assign levels to nodes using breadth-first search (BFS) starting from root node
- **MUST**: Ensure all children of a node are assigned to level + 1
- **MUST**: Handle parallel branches by placing all branch nodes at the same level relative to their parent

### FR-3: Node Position Calculation
- **MUST**: Center nodes on their layout coordinates
- **MUST**: Apply centering offset: `finalX = layoutX - nodeWidth / 2`, `finalY = layoutY - nodeHeight / 2`
- **MUST**: Calculate horizontal spacing (nodesep) with default value of 75px

### FR-4: Layout Engine Replacement
- **MUST**: Replace `DagreLayout` with custom layout engine implementing fixed vertical spacing
- **MUST**: Implement custom layout engine as a separate service module
- **MUST**: Maintain backward compatibility with existing graph structure and data models

### FR-5: Performance Optimization
- **MUST**: Implement debounce mechanism with 16ms threshold to prevent excessive recalculations
- **MUST**: Use `switchMap` to cancel previous layout calculations when new requests arrive
- **MUST**: Cache layout results for identical graph configurations
- **MUST**: Achieve sub-100ms layout calculation time for graphs with up to 100 nodes

### FR-6: Visual Consistency
- **MUST**: Ensure all nodes at the same level have the same baseline Y-coordinate
- **MUST**: Maintain consistent vertical spacing throughout the entire graph lifecycle (initial render, updates, deletions)

### FR-7: Error Handling
- **MUST**: Provide user-friendly error message if layout calculation fails
- **MUST**: Gracefully handle cycles in graph (if detected, notify user instead of crashing)
- **MUST**: Validate graph structure before layout calculation

---

## Success Criteria

### Quantitative Metrics

1. **Visual Accuracy**
   - 100% of consecutive level pairs maintain exactly 50px vertical spacing (measured in pixels)
   - Zero variance in vertical spacing regardless of node height variations

2. **Performance Targets**
   - Layout calculation time: < 100ms for graphs with up to 100 nodes
   - Rendering performance: Maintains 60fps during graph updates
   - Debounce effectiveness: Reduces redundant calculations by at least 90%

3. **Test Coverage**
   - 95% code coverage for layout engine implementation
   - All edge case scenarios included in test suite

### Qualitative Outcomes

1. **User Experience**
   - Users can predictably understand flow progression without visual distraction
   - Graph hierarchy is immediately apparent due to consistent spacing
   - Visual inspection confirms uniform spacing across all levels

2. **Maintainability**
   - Custom layout engine is modular and extensible for future layout requirements
   - Code follows project constitution principles (TypeScript strict mode, type safety)
   - Layout algorithm is well-documented with clear separation of concerns

3. **Compliance**
   - Implementation fully complies with Project Constitution Principle 3 (Layout Performance Optimization)
   - No violations of the strict 50px vertical spacing rule

---

## Key Entities

| Entity | Description | Key Attributes |
|--------|-------------|----------------|
| **Node** | Represents a single element in the flow graph (e.g., approval node, action node) | id, width, height, position (x, y), level |
| **Level** | Represents a horizontal layer in the graph hierarchy | levelIndex, nodes[], baseY |
| **Edge** | Represents a connection between two nodes | sourceNodeId, targetNodeId, type |
| **LayoutEngine** | Custom service implementing fixed vertical spacing algorithm | calculateLayout(), calculateNodeY(), assignLevels() |
| **Graph** | Complete flow graph containing all nodes and edges | nodes[], edges[], rootNode |
| **LayoutResult** | Output of layout calculation containing node positions | nodePositions[], totalWidth, totalHeight |

---

## Assumptions & Constraints

### Assumptions

1. The flow graph is a directed acyclic graph (DAG) without cycles
2. All nodes have defined width and height attributes
3. The graph structure (nodes and edges) is valid before layout calculation
4. The existing `FlowGraph` and `FlowNode` models remain compatible with new layout engine
5. RxJS is available for reactive patterns (`BehaviorSubject`, `switchMap`, `debounceTime`)

### Constraints

1. **Technical Constraints**
   - Must use TypeScript strict mode
   - Must integrate with AntV X6 graph library
   - Must follow Angular 19+ component architecture
   - Must not break existing graph rendering functionality

2. **Performance Constraints**
   - Layout calculation must complete in < 100ms for 100-node graphs
   - Must not block the UI thread during layout computation
   - Memory usage must remain reasonable for large graphs

3. **Compatibility Constraints**
   - Must work with existing node types (info nodes, action nodes, container nodes)
   - Must maintain compatibility with branch group manager
   - Must preserve existing event subscriptions and graph interaction behaviors

---

## Out of Scope

The following items are explicitly excluded from this feature:

- **Horizontal spacing customization**: While horizontal spacing (nodesep) defaults to 75px, making it user-configurable is out of scope
- **Node size normalization**: This feature does not change node sizes; it only enforces spacing between nodes of any size
- **Animation of layout changes**: Smooth transitions during layout updates are not required (can be added as a future enhancement)
- **User-configurable vertical spacing**: The 50px spacing is fixed per project constitution and not user-configurable
- **Cycle detection and resolution**: The algorithm assumes a DAG; cycle handling is out of scope (cycles should be prevented at the graph editing stage)
- **Edge routing optimization**: Focus is on node positioning; edge routing (orthogonal, curved, etc.) is not modified

---

## Dependencies

### Internal Dependencies

- **Project Constitution Principle 3**: Defines the strict 50px vertical spacing requirement
- **EditorService**: Existing service managing graph rendering and node selection
- **FlowGraph Model**: Data model representing the graph structure
- **FlowNode Model**: Data model representing individual nodes
- **BranchGroupManager**: Manages parallel branch grouping

### External Dependencies

- **AntV X6**: Graph rendering library (version 3.1+)
- **@antv/layout**: Layout library (version 0.3+) - used for reference, will be extended or replaced
- **RxJS**: Reactive programming library for performance optimizations
- **Angular 19+**: Component framework
- **TypeScript 5.7+**: Programming language with strict mode

---

## Risks & Mitigations

| Risk | Likelihood | Impact | Mitigation Strategy |
|------|------------|--------|-------------------|
| Performance degradation with large graphs | Medium | High | Implement layout result caching, debounce mechanism, and consider Web Worker for very large graphs |
| Incorrect level assignment for complex graphs | Medium | High | Thorough unit testing with various graph topologies (linear, parallel, nested parallel) |
| Breaking changes to existing graph interactions | Low | High | Maintain backward compatibility with event subscriptions and node selection logic |
| Regression in node positioning accuracy | Low | Medium | Visual regression testing to compare before/after layouts |
| Integration issues with existing BranchGroupManager | Low | Medium | Review and test integration with branch group manager for parallel branch handling |

---

## Related Work

- **Constitution Amendment 1.1.0**: Updated Principle 3 to enforce strict 50px vertical spacing
- **Current Implementation**: `editor.service.ts:61-92` uses `DagreLayout` with `ranksep: 35` (identified as non-compliant)
- **Branch**: `001-uniform-vertical-spacing`: Current branch where the issue was identified

---

## Glossary

| Term | Definition |
|------|------------|
| **DAG** | Directed Acyclic Graph - a graph with directed edges and no cycles |
| **Level** | Horizontal layer in a hierarchical graph layout; all nodes in a level share the same Y-coordinate baseline |
| **Vertical Spacing** | Distance between the baseline Y-coordinates of consecutive levels |
| **Horizontal Spacing (nodesep)** | Distance between adjacent nodes within the same level |
| **Layout Engine** | Algorithm or service that calculates optimal positions for all nodes in a graph |
| **Sugiyama Algorithm** | A hierarchical graph layout algorithm consisting of three phases: level assignment, node ordering, and position calculation |
| **BFS (Breadth-First Search)** | Graph traversal algorithm used to assign levels by visiting nodes layer by layer |
| **Debounce** | Technique to delay processing until input stabilizes, used here to prevent excessive layout recalculations |
| **SwitchMap** | RxJS operator that cancels previous observable when new one arrives, used to cancel outdated layout calculations |

---

**End of Specification**
