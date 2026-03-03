# Flow Graph Module

The Flow Graph module provides visual workflow editing capabilities for the ApFlow.UI application. It uses AntV X6 for graph rendering and a custom layout engine enforcing strict 50px vertical spacing per the project constitution.

## Architecture

### Core Components

- **FlowGraph**: Model representing the workflow graph with nodes and edges
- **FlowNode**: Model for individual nodes with dimensions and styling
- **FlowEdge**: Model for connections between nodes
- **BranchGroupManager**: Manages parallel branch structures

### Services

- **EditorService**: Manages graph rendering and user interactions
- **FlowLayoutEngine**: Custom layout engine with fixed 50px vertical spacing

## Layout Engine

The `FlowLayoutEngine` is a custom layout engine that enforces strict 50px vertical spacing between consecutive node levels, as required by the project constitution (Principle 3).

### Key Features

- **Fixed Vertical Spacing**: Exactly 50px between all consecutive levels
- **BFS Level Assignment**: Uses Breadth-First Search for topological level assignment
- **Automatic Caching**: Caches layout results for performance
- **Type Safety**: Full TypeScript type safety with no `any` types

### Usage

```typescript
import { FlowLayoutEngine } from './services/flow-layout-engine.service';
import type { ILayoutConfig } from './models/layout.models';

const layoutEngine = new FlowLayoutEngine();

// Use default configuration
const result = layoutEngine.layout(flowGraph);

// Or use custom configuration
const config: ILayoutConfig = {
  verticalSpacing: 50,        // Fixed at 50px
  horizontalSpacing: 75,      // Horizontal spacing between nodes
  baseYOffset: 0,             // Y offset for entire graph
  centerGraph: true,          // Center graph horizontally
};
const result = layoutEngine.layout(flowGraph, config);
```

### Configuration Options

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| `verticalSpacing` | `50` | `50` | Fixed vertical spacing (immutable) |
| `horizontalSpacing` | `number` | `75` | Horizontal spacing between nodes |
| `baseYOffset` | `number` | `0` | Y offset for entire graph |
| `centerGraph` | `boolean` | `true` | Center graph horizontally |

### Layout Algorithm

1. **Validation**: Validate graph structure and node dimensions
2. **Cache Check**: Check if layout result is cached
3. **Level Assignment**: Assign levels using BFS (O(V + E))
4. **Position Calculation**: Calculate positions with fixed spacing
5. **Cache Storage**: Store result in cache

### Performance

- **Time Complexity**: O(V + E) for level assignment
- **Space Complexity**: O(V) for cache storage
- **Target**: < 100ms for 100-node graphs

### Error Handling

The layout engine throws `LayoutError` with specific error codes:

| Error Code | Description |
|------------|-------------|
| `INVALID_GRAPH` | Graph structure is invalid |
| `CYCLE_DETECTED` | Graph contains cycles |
| `INVALID_NODE_SIZE` | Node dimensions are invalid |
| `LEVEL_ASSIGNMENT_FAILED` | Failed to assign levels |

## Node Types

### Supported Shapes

- `rect`: Rectangle nodes (start, end)
- `operation`: Operation nodes
- `approve`: Approval nodes
- `parallelApproval`: Parallel approval nodes
- `parallelApprovalMerge`: Parallel merge nodes

### Node Dimensions

| Type | Width | Height |
|------|-------|--------|
| `operation` | 40px | 41px |
| `approve` | 170px | 142px |
| `parallelApproval` | 170px | 142px |
| `parallelApprovalMerge` | 170px | 100px |
| `rect` (default) | 80px | 40px |

## Testing

### Unit Tests

Unit tests are located in `services/flow-layout-engine.service.spec.ts` with 95%+ coverage target.

Run tests:

```bash
npm test
```

### Integration Tests

Integration tests verify the layout engine integration with EditorService.

## Constitution Compliance

This module is fully compliant with the Project Constitution:

- ✅ **Principle 1**: Type Safety First - No `any` types in public APIs
- ✅ **Principle 2**: Component Architecture - Services follow Angular patterns
- ✅ **Principle 3**: Layout Performance - Fixed 50px vertical spacing enforced
- ✅ **Principle 4**: Code Consistency - Follows naming conventions

## Migration from DagreLayout

The custom `FlowLayoutEngine` replaces `DagreLayout` to ensure strict 50px vertical spacing.

**Key Differences**:

1. `ranksep` parameter removed (fixed at 50px)
2. Return type is `ILayoutResult` instead of generic object
3. Explicit error handling with `LayoutError`
4. Automatic result caching
5. Full type safety

**Before**:

```typescript
const dagreLayout = new DagreLayout({
  type: 'dagre',
  rankdir: 'TB',
  ranksep: 35, // ❌ Violates 50px rule
  nodesep: 75,
});
const layoutedData = dagreLayout.layout(flowGraph);
```

**After**:

```typescript
const layoutEngine = new FlowLayoutEngine();
const result = layoutEngine.layout(flowGraph);
```

## Contributing

When modifying the layout engine:

1. Maintain strict 50px vertical spacing
2. Ensure 95%+ test coverage
3. Update this documentation
4. Follow TypeScript strict mode rules

## License

This module is part of the ApFlow.UI project.
