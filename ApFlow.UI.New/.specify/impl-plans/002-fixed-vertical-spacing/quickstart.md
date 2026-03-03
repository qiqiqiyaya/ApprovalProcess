# Quick Start Guide: Fixed Vertical Spacing Layout Engine

**Feature ID**: 002-fixed-vertical-spacing  
**Target Audience**: Developers  
**Date**: 2026-03-04

---

## Overview

This guide helps developers quickly get started with the custom layout engine that enforces strict 50px vertical spacing in the ApFlow.UI flow graph editor.

---

## Prerequisites

Before using the layout engine, ensure you have:

- ✅ Angular 19+ installed
- ✅ TypeScript 5.7+ with strict mode enabled
- ✅ AntV X6 3.1+ and @antv/layout 0.3+ installed
- ✅ Existing `FlowGraph` and `FlowNode` models in your project
- ✅ RxJS available for reactive patterns

---

## Installation

The layout engine is provided as a standalone service. No additional installation is required if you're working within the ApFlow.UI project.

```bash
# Verify required dependencies are installed
npm list @antv/x6 @antv/layout rxjs
```

---

## Basic Usage

### 1. Import the Layout Engine

```typescript
import { FlowLayoutEngine } from './services/flow-layout-engine.service';
import type { ILayoutConfig, ILayoutResult } from './models/layout.models';
```

### 2. Inject the Service

```typescript
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class EditorService {
  private layoutEngine = inject(FlowLayoutEngine);
  
  constructor() {}
}
```

### 3. Calculate Layout

```typescript
// Basic usage with default configuration
const result = this.layoutEngine.layout(flowGraph);

console.log(`Total width: ${result.totalWidth}px`);
console.log(`Total height: ${result.totalHeight}px`);
console.log(`Max level: ${result.maxLevel}`);
```

### 4. Apply Layout to Graph

```typescript
// Convert layout result to X6 format
const graphData = {
  nodes: Array.from(result.nodePositions.values()).map(pos => ({
    id: pos.id,
    x: pos.x,
    y: pos.y,
    width: pos.width,
    height: pos.height,
  })),
  edges: flowGraph.edges,
};

// Apply to X6 graph
this.graph.fromJSON(graphData);
this.graph.centerContent();
```

---

## Configuration

### Default Configuration

```typescript
const DEFAULT_LAYOUT_CONFIG: ILayoutConfig = {
  verticalSpacing: 50,      // Fixed per constitution (cannot be changed)
  horizontalSpacing: 75,     // Space between nodes in same level
  baseYOffset: 0,            // Top padding
  centerGraph: true,         // Center horizontally in viewport
} as const;
```

### Custom Configuration

```typescript
const customConfig: ILayoutConfig = {
  verticalSpacing: 50,      // Must be 50
  horizontalSpacing: 100,    // Wider horizontal spacing
  baseYOffset: 50,          // Add 50px top padding
  centerGraph: false,       // Disable centering
};

const result = this.layoutEngine.layout(flowGraph, customConfig);
```

---

## RxJS Integration

### Reactive Layout Updates

The layout engine integrates seamlessly with RxJS for reactive updates:

```typescript
import { BehaviorSubject, debounceTime, switchMap, from, of } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Injectable({ providedIn: 'root' })
export class EditorService {
  private graphSubject = new BehaviorSubject<FlowGraph | null>(null);
  private layoutEngine = inject(FlowLayoutEngine);
  
  constructor() {
    // Set up reactive layout pipeline
    this.graphSubject.pipe(
      debounceTime(16), // ~60fps - prevents excessive recalculations
      switchMap(graph => {
        if (!graph) return of(null);
        
        // Calculate layout with error handling
        return from(this.layoutEngine.layout(graph)).pipe(
          catchError(error => {
            console.error('Layout failed:', error);
            // Display user-friendly error message
            this.notificationService.error(
              'Layout Error',
              error instanceof LayoutError ? error.message : 'Unknown error'
            );
            return of(null);
          })
        );
      }),
      takeUntilDestroyed()
    ).subscribe(result => {
      if (result) {
        this.applyLayout(result);
      }
    });
  }
  
  /**
   * Triggers layout recalculation
   * @param graph The flow graph to layout
   */
  public triggerLayout(graph: FlowGraph): void {
    this.graphSubject.next(graph);
  }
  
  private applyLayout(result: ILayoutResult): void {
    // Convert and apply to X6 graph
    const graphData = {
      nodes: Array.from(result.nodePositions.values()).map(pos => ({
        id: pos.id,
        x: pos.x,
        y: pos.y,
        width: pos.width,
        height: pos.height,
      })),
      edges: this.flowGraph.edges,
    };
    
    this.graph.fromJSON(graphData);
    this.graph.centerContent();
  }
}
```

### Performance Benefits

The RxJS pipeline provides:
- **Debouncing**: Prevents excessive recalculations during rapid user input
- **Cancellation**: `switchMap` cancels previous calculations when new ones arrive
- **Error Handling**: Graceful error recovery without breaking the UI
- **Reactive Updates**: Automatic layout updates when graph structure changes

---

## Error Handling

### Handling Layout Errors

The layout engine throws `LayoutError` for specific failure scenarios:

```typescript
import { LayoutError, LayoutErrorCode } from './models/layout.models';

try {
  const result = this.layoutEngine.layout(flowGraph);
} catch (error) {
  if (error instanceof LayoutError) {
    switch (error.code) {
      case LayoutErrorCode.CYCLE_DETECTED:
        console.error('Graph contains cycles:', error.message);
        break;
      
      case LayoutErrorCode.INVALID_NODE_SIZE:
        console.error('Invalid node dimensions:', error.message);
        break;
      
      case LayoutErrorCode.INVALID_GRAPH:
        console.error('Invalid graph structure:', error.message);
        break;
      
      default:
        console.error('Unknown layout error:', error.message);
    }
    
    // Display user-friendly error
    this.notificationService.error('Layout Error', error.message);
  }
}
```

### Validation Before Layout

```typescript
/**
 * Validates graph structure before layout calculation
 */
private validateGraph(graph: FlowGraph): void {
  if (!graph || !graph.nodes || graph.nodes.length === 0) {
    throw new LayoutError(
      'Graph must contain at least one node',
      LayoutErrorCode.INVALID_GRAPH
    );
  }
  
  if (!graph.edges) {
    throw new LayoutError(
      'Graph must have an edges array',
      LayoutErrorCode.INVALID_GRAPH
    );
  }
  
  // Validate node dimensions
  for (const node of graph.nodes) {
    if (!node.width || node.width <= 0 || !node.height || node.height <= 0) {
      throw new LayoutError(
        `Node ${node.id} has invalid dimensions: width=${node.width}, height=${node.height}`,
        LayoutErrorCode.INVALID_NODE_SIZE
      );
    }
  }
}
```

---

## Advanced Usage

### Accessing Level Information

```typescript
const result = this.layoutEngine.layout(flowGraph);

// Access level information
result.levels.forEach(level => {
  console.log(`Level ${level.levelIndex}:`);
  console.log(`  Base Y: ${level.baseY}px`);
  console.log(`  Nodes: ${level.nodes.join(', ')}`);
});

// Verify vertical spacing
for (let i = 0; i < result.levels.length - 1; i++) {
  const current = result.levels[i];
  const next = result.levels[i + 1];
  const spacing = next.baseY - current.baseY;
  console.log(`Spacing between level ${i} and ${i + 1}: ${spacing}px`);
  // Should always be 50px
}
```

### Inspecting Node Positions

```typescript
const result = this.layoutEngine.layout(flowGraph);

// Get position of a specific node
const nodeId = 'node-123';
const position = result.nodePositions.get(nodeId);

if (position) {
  console.log(`Node ${nodeId}:`);
  console.log(`  Level: ${position.level}`);
  console.log(`  Layout (center): (${position.layoutX}, ${position.layoutY})`);
  console.log(`  Final (top-left): (${position.x}, ${position.y})`);
  console.log(`  Dimensions: ${position.width}x${position.height}`);
}
```

### Caching Layout Results

The layout engine automatically caches results. To manually manage the cache:

```typescript
// Cache is internal to FlowLayoutEngine
// But you can manually trigger layout updates

// Force recalculation by modifying the graph
flowGraph.newNode('operation');
this.layoutEngine.layout(flowGraph); // Will use cache if structure unchanged

// Clear cache (if you implement a public method)
// this.layoutEngine.clearCache();
```

---

## Integration with BranchGroupManager

The layout engine integrates seamlessly with the `BranchGroupManager` to handle parallel branches:

```typescript
import { BranchGroupManager } from './models/branch-group-manager';

@Injectable({ providedIn: 'root' })
export class EditorService {
  private layoutEngine = inject(FlowLayoutEngine);
  
  public addParallelBranch(operationNode: FlowNode): void {
    const branchManager = this.getBranchGroupManager();
    
    // Add parallel branch using existing logic
    const group = branchManager.createBranchGroup(operationNode.id, 2);
    // ... add nodes to branches ...
    
    // Trigger layout recalculation
    // Layout engine will automatically handle parallel branches
    this.layoutEngine.layout(this.flowGraph);
  }
  
  /**
   * Gets the branch group manager
   */
  public getBranchGroupManager(): BranchGroupManager {
    if (!this.flowGraph) {
      throw new Error('FlowGraph is not initialized');
    }
    return (this.flowGraph as any).branchManager as BranchGroupManager;
  }
}
```

**Key Points**:
- Layout engine queries `BranchGroupManager` during level assignment
- All nodes in a parallel branch are assigned to the same level
- Merge nodes are assigned to the next level
- Vertical spacing of 50px is maintained for all nodes

---

## Testing

### Unit Testing Layout Engine

```typescript
import { TestBed } from '@angular/core/testing';
import { FlowLayoutEngine } from './flow-layout-engine.service';
import { FlowGraph } from '../models/flow-graph';

describe('FlowLayoutEngine', () => {
  let service: FlowLayoutEngine;
  
  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FlowLayoutEngine);
  });
  
  it('should create service', () => {
    expect(service).toBeTruthy();
  });
  
  it('should calculate layout with strict vertical spacing', () => {
    const graph = createTestGraph();
    const result = service.layout(graph);
    
    // Verify vertical spacing
    for (let i = 0; i < result.levels.length - 1; i++) {
      const spacing = result.levels[i + 1].baseY - result.levels[i].baseY;
      expect(spacing).toBe(50);
    }
  });
  
  it('should throw error for invalid graph', () => {
    expect(() => service.layout(null as any)).toThrowError('INVALID_GRAPH');
  });
  
  it('should throw error for cycles', () => {
    const graph = createCyclicGraph();
    expect(() => service.layout(graph)).toThrowError('CYCLE_DETECTED');
  });
  
  it('should cache layout results', () => {
    const graph = createTestGraph();
    const result1 = service.layout(graph);
    const result2 = service.layout(graph);
    
    // Should return same cached result
    expect(result1).toBe(result2);
  });
});

function createTestGraph(): FlowGraph {
  // Create a simple test graph
  return new FlowGraph(
    [
      new FlowNode('start'),
      new FlowNode('operation'),
      new FlowNode('approve', 'approve'),
      new FlowNode('end'),
    ],
    [
      new FlowEdge('start', 'operation'),
      new FlowEdge('operation', 'approve'),
      new FlowEdge('approve', 'end'),
    ]
  );
}

function createCyclicGraph(): FlowGraph {
  // Create a graph with a cycle
  const node1 = new FlowNode('node1');
  const node2 = new FlowNode('node2');
  return new FlowGraph(
    [node1, node2],
    [
      new FlowEdge(node1.id, node2.id),
      new FlowEdge(node2.id, node1.id), // Cycle!
    ]
  );
}
```

---

## Performance Optimization Tips

### 1. Use RxJS Debouncing

Always use `debounceTime(16)` for reactive layout updates:
```typescript
this.graphSubject.pipe(
  debounceTime(16), // ~60fps
  switchMap(graph => from(this.layoutEngine.layout(graph)))
).subscribe(result => {
  // Apply layout
});
```

### 2. Enable Caching

The layout engine automatically caches results. No additional setup required.

### 3. Minimize Unnecessary Recalculations

Only trigger layout when graph structure changes:
```typescript
// ❌ Bad: Layout on every render
ngAfterViewChecked() {
  this.layoutEngine.layout(this.flowGraph);
}

// ✅ Good: Layout only when needed
onNodeAdded(node: FlowNode) {
  this.flowGraph.newNode(node.shape);
  this.triggerLayout();
}
```

### 4. Use Virtual Scrolling for Large Graphs

For graphs with 100+ nodes, consider implementing virtual scrolling:
```typescript
// Render only visible nodes
const visibleNodes = this.getVisibleNodes(result);
this.graph.fromJSON({ nodes: visibleNodes, edges: [] });
```

---

## Troubleshooting

### Issue: Nodes Overlapping

**Symptom**: Nodes in the same level overlap each other.

**Solution**: Increase `horizontalSpacing`:
```typescript
const config: ILayoutConfig = {
  verticalSpacing: 50,
  horizontalSpacing: 100, // Increase from default 75
};
```

---

### Issue: Vertical Spacing Not 50px

**Symptom**: Vertical spacing varies between levels.

**Solution**: Ensure you're using `FlowLayoutEngine` (not `DagreLayout`):
```typescript
// ❌ Wrong: DagreLayout doesn't enforce strict spacing
const dagreLayout = new DagreLayout({ ranksep: 35 });

// ✅ Correct: FlowLayoutEngine enforces 50px spacing
const result = this.layoutEngine.layout(flowGraph);
```

---

### Issue: Layout Calculation Too Slow

**Symptom**: Layout calculation takes > 100ms for 100-node graphs.

**Solution**:
1. Enable caching (automatic)
2. Use RxJS debouncing
3. Consider Web Worker for very large graphs:
```typescript
// Future enhancement: Offload to Web Worker
const worker = new Worker('./layout.worker');
worker.postMessage(graph);
worker.onmessage = ({ data }) => {
  const result = data as ILayoutResult;
  this.applyLayout(result);
};
```

---

### Issue: "Graph contains cycles" Error

**Symptom**: `LayoutError` with code `CYCLE_DETECTED`.

**Solution**: Graph must be a Directed Acyclic Graph (DAG). Check for:
- Edges that create loops (A → B → C → A)
- Self-loops (A → A)
- Break cycles before calling `layout()`

---

## Common Patterns

### Pattern 1: Auto-Layout on Node Change

```typescript
onNodeChanged(node: FlowNode): void {
  this.flowGraph.updateNode(node);
  this.triggerLayout(); // Auto-recalculate
}
```

---

### Pattern 2: Layout with Undo/Redo

```typescript
// Save state before layout
const previousLayout = this.saveCurrentLayout();

// Calculate new layout
const result = this.layoutEngine.layout(flowGraph);
this.applyLayout(result);

// Allow undo
this.undoManager.push(() => {
  this.restoreLayout(previousLayout);
});
```

---

### Pattern 3: Layout with Animation

```typescript
async animateLayout(graph: FlowGraph): Promise<void> {
  const result = this.layoutEngine.layout(graph);
  
  for (const [nodeId, position] of result.nodePositions) {
    const node = this.graph.getCell(nodeId);
    if (node) {
      await node.transition('position', { x: position.x, y: position.y }, {
        duration: 300,
      });
    }
  }
}
```

---

## Next Steps

- 📖 Read the [Data Model Document](./data-model.md) for detailed entity definitions
- 📖 Read the [Interface Contract](./contracts/layout-engine-contract.md) for API specifications
- 📖 Read the [Research Document](./research.md) for algorithm details and design decisions
- 🔧 Review the [Feature Specification](../../../specs/002-fixed-vertical-spacing/spec.md) for requirements
- ✅ Check the [Project Constitution](../../../memory/constitution.md) for development principles

---

## Support

For questions or issues:
- **Feature ID**: 002-fixed-vertical-spacing
- **Maintainer**: ApFlow.UI Development Team
- **Documentation Version**: 1.0.0

---

**Happy Coding! 🚀**
