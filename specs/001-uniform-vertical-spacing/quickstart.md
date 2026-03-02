# Quick Start Guide: Uniform Vertical Spacing Feature

**Feature Branch**: `001-uniform-vertical-spacing`  
**Date**: 2026-03-03  
**Target Audience**: Developers

This guide helps you quickly get started with implementing and using the uniform vertical spacing feature in ApFlow.UI.

---

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Installation](#installation)
3. [Quick Start](#quick-start)
4. [Basic Usage](#basic-usage)
5. [Configuration](#configuration)
6. [Testing](#testing)
7. [Troubleshooting](#troubleshooting)
8. [API Reference](#api-reference)

---

## Prerequisites

Before starting, ensure you have:

- ✅ Node.js 18+ installed
- ✅ Angular CLI 21.0.4+ installed
- ✅ Git repository cloned and on `001-uniform-vertical-spacing` branch
- ✅ Existing ApFlow.UI project structure set up
- ✅ TypeScript strict mode enabled

### Verify Setup

```bash
# Check Node.js version
node --version  # Should be 18+

# Check Angular CLI version
ng version  # Should show Angular 21.0.0+

# Check current branch
git branch --show-current  # Should show 001-uniform-vertical-spacing
```

---

## Installation

### 1. Install Dependencies

The feature uses the following existing dependencies (already installed):

```json
{
  "@angular/common": "^21.0.0",
  "@angular/core": "^21.0.0",
  "@antv/x6": "^3.1.5",
  "@antv/layout": "^0.3.12",
  "rxjs": "~7.8.0"
}
```

No additional dependencies are required.

### 2. Create Directory Structure

Create the following directories if they don't exist:

```bash
# Create layout services directory
mkdir -p src/app/pages/flow-graph/services/layout

# Create layout configuration component directory
mkdir -p src/app/pages/flow-graph/components/layout-config

# Create layout tests directory
mkdir -p tests/unit/flow-graph/layout
```

### 3. Update TypeScript Configuration

Ensure `tsconfig.json` has strict mode enabled:

```json
{
  "compilerOptions": {
    "strict": true,
    "noImplicitOverride": true,
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "noImplicitReturns": true
  }
}
```

---

## Quick Start

### Step 1: Create Layout Configuration Service

Create `src/app/pages/flow-graph/services/layout/layout-config.service.ts`:

```typescript
import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { LayoutConfig, LayoutPreset, LAYOUT_PRESETS } from '../models/layout-config';

@Injectable({ providedIn: 'root' })
export class LayoutConfigService {
  private readonly STORAGE_KEY = 'apflow.layout.config';
  private configSubject = new BehaviorSubject<LayoutConfig>({
    verticalSpacing: 50,
    horizontalSpacing: 100,
    animate: true,
    animationDuration: 300,
  });

  readonly config$ = this.configSubject.asObservable();
  readonly config = this.configSubject.value;

  constructor() {
    this.loadFromStorage();
  }

  updateConfig(partialConfig: Partial<LayoutConfig>): void {
    const current = this.configSubject.value;
    const updated = { ...current, ...partialConfig };
    this.configSubject.next(updated);
    this.saveToStorage(updated);
  }

  resetToDefaults(): void {
    const defaults: LayoutConfig = {
      verticalSpacing: 50,
      horizontalSpacing: 100,
      animate: true,
      animationDuration: 300,
    };
    this.configSubject.next(defaults);
    this.saveToStorage(defaults);
  }

  getConfigSnapshot(): LayoutConfig {
    return { ...this.configSubject.value };
  }

  applyPreset(preset: LayoutPreset): void {
    this.updateConfig(preset.config);
  }

  getPresets(): LayoutPreset[] {
    return LAYOUT_PRESETS;
  }

  getCurrentPreset(): LayoutPreset | null {
    const current = this.getConfigSnapshot();
    return (
      LAYOUT_PRESETS.find(
        preset =>
          preset.config.verticalSpacing === current.verticalSpacing &&
          preset.config.horizontalSpacing === current.horizontalSpacing
      ) || null
    );
  }

  private loadFromStorage(): void {
    try {
      const stored = localStorage.getItem(this.STORAGE_KEY);
      if (stored) {
        const parsed = JSON.parse(stored) as LayoutConfig;
        this.configSubject.next(parsed);
      }
    } catch (error) {
      console.error('Failed to load layout config:', error);
    }
  }

  private saveToStorage(config: LayoutConfig): void {
    try {
      localStorage.setItem(this.STORAGE_KEY, JSON.stringify(config));
    } catch (error) {
      console.error('Failed to save layout config:', error);
    }
  }
}
```

### Step 2: Create Layout Models

Create `src/app/pages/flow-graph/models/layout-config.ts`:

```typescript
export interface LayoutConfig {
  verticalSpacing: number;
  horizontalSpacing: number;
  minNodeWidth?: number;
  minNodeHeight?: number;
  animate?: boolean;
  animationDuration?: number;
}

export interface LayoutPreset {
  name: string;
  description: string;
  config: LayoutConfig;
}

export const LAYOUT_PRESETS: LayoutPreset[] = [
  {
    name: 'Compact',
    description: 'Dense layout for complex flows',
    config: {
      verticalSpacing: 30,
      horizontalSpacing: 80,
      animate: true,
      animationDuration: 200,
    },
  },
  {
    name: 'Standard',
    description: 'Balanced layout (default)',
    config: {
      verticalSpacing: 50,
      horizontalSpacing: 100,
      animate: true,
      animationDuration: 300,
    },
  },
  {
    name: 'Spacious',
    description: 'Wide layout for presentations',
    config: {
      verticalSpacing: 80,
      horizontalSpacing: 150,
      animate: true,
      animationDuration: 400,
    },
  },
];
```

### Step 3: Create Layout Engine

Create `src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts`:

```typescript
import { Injectable, inject } from '@angular/core';
import { LayoutConfig, LayoutResult, NodePosition, LayoutMetadata, LayoutEngine, LayoutErrorCode } from '../models/layout-config';
import { Graph } from '@antv/x6';
import { FlowNode, FlowEdge } from '../../models/flow-node';

@Injectable({ providedIn: 'root' })
export class UniformSpacingLayout implements LayoutEngine<FlowNode, FlowEdge> {
  layout(nodes: FlowNode[], edges: FlowEdge[], config: LayoutConfig): LayoutResult {
    const startTime = performance.now();
    
    // Step 1: Build hierarchy and calculate levels
    const levels = this.calculateLevels(nodes, edges);
    
    // Step 2: Group nodes by level
    const levelNodes = this.groupNodesByLevel(nodes, levels);
    
    // Step 3: Calculate positions with uniform spacing
    const positions = this.calculatePositions(levelNodes, config);
    
    // Step 4: Build metadata
    const calculationTime = performance.now() - startTime;
    const metadata: LayoutMetadata = {
      totalLevels: levelNodes.size,
      maxNodesPerLevel: Math.max(...Array.from(levelNodes.values()).map(ns => ns.length)),
      calculationTime,
      branchGroupCount: 0,
      nodeCount: nodes.length,
      edgeCount: edges.length,
    };
    
    return { nodes: positions, metadata };
  }
  
  private calculateLevels(nodes: FlowNode[], edges: FlowEdge[]): Map<string, number> {
    const levels = new Map<string, number>();
    const visited = new Set<string>();
    
    // Find root nodes (no incoming edges)
    const incomingEdges = new Map<string, string[]>();
    edges.forEach(edge => {
      if (!incomingEdges.has(edge.target)) {
        incomingEdges.set(edge.target, []);
      }
      incomingEdges.get(edge.target)!.push(edge.source);
    });
    
    const roots = nodes.filter(n => !incomingEdges.has(n.id));
    
    // BFS to assign levels
    const queue = roots.map(n => ({ node: n, level: 0 }));
    queue.forEach(({ node, level }) => {
      levels.set(node.id, level);
      visited.add(node.id);
    });
    
    while (queue.length > 0) {
      const { node, level } = queue.shift()!;
      
      // Find children
      const children = edges.filter(e => e.source === node.id).map(e => nodes.find(n => n.id === e.target)).filter(Boolean) as FlowNode[];
      
      children.forEach(child => {
        if (!visited.has(child.id)) {
          const childLevel = level + 1;
          levels.set(child.id, childLevel);
          visited.add(child.id);
          queue.push({ node: child, level: childLevel });
        }
      });
    }
    
    return levels;
  }
  
  private groupNodesByLevel(nodes: FlowNode[], levels: Map<string, number>): Map<number, FlowNode[]> {
    const levelNodes = new Map<number, FlowNode[]>();
    
    levels.forEach((level, nodeId) => {
      if (!levelNodes.has(level)) {
        levelNodes.set(level, []);
      }
      const node = nodes.find(n => n.id === nodeId);
      if (node) {
        levelNodes.get(level)!.push(node);
      }
    });
    
    return levelNodes;
  }
  
  private calculatePositions(levelNodes: Map<number, FlowNode[]>, config: LayoutConfig): NodePosition[] {
    const positions: NodePosition[] = [];
    const { verticalSpacing, horizontalSpacing } = config;
    
    levelNodes.forEach((nodes, level) => {
      const y = level * verticalSpacing;
      
      nodes.forEach((node, index) => {
        const x = index * horizontalSpacing;
        positions.push({
          id: node.id,
          x,
          y,
          level,
        });
      });
    });
    
    return positions;
  }
}
```

### Step 4: Create Main Layout Service

Create `src/app/pages/flow-graph/services/layout/layout.service.ts`:

```typescript
import { Injectable, Inject } from '@angular/core';
import { Observable, Subject, BehaviorSubject, debounceTime, switchMap, tap } from 'rxjs';
import { Graph } from '@antv/x6';
import { LayoutConfig, LayoutResult } from '../../models/layout-config';
import { LayoutConfigService } from './layout-config.service';
import { UniformSpacingLayout } from './uniform-spacing-layout';

export const X6_GRAPH = new InjectionToken<Graph>('X6_GRAPH');

@Injectable({ providedIn: 'root' })
export class LayoutService {
  private layoutTrigger$ = new Subject<void>();
  private layoutResultSubject = new BehaviorSubject<LayoutResult | null>(null);
  
  readonly layoutResult$ = this.layoutResultSubject.asObservable();
  
  private layoutEngine: UniformSpacingLayout;
  
  constructor(
    @Inject(X6_GRAPH) private graph: Graph,
    private configService: LayoutConfigService
  ) {
    this.layoutEngine = new UniformSpacingLayout();
    this.setupLayoutTrigger();
  }
  
  triggerLayout(): void {
    this.layoutTrigger$.next();
  }
  
  private setupLayoutTrigger(): void {
    this.layoutTrigger$.pipe(
      debounceTime(16), // ~60fps
      switchMap(() => this.calculateLayout$())
    ).subscribe(result => {
      this.applyLayout(result);
    });
  }
  
  private calculateLayout$(): Observable<LayoutResult> {
    return new Observable(subscriber => {
      const config = this.configService.getConfigSnapshot();
      const nodes = this.graph.getNodes().map(node => ({
        id: node.id,
        // ... extract node properties
      }));
      const edges = this.graph.getEdges().map(edge => ({
        id: edge.id,
        source: edge.getSourceCellId()!,
        target: edge.getTargetCellId()!,
      }));
      
      try {
        const result = this.layoutEngine.layout(nodes, edges, config);
        subscriber.next(result);
        subscriber.complete();
      } catch (error) {
        subscriber.error(error);
      }
    });
  }
  
  private applyLayout(result: LayoutResult): void {
    const config = this.configService.getConfigSnapshot();
    const animate = config.animate ?? true;
    const duration = config.animationDuration ?? 300;
    
    result.nodes.forEach(position => {
      const cell = this.graph.getCellById(position.id);
      if (cell && cell.isNode()) {
        if (animate) {
          cell.transition('position', { x: position.x, y: position.y }, {
            duration,
            easing: 'ease',
          });
        } else {
          cell.position(position.x, position.y);
        }
      }
    });
    
    this.layoutResultSubject.next(result);
  }
}
```

### Step 5: Update Editor Component

Update `src/app/pages/flow-graph/components/editor/editor.component.ts`:

```typescript
import { Component, OnInit, inject } from '@angular/core';
import { LayoutService } from '../../services/layout/layout.service';
import { LayoutConfigService } from '../../services/layout/layout-config.service';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.css'],
})
export class EditorComponent implements OnInit {
  private layoutService = inject(LayoutService);
  private configService = inject(LayoutConfigService);
  
  ngOnInit(): void {
    // Subscribe to layout updates
    this.layoutService.layoutResult$.subscribe(result => {
      console.log('Layout updated:', result.metadata);
    });
  }
  
  // Call this when nodes are added/removed
  onNodeChange(): void {
    this.layoutService.triggerLayout();
  }
}
```

### Step 6: Test the Implementation

Create a simple test in your component:

```typescript
// In your component's ngOnInit
setTimeout(() => {
  this.layoutService.triggerLayout();
}, 1000); // Trigger layout after graph is ready
```

---

## Basic Usage

### Triggering Layout

```typescript
// In your component
constructor(private layoutService: LayoutService) {}

// Trigger layout when nodes change
onNodeAdded(): void {
  this.layoutService.triggerLayout();
}

onNodeDeleted(): void {
  this.layoutService.triggerLayout();
}

onNodeResized(): void {
  this.layoutService.triggerLayout();
}
```

### Subscribing to Layout Results

```typescript
// Subscribe to layout updates
this.layoutService.layoutResult$.subscribe(result => {
  console.log('Layout calculation time:', result.metadata.calculationTime, 'ms');
  console.log('Total levels:', result.metadata.totalLevels);
  console.log('Node positions:', result.nodes);
});
```

### Manual Configuration

```typescript
// Update configuration programmatically
constructor(private configService: LayoutConfigService) {}

// Change spacing
setCompactSpacing(): void {
  this.configService.updateConfig({
    verticalSpacing: 30,
    horizontalSpacing: 80,
  });
}

setSpaciousSpacing(): void {
  this.configService.updateConfig({
    verticalSpacing: 80,
    horizontalSpacing: 150,
  });
}

// Disable animation
disableAnimation(): void {
  this.configService.updateConfig({
    animate: false,
  });
}

// Reset to defaults
resetSpacing(): void {
  this.configService.resetToDefaults();
}
```

### Using Presets

```typescript
// Apply a preset
applyStandardPreset(): void {
  const presets = this.configService.getPresets();
  const standard = presets.find(p => p.name === 'Standard');
  if (standard) {
    this.configService.applyPreset(standard);
  }
}
```

---

## Configuration

### UI Component (User Story 3)

Create `src/app/pages/flow-graph/components/layout-config/layout-config.component.ts`:

```typescript
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LayoutConfigService } from '../../services/layout/layout-config.service';
import { LayoutPreset } from '../../models/layout-config';

@Component({
  selector: 'app-layout-config',
  template: `
    <div class="layout-config">
      <h3>Layout Configuration</h3>
      
      <nz-form [formGroup]="configForm" (ngSubmit)="onSubmit()">
        <nz-form-item>
          <nz-form-label [nzSpan]="8">Vertical Spacing (px)</nz-form-label>
          <nz-form-control [nzSpan]="16">
            <nz-input-number
              formControlName="verticalSpacing"
              [nzMin]="20"
              [nzMax]="200"
              [nzStep]="10"
            ></nz-input-number>
          </nz-form-control>
        </nz-form-item>
        
        <nz-form-item>
          <nz-form-label [nzSpan]="8">Horizontal Spacing (px)</nz-form-label>
          <nz-form-control [nzSpan]="16">
            <nz-input-number
              formControlName="horizontalSpacing"
              [nzMin]="50"
              [nzMax]="500"
              [nzStep]="10"
            ></nz-input-number>
          </nz-form-control>
        </nz-form-item>
        
        <nz-form-item>
          <nz-form-label [nzSpan]="8">Animate</nz-form-label>
          <nz-form-control [nzSpan]="16">
            <nz-switch formControlName="animate"></nz-switch>
          </nz-form-control>
        </nz-form-item>
        
        <button nz-button nzType="primary" [disabled]="configForm.invalid">
          Apply
        </button>
        <button nz-button (click)="onReset()">Reset</button>
      </nz-form>
      
      <div class="presets">
        <h4>Presets</h4>
        <nz-select
          [(ngModel)]="selectedPreset"
          (ngModelChange)="onPresetChange($event)"
          nzPlaceHolder="Select a preset"
        >
          <nz-option
            *ngFor="let preset of presets"
            [nzValue]="preset.name"
            [nzLabel]="preset.name"
          ></nz-option>
        </nz-select>
      </div>
    </div>
  `,
  styles: [`
    .layout-config {
      padding: 16px;
    }
    .presets {
      margin-top: 16px;
    }
  `],
})
export class LayoutConfigComponent implements OnInit {
  configForm: FormGroup;
  presets: LayoutPreset[] = [];
  selectedPreset: string | null = null;

  constructor(
    private fb: FormBuilder,
    private configService: LayoutConfigService
  ) {
    this.configForm = this.fb.group({
      verticalSpacing: [
        50,
        [Validators.required, Validators.min(20), Validators.max(200)],
      ],
      horizontalSpacing: [
        100,
        [Validators.required, Validators.min(50), Validators.max(500)],
      ],
      animate: [true],
      animationDuration: [300],
    });
  }

  ngOnInit(): void {
    // Load current config
    const config = this.configService.getConfigSnapshot();
    this.configForm.patchValue(config);
    
    // Load presets
    this.presets = this.configService.getPresets();
    
    // Check current preset
    const current = this.configService.getCurrentPreset();
    if (current) {
      this.selectedPreset = current.name;
    }
  }

  onSubmit(): void {
    if (this.configForm.valid) {
      this.configService.updateConfig(this.configForm.value);
    }
  }

  onReset(): void {
    this.configService.resetToDefaults();
    const defaults = this.configService.getConfigSnapshot();
    this.configForm.patchValue(defaults);
    this.selectedPreset = 'Standard';
  }

  onPresetChange(presetName: string): void {
    const preset = this.presets.find(p => p.name === presetName);
    if (preset) {
      this.configService.applyPreset(preset);
      this.configForm.patchValue(preset.config);
    }
  }
}
```

### Configuration Values

| Parameter | Default | Min | Max | Description |
|-----------|---------|-----|-----|-------------|
| `verticalSpacing` | 50px | 20px | 200px | Vertical space between nodes |
| `horizontalSpacing` | 100px | 50px | 500px | Horizontal space between nodes |
| `animate` | true | - | - | Enable layout animations |
| `animationDuration` | 300ms | 0ms | 1000ms | Animation duration |

---

## Testing

### Unit Tests

Create `tests/unit/flow-graph/layout/uniform-spacing-layout.spec.ts`:

```typescript
import { describe, it, expect } from 'vitest';
import { UniformSpacingLayout } from '../../../../src/app/pages/flow-graph/services/layout/uniform-spacing-layout';
import { FlowNode } from '../../../../src/app/pages/flow-graph/models/flow-node';
import { FlowEdge } from '../../../../src/app/pages/flow-graph/models/flow-edge';

describe('UniformSpacingLayout', () => {
  it('should calculate uniform vertical spacing', () => {
    const layout = new UniformSpacingLayout();
    const nodes: FlowNode[] = [
      { id: '1', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
      { id: '2', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
      { id: '3', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
    ];
    const edges: FlowEdge[] = [
      { id: 'e1', source: '1', target: '2' } as FlowEdge,
      { id: 'e2', source: '2', target: '3' } as FlowEdge,
    ];
    const config = { verticalSpacing: 50, horizontalSpacing: 100 };
    
    const result = layout.layout(nodes, edges, config);
    
    expect(result.nodes).toHaveLength(3);
    expect(result.nodes[0].y).toBe(0);
    expect(result.nodes[1].y).toBe(50);
    expect(result.nodes[2].y).toBe(100);
  });
  
  it('should handle empty graph', () => {
    const layout = new UniformSpacingLayout();
    const nodes: FlowNode[] = [];
    const edges: FlowEdge[] = [];
    const config = { verticalSpacing: 50, horizontalSpacing: 100 };
    
    const result = layout.layout(nodes, edges, config);
    
    expect(result.nodes).toHaveLength(0);
  });
});
```

### Run Tests

```bash
# Run all tests
npm test

# Run specific test file
npm test uniform-spacing-layout.spec.ts

# Run with coverage
npm test -- --coverage
```

### Manual Testing

1. **Test Uniform Spacing**:
   - Create a simple flow with 5 nodes
   - Verify all vertical gaps are exactly 50px
   - Use browser dev tools to measure distances

2. **Test Different Node Sizes**:
   - Create nodes with varying heights (40px, 100px, 150px)
   - Verify spacing remains 50px regardless of node height

3. **Test Configuration Changes**:
   - Change vertical spacing to 30px
   - Verify all gaps update to 30px
   - Change to 100px and verify again

4. **Test Performance**:
   - Create a flow with 50 nodes
   - Measure layout recalculation time (should be < 100ms)
   - Check browser console for calculation time in metadata

---

## Troubleshooting

### Common Issues

#### Issue: Layout not applying

**Symptoms**: Nodes don't move after calling `triggerLayout()`

**Solutions**:
1. Check if X6 graph is initialized: `console.log(this.graph.isInitialized())`
2. Verify nodes have valid IDs: `console.log(this.graph.getNodes().map(n => n.id))`
3. Check for circular dependencies in edges
4. Ensure LayoutService is provided at root level

#### Issue: Spacing not uniform

**Symptoms**: Vertical gaps vary between nodes

**Solutions**:
1. Verify `verticalSpacing` configuration value
2. Check node level calculation: `console.log(result.nodes.map(n => ({ id: n.id, level: n.level, y: n.y })))`
3. Ensure no manual node positioning is overriding layout
4. Check if BranchGroup is interfering with layout

#### Issue: Performance issues with large graphs

**Symptoms**: Layout takes > 100ms or UI freezes

**Solutions**:
1. Check graph size: `console.log('Nodes:', this.graph.getNodes().length)`
2. Verify debounce is working (should wait 16ms)
3. Consider implementing incremental updates for 100+ nodes
4. Check browser console for calculation time in metadata

#### Issue: Animation not working

**Symptoms**: Nodes jump to new positions without animation

**Solutions**:
1. Check `animate` configuration: `configService.config.animate`
2. Verify animation duration is set: `configService.config.animationDuration`
3. Ensure X6 graph supports transitions
4. Check browser console for animation errors

### Debug Mode

Enable debug logging:

```typescript
// In LayoutService constructor
private debugMode = true;

private calculateLayout$(): Observable<LayoutResult> {
  return new Observable(subscriber => {
    if (this.debugMode) {
      console.log('[Layout] Starting calculation...');
      console.log('[Layout] Nodes:', nodes.length);
      console.log('[Layout] Edges:', edges.length);
      console.log('[Layout] Config:', config);
    }
    
    // ... calculation
    
    if (this.debugMode) {
      console.log('[Layout] Calculation complete:', result.metadata.calculationTime, 'ms');
    }
  });
}
```

### Validation Tools

Validate layout result:

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

---

## API Reference

### LayoutService

| Method | Parameters | Returns | Description |
|--------|------------|---------|-------------|
| `triggerLayout()` | - | `void` | Triggers layout recalculation |
| `layoutResult$` | - | `Observable<LayoutResult>` | Observable of layout results |

### LayoutConfigService

| Method | Parameters | Returns | Description |
|--------|------------|---------|-------------|
| `updateConfig()` | `Partial<LayoutConfig>` | `void` | Updates configuration |
| `resetToDefaults()` | - | `void` | Resets to default values |
| `getConfigSnapshot()` | - | `LayoutConfig` | Gets current config |
| `applyPreset()` | `LayoutPreset` | `void` | Applies a preset |
| `getPresets()` | - | `LayoutPreset[]` | Gets all presets |
| `getCurrentPreset()` | - | `LayoutPreset \| null` | Gets current preset |
| `config$` | - | `Observable<LayoutConfig>` | Observable of config changes |

### LayoutConfig Interface

```typescript
interface LayoutConfig {
  verticalSpacing: number;      // 20-200, default: 50
  horizontalSpacing: number;    // 50-500, default: 100
  minNodeWidth?: number;
  minNodeHeight?: number;
  animate?: boolean;            // default: true
  animationDuration?: number;   // default: 300
}
```

### LayoutResult Interface

```typescript
interface LayoutResult {
  nodes: NodePosition[];
  edges?: EdgeRoute[];
  metadata: LayoutMetadata;
}

interface NodePosition {
  id: string;
  x: number;
  y: number;
  level?: number;
}

interface LayoutMetadata {
  totalLevels: number;
  maxNodesPerLevel: number;
  calculationTime: number;
  branchGroupCount: number;
  nodeCount: number;
  edgeCount: number;
}
```

---

## Next Steps

1. **Implement User Story 1 (P1)**: Basic uniform spacing with hardcoded 50px
2. **Implement User Story 2 (P2)**: Visual balance validation for different node sizes
3. **Implement User Story 3 (P3)**: Configuration UI with presets
4. **Add comprehensive tests**: Unit tests for all components
5. **Performance optimization**: Benchmark with 100+ nodes
6. **Documentation**: Add TSDoc comments to all public APIs

---

## Resources

- [AntV X6 Documentation](https://x6.antv.antgroup.com/)
- [AntV Layout Documentation](https://layout.antv.antgroup.com/)
- [Angular RxJS Documentation](https://rxjs.dev/)
- [Project Data Model](./data-model.md)
- [Research Report](./research.md)

---

**Quick Start Guide Completed**: 2026-03-03  
**Status**: ✅ Ready for Implementation  
**Questions?** Check the [troubleshooting section](#troubleshooting) or refer to the full [data model documentation](./data-model.md).
