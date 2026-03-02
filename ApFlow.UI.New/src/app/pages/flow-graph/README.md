# Flow Graph Module

## Overview

The Flow Graph module provides a visual editor for creating and managing approval process workflows using AntV X6 graph library.

## Features

### 1. Uniform Vertical Spacing Layout

The module implements a custom layout engine that ensures uniform vertical spacing between all flow graph nodes, regardless of node sizes or branching complexity.

#### Key Characteristics

- **Guaranteed Uniform Spacing**: All adjacent nodes maintain consistent vertical distance (configurable, default: 50px)
- **Parallel Branch Support**: Handles parallel branches with proper alignment
- **Nested Branch Support**: Correctly layouts nested parallel branches
- **Visual Balance**: Uses barycenter heuristic to minimize edge crossings
- **Configurable**: Users can adjust spacing parameters through UI controls

#### User Stories

1. **Display Uniform Vertical Spacing (US1 - MVP)**
   - Basic layout with hardcoded 50px spacing
   - Supports simple linear flows

2. **Maintain Visual Balance With Different Node Sizes (US2)**
   - Handles mixed node sizes (small: 80×40, medium: 100×50, large: 200×100)
   - Supports parallel and nested branches
   - Performance optimized for 100+ nodes

3. **Configurable Spacing Distance (US3)**
   - UI controls for spacing configuration
   - Layout presets (Compact, Standard, Spacious)
   - LocalStorage persistence for user preferences

## Architecture

### Components

```
flow-graph/
├── components/
│   ├── editor/              # Main graph editor component
│   │   ├── editor.component.ts
│   │   ├── editor.component.html
│   │   └── editor.component.css
│   ├── layout-config/       # Layout configuration UI
│   │   ├── layout-config.component.ts
│   │   ├── layout-config.component.html
│   │   └── layout-config.component.scss
│   └── nodes/              # Custom node definitions
│       └── node-register.ts
├── models/
│   ├── flow-graph.ts        # Graph model and state management
│   ├── flow-node.ts         # Node model
│   ├── flow-edge.ts         # Edge model
│   ├── flow-group.ts        # Branch group model
│   ├── branch-group-manager.ts # Branch group management
│   ├── layout-config.ts     # Layout configuration types
│   ├── layout-config-extended.ts # Extended layout types
│   └── layout-service-interfaces.ts # Service interfaces
├── services/
│   ├── editor.service.ts    # Graph editing operations
│   └── layout/
│       ├── layout.service.ts      # Main layout service
│       ├── layout-config.service.ts # Configuration management
│       └── uniform-spacing-layout.ts # Layout algorithm
└── helper/
    ├── add-approve-node-helper.ts
    └── flow-node-helper.ts
```

### Services

#### LayoutService

Main service for managing graph layout calculations and applications.

```typescript
@Injectable({ providedIn: 'root' })
export class LayoutService implements ILayoutService {
  // Observable of layout results
  readonly layoutResult$: Observable<LayoutResult>;

  // Observable of configuration
  readonly config$: Observable<LayoutConfig>;

  // Trigger layout recalculation
  triggerLayout(): void;

  // Apply layout to graph
  applyLayout(result: LayoutResult, animate?: boolean): void;
}
```

**Key Features**:
- Reactive updates using RxJS
- Debouncing (16ms) for ~60fps performance
- Automatic caching to avoid duplicate calculations
- Configuration change detection with `distinctUntilChanged`

#### LayoutConfigService

Manages layout configuration with validation and persistence.

```typescript
@Injectable({ providedIn: 'root' })
export class LayoutConfigService {
  // Observable of configuration
  readonly config$: Observable<LayoutConfig>;

  // Update configuration
  updateConfig(config: Partial<LayoutConfig>): void;

  // Apply preset
  applyPreset(preset: LayoutPreset): void;

  // Reset to defaults
  resetToDefaults(): void;
}
```

**Configuration Options**:

| Option | Type | Range | Default | Description |
|--------|------|-------|---------|-------------|
| `verticalSpacing` | `number` | 20-200 | 50 | Vertical distance between adjacent nodes (px) |
| `horizontalSpacing` | `number` | 50-500 | 100 | Horizontal distance between nodes (px) |
| `animate` | `boolean` | - | true | Enable layout transition animations |
| `animationDuration` | `number` | - | 300 | Animation duration (ms) |

**Presets**:

- **Compact**: `{ verticalSpacing: 20, horizontalSpacing: 80 }`
- **Standard**: `{ verticalSpacing: 50, horizontalSpacing: 100 }` (default)
- **Spacious**: `{ verticalSpacing: 100, horizontalSpacing: 150 }`

#### UniformSpacingLayout

Core layout engine implementing modified Sugiyama algorithm.

```typescript
@Injectable({ providedIn: 'root' })
export class UniformSpacingLayout implements LayoutEngine<FlowNode, FlowEdge> {
  // Calculate layout positions
  layout(
    nodes: FlowNode[],
    edges: FlowEdge[],
    config: LayoutConfig,
    branchGroups?: BranchGroup[]
  ): LayoutResult;
}
```

**Algorithm Phases**:

1. **Level Assignment**: Uses BFS to assign hierarchy levels
2. **Node Grouping**: Groups nodes by level
3. **Parallel Branch Handling**: Aligns branch groups to same level
4. **Barycenter Ordering**: Reduces edge crossings using centroid heuristic
5. **Position Calculation**: Calculates positions with uniform spacing

**Error Handling**:
- Circular dependency detection
- Empty graph handling
- Multiple subgraph detection (with warning)

### Models

#### LayoutConfig

```typescript
interface LayoutConfig {
  verticalSpacing: number;      // 20-200, default: 50
  horizontalSpacing: number;    // 50-500, default: 100
  animate?: boolean;            // default: true
  animationDuration?: number;   // default: 300
}
```

#### LayoutResult

```typescript
interface LayoutResult {
  nodes: NodePosition[];
  metadata: LayoutMetadata;
}

interface LayoutMetadata {
  totalLevels: number;
  maxNodesPerLevel: number;
  calculationTime: number;      // milliseconds
  branchGroupCount: number;
  nodeCount: number;
  edgeCount: number;
}
```

## Usage

### Basic Integration

```typescript
import { LayoutService } from './services/layout/layout.service';
import { LayoutConfigService } from './services/layout/layout-config.service';

@Component({
  // ...
})
export class EditorComponent implements OnInit {
  constructor(
    private layoutService: LayoutService,
    private layoutConfigService: LayoutConfigService
  ) {}

  ngOnInit() {
    // Subscribe to layout updates
    this.layoutService.layoutResult$.subscribe(result => {
      console.log('Layout updated:', result.metadata);
    });

    // Trigger initial layout
    this.layoutService.triggerLayout();
  }

  // Update configuration
  updateSpacing(spacing: number) {
    this.layoutConfigService.updateConfig({
      verticalSpacing: spacing
    });
  }
}
```

### Configuration UI

The `LayoutConfigComponent` provides a user-friendly interface for layout configuration:

```html
<app-layout-config></app-layout-config>
```

This component includes:
- Vertical spacing slider (20-200px)
- Horizontal spacing slider (50-500px)
- Animation toggle
- Preset selector (Compact, Standard, Spacious)
- Reset button

## Performance

### Benchmarks

- **Small Graph** (10 nodes): <10ms
- **Medium Graph** (50 nodes): <50ms
- **Large Graph** (100 nodes): <100ms
- **Initial Render** (50 nodes): <500ms

### Optimization Techniques

1. **Debouncing**: Layout calculations are debounced by 16ms (~60fps)
2. **Caching**: Results are cached based on graph state and configuration
3. **SwitchMap**: Automatically cancels outdated calculations
4. **Distinct Until Changed**: Avoids duplicate calculations for same state

## Error Handling

The layout system provides comprehensive error handling:

### Circular Dependencies

Detected automatically and throws an error with cycle details:

```typescript
try {
  this.layoutService.triggerLayout();
} catch (error) {
  if (error.message.includes('Circular dependency')) {
    // Handle circular dependency
  }
}
```

### Multiple Subgraphs

When multiple disconnected subgraphs are detected, a warning is logged:

```typescript
console.warn(
  'Multiple subgraphs detected. Layout may not be optimal.'
);
```

### Empty Graph

Empty graphs are handled gracefully:

```typescript
const result = layout.layout([], [], config);
// Returns: { nodes: [], metadata: { totalLevels: 0, ... } }
```

## Testing

### Unit Tests

```bash
# Run all layout tests
npm test -- tests/unit/flow-graph/layout/

# Run specific test file
npm test -- tests/unit/flow-graph/layout/uniform-spacing-layout.spec.ts
```

### Test Coverage

- **UniformSpacingLayout**: Layer assignment, node ordering, spacing calculation, parallel branches
- **LayoutService**: Configuration management, caching, reactive updates
- **LayoutConfigService**: Validation, persistence, presets

### Performance Tests

Performance benchmarks are included to ensure layout calculation time meets targets:

```typescript
it('should calculate layout for 100 nodes in less than 100ms', () => {
  // Test implementation
});
```

## Future Enhancements

### Potential Improvements

1. **Web Worker Support**: Move layout calculations to background thread
2. **Virtual Scrolling**: Optimize rendering for very large graphs
3. **Incremental Updates**: Recalculate only affected portions
4. **Custom Layouts**: Support user-defined layout strategies
5. **Export/Import**: Save and load graph layouts
6. **Undo/Redo**: History management for layout changes

### Known Limitations

1. **Cyclic Graphs**: Not supported (throws error)
2. **Dynamic Sizing**: Node size changes require re-layout
3. **Cross-Level Edges**: May affect visual clarity

## References

- [AntV X6 Documentation](https://x6.antv.antgroup.com/)
- [Sugiyama Algorithm](https://en.wikipedia.org/wiki/Layered_graph_drawing)
- [Angular Reactive Programming](https://angular.io/guide/rxjs-library)

## Contributing

When adding new layout features:

1. Add type definitions to `layout-config.ts` or `layout-config-extended.ts`
2. Implement in `uniform-spacing-layout.ts` with TSDoc comments
3. Add unit tests to `tests/unit/flow-graph/layout/`
4. Update this README with new functionality
5. Verify performance benchmarks still pass
6. Run TypeScript compiler in strict mode

## License

Internal project - All rights reserved.
