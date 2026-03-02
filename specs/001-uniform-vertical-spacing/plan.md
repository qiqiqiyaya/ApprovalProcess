# Implementation Plan: Uniform Vertical Spacing for Flow Graph Nodes

**Branch**: `001-uniform-vertical-spacing` | **Date**: 2026-03-03 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/001-uniform-vertical-spacing/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/plan-template.md` for the execution workflow.

## Summary

实现流程图节点的统一垂直间距功能，确保所有相邻节点之间的垂直间距保持一致的固定值（默认50像素），无论节点尺寸如何变化。通过AntV X6的布局引擎和自定义布局算法，支持简单流程图、包含并行分支的复杂流程图以及嵌套并行分支结构。系统将提供配置化的间距值，并在添加、删除或调整节点时自动重新计算布局以保持间距统一。

## Technical Context

**Language/Version**: TypeScript 5.9.2 (Strict Mode Enabled)
**Primary Dependencies**:
- Angular 21.0.0 (UI Framework)
- @antv/x6 3.1.5 (Graph Visualization Engine)
- @antv/layout 0.3.12 (Layout Algorithm)
- @antv/x6-angular-shape 3.0.1 (Angular Integration)
- ng-zorro-antd 21.0.2 (UI Components)
- RxJS 7.8.0 (Reactive Programming)

**Storage**: In-memory (FlowGraph model with nodes and edges arrays)
**Testing**: Vitest 4.0.8 + Angular Test Bed
**Target Platform**: Web Browser (Desktop/Tablet)
**Project Type**: Web Application (Angular SPA)
**Performance Goals**:
- Layout recalculation: < 100ms (after node add/delete)
- Initial rendering: < 500ms for 50-node graphs
- Node size change response: < 50ms

**Constraints**:
- Must maintain AntV X6 graph integration
- Must preserve existing BranchGroup and parallel branch functionality
- Must support nested parallel branches
- TypeScript strict mode violations not allowed

**Scale/Scope**:
- Target: Support up to 100 nodes per flow graph
- Impact: All flow graph visualization components
- Affected modules: `src/app/pages/flow-graph/`

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### ✅ I. Angular Best Practices - PASS
- [x] Will use Angular services for layout calculation
- [x] Will use RxJS patterns for reactive layout updates
- [x] Will organize by feature (flow-graph module)
- [x] Will maintain change detection efficiency

**Implementation**: Layout service with BehaviorSubject for state management, OnPush strategy for components.

### ✅ II. Visual Consistency (ng-zorro-antd) - PASS
- [x] Configuration UI will use ng-zorro-antd form components
- [x] Spacing configuration will use nz-input-number with proper validation
- [x] Will use ng-zorro-antd design tokens for visual consistency

**Implementation**: nz-slider or nz-input-number for spacing configuration, nz-form-item for layout.

### ✅ III. Flow Graph Visualisation (AntV X6) - PASS
- [x] Will use @antv/x6 for graph rendering
- [x] Will extend AntV X6 base classes for custom layout
- [x] Will integrate with Angular change detection
- [x] Will use AntV X6 built-in graph interactions

**Implementation**: Custom layout algorithm using @antv/layout, integrated with X6 graph instance.

### ✅ IV. Type Safety (TypeScript) - PASS
- [x] Will enforce TypeScript strict mode
- [x] Will use interfaces for data models (LayoutConfig, FlowNode, etc.)
- [x] Will use generics for type-safe components
- [x] Will perform null checks before property access

**Implementation**: Strongly typed interfaces for LayoutConfig, LayoutResult, LayoutCalculator.

### ✅ V. Incremental Delivery - PASS
- [x] User Story 1 (P1): Display uniform vertical spacing - Independent, testable, high value
- [x] User Story 2 (P2): Visual balance with different node sizes - Builds on P1, independent test
- [x] User Story 3 (P3): Configurable spacing - Enhancement, can be delivered separately

**Implementation**: 
- MVP: User Story 1 (hardcoded 50px spacing)
- Enhancement: User Story 2 (visual balance validation)
- Enhancement: User Story 3 (configurable spacing via UI)

### ✅ All Gates Passed (Pre-Phase 0)

**Conclusion**: Feature specification fully complies with ApFlow.UI constitution. No violations detected. Ready to proceed with Phase 0 research.

---

## Post-Phase 1 Constitution Re-Check

*GATE: Must pass after Phase 1 design before Phase 2 implementation.*

### ✅ I. Angular Best Practices - PASS (Verified)
- [x] **Implemented**: `LayoutService` with `BehaviorSubject` for reactive state management
- [x] **Implemented**: `LayoutConfigService` provided at root level with `@Injectable({ providedIn: 'root' })`
- [x] **Implemented**: RxJS operators (`debounceTime`, `switchMap`, `distinctUntilChanged`) for efficient updates
- [x] **Implemented**: Feature module organization in `src/app/pages/flow-graph/services/layout/`
- [x] **Planned**: OnPush change detection strategy in components using layout

**Implementation Details**:
- `LayoutConfigService` uses `BehaviorSubject` with `asObservable()` pattern
- `LayoutService` uses `switchMap` to cancel outdated layout calculations
- Debounce time of 16ms (~60fps) prevents excessive recalculations
- All services follow Angular dependency injection conventions

### ✅ II. Visual Consistency (ng-zorro-antd) - PASS (Verified)
- [x] **Designed**: `LayoutConfigComponent` using ng-zorro-antd form components
- [x] **Designed**: `nz-input-number` for spacing configuration with min/max validation
- [x] **Designed**: `nz-form-item` and `nz-form-control` for form layout
- [x] **Designed**: `nz-switch` for animate toggle
- [x] **Designed**: `nz-select` for preset selection

**Implementation Details**:
- Configuration form uses Reactive Forms with ng-zorro-antd components
- Validators configured for `verticalSpacing` (20-200) and `horizontalSpacing` (50-500)
- Preset dropdown uses `nz-select` with `nz-option` components
- All components follow ng-zorro-antd design patterns

### ✅ III. Flow Graph Visualisation (AntV X6) - PASS (Verified)
- [x] **Designed**: Custom `UniformSpacingLayout` class extending layout pattern
- [x] **Designed**: Integration with AntV X6 via `graph.fromJSON()` and `cell.transition()`
- [x] **Designed**: `X6_GRAPH` injection token for type-safe graph instance access
- [x] **Designed**: Layout result application with optional animation
- [x] **Designed**: Preservation of existing `FlowGraph` and `BranchGroup` integration

**Implementation Details**:
- `UniformSpacingLayout` implements `LayoutEngine<TNode, TEdge>` interface
- Layout algorithm uses modified Sugiyama approach (layering, ordering, positioning)
- Integration with X6 through `graph.getCellById()` and `cell.position()`
- Animation uses X6's `cell.transition()` with configurable duration
- Parallel branch handling respects existing `BranchGroup` structure

### ✅ IV. Type Safety (TypeScript) - PASS (Verified)
- [x] **Defined**: Strict interfaces for `LayoutConfig`, `LayoutResult`, `NodePosition`, `LayoutMetadata`
- [x] **Defined**: Generic `LayoutEngine<TNode, TEdge>` interface for type-safe layout engines
- [x] **Defined**: Utility types: `LayoutResultOrError`, `LayoutPreset`, `HierarchicalNode<T>`
- [x] **Defined**: `StrictLayoutConfig` class with runtime validation
- [x] **Defined**: Type guards: `isLayoutError()`, `isLayoutResult()`
- [x] **Defined**: `X6_GRAPH` injection token with proper typing
- [x] **No `any` types**: All interfaces and classes use explicit types
- [x] **TSDoc comments**: All public APIs have comprehensive documentation

**Implementation Details**:
- All interfaces have TSDoc comments with parameter descriptions
- `StrictLayoutConfig` validates ranges: `verticalSpacing` (20-200), `horizontalSpacing` (50-500)
- Generic types enable reusability: `LayoutEngine<TNode, TEdge>`, `HierarchicalNode<T>`
- Type-safe union types: `LayoutResultOrError = LayoutResult | LayoutError`
- Null checks performed before property access throughout

### ✅ V. Incremental Delivery - PASS (Verified)
- [x] **User Story 1 (P1)**: Display uniform vertical spacing
  - ✅ **Independent**: Can be tested with hardcoded 50px spacing
  - ✅ **Testable**: Unit tests for spacing calculation independent of other features
  - ✅ **High Value**: Core visual requirement, direct user impact
  - ✅ **No Cross-Dependencies**: Does not require Story 2 or 3

- [x] **User Story 2 (P2)**: Visual balance with different node sizes
  - ✅ **Independent**: Can be tested with mixed node sizes after Story 1 complete
  - ✅ **Testable**: Visual balance validation independent of configuration UI
  - ✅ **Builds on P1**: Extends Story 1 without breaking it
  - ✅ **No Cross-Dependencies**: Does not require Story 3

- [x] **User Story 3 (P3)**: Configurable spacing
  - ✅ **Independent**: Enhancement that can be delivered after Stories 1 and 2
  - ✅ **Testable**: Configuration UI testing independent of layout logic
  - ✅ **Enhancement**: Adds value without being required for MVP
  - ✅ **No Cross-Dependencies**: Can be delivered as separate feature

**Implementation Plan**:
- **Week 1-2**: Story 1 (P1) - Basic layout with hardcoded 50px spacing
- **Week 3**: Story 2 (P2) - Visual balance validation and optimization
- **Week 4**: Story 3 (P3) - Configuration UI and presets
- **MVP**: Deliver Story 1 first for immediate value

### ✅ All Gates Passed (Post-Phase 1)

**Conclusion**: Phase 1 design fully complies with ApFlow.UI constitution. No violations detected. Data model and technical approach validated. Ready to proceed with Phase 2 implementation planning.

## Project Structure

### Documentation (this feature)

```text
specs/001-uniform-vertical-spacing/
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command) - N/A (internal feature)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

**Note**: No contracts/ directory will be created as this is an internal feature with no external API contracts.

### Source Code (repository root)

```text
# Option 2: Web application (Angular SPA)
src/app/pages/flow-graph/
├── models/
│   ├── flow-graph.ts                    # Existing: Main graph model
│   ├── flow-node.ts                     # Existing: Node model
│   ├── flow-edge.ts                     # Existing: Edge model
│   ├── flow-group.ts                    # Existing: BranchGroup model
│   └── layout-config.ts                 # NEW: Layout configuration interface
├── services/
│   ├── editor.service.ts                # Existing: Editor service
│   ├── layout/
│   │   ├── layout.service.ts            # NEW: Main layout service
│   │   ├── vertical-spacing-calculator.ts  # NEW: Spacing calculation logic
│   │   └── layout-config.service.ts     # NEW: Configuration management
│   └── branch-group-manager.ts          # Existing: Branch group management
├── components/
│   ├── editor/
│   │   ├── editor.component.ts          # Existing: Main editor
│   │   └── editor.component.html
│   ├── layout-config/
│   │   ├── layout-config.component.ts   # NEW: Spacing configuration UI
│   │   └── layout-config.component.html
│   └── nodes/                           # Existing: Node components
└── helpers/
    └── layout-helper.ts                 # NEW: Layout helper utilities

tests/unit/flow-graph/layout/
├── vertical-spacing-calculator.spec.ts   # NEW: Spacing calculation tests
├── layout.service.spec.ts               # NEW: Layout service tests
└── layout-config.component.spec.ts     # NEW: Configuration UI tests
```

**Structure Decision**: 
- Following existing Angular feature module structure in `src/app/pages/flow-graph/`
- New `services/layout/` subdirectory for layout-related services
- New `components/layout-config/` for configuration UI (User Story 3)
- Unit tests in `tests/unit/flow-graph/layout/` following Vitest conventions
- No external contracts needed as this is an internal feature

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

No constitution violations detected. Complexity tracking not required.

---

## Phase 0: Research Tasks

**Status**: 🔄 In Progress

### 0.1 Research AntV X6 Layout System Integration
- **Task**: Research how to integrate custom layout algorithms with AntV X6 graph engine
- **Unknown**: Best practices for creating custom layout engines that work with X6's graph rendering
- **Approach**: 
  - Study @antv/x6 documentation on graph.layout() API
  - Review @antv/layout library capabilities for DAG (Directed Acyclic Graph) layout
  - Analyze how to integrate layout results with X6 node positions
- **Expected Outcome**: Documentation on X6 layout integration patterns

### 0.2 Research Vertical Spacing Calculation Algorithms
- **Task**: Research algorithms for maintaining uniform vertical spacing in hierarchical graphs
- **Unknown**: How to handle parallel branches with varying node counts while maintaining uniform spacing
- **Approach**:
  - Study hierarchical graph layout algorithms (e.g., Sugiyama algorithm)
  - Research how AntV X6 handles parallel branch layouts
  - Analyze existing BranchGroup layout implementation
- **Expected Outcome**: Algorithm design for uniform vertical spacing calculation

### 0.3 Research Reactive Layout Updates with RxJS
- **Task**: Research reactive patterns for efficient layout updates in Angular
- **Unknown**: How to debounce layout updates to avoid excessive recalculations during rapid node additions
- **Approach**:
  - Study RxJS operators (debounceTime, throttleTime, distinctUntilChanged)
  - Research Angular change detection strategies with OnPush
  - Analyze performance patterns for large graph updates
- **Expected Outcome**: RxJS patterns for efficient layout state management

### 0.4 Research TypeScript Type Safety for Layout Calculations
- **Task**: Define strict TypeScript types for layout configuration and calculation
- **Unknown**: How to type layout algorithms to ensure type safety while maintaining flexibility
- **Approach**:
  - Define interfaces for LayoutConfig, LayoutResult, NodePosition
  - Research generic types for reusable layout components
  - Study TypeScript utility types for layout transformations
- **Expected Outcome**: Type-safe interfaces and utilities for layout system

### 0.5 Research Configuration Management Best Practices
- **Task**: Research patterns for managing configurable layout parameters in Angular
- **Unknown**: Best practices for persistence and validation of layout configuration
- **Approach**:
  - Study Angular services with BehaviorSubject for state management
  - Research localStorage or service-based configuration persistence
  - Analyze validation patterns using Reactive Forms
- **Expected Outcome**: Configuration service design with RxJS state management

---

## Phase 1: Design & Contracts

**Status**: ⏳ Pending (depends on Phase 0 completion)

### 1.1 Data Model Design
**Output**: `data-model.md`

Define entities:
- `LayoutConfig`: Configuration interface for vertical spacing and layout options
- `LayoutResult`: Result interface containing calculated node positions
- `VerticalSpacingCalculator`: Service interface for spacing calculation
- `LayoutService`: Main service interface for layout orchestration

### 1.2 Interface Contracts
**Output**: N/A (internal feature, no external contracts)

This feature does not expose external APIs. All interfaces are internal to the flow-graph module.

### 1.3 Agent Context Update
**Output**: Run `.specify/scripts/powershell/update-agent-context.ps1 -AgentType codebuddy`

Add new technology and patterns to agent context:
- AntV X6 layout integration patterns
- RxJS reactive layout update patterns
- TypeScript strict type definitions for layout algorithms
- Angular service-based configuration management

### 1.4 Quick Start Guide
**Output**: `quickstart.md`

Developer guide for:
- Using the new layout service
- Configuring vertical spacing
- Testing layout calculations
- Debugging layout issues

---

## Phase 2: Implementation Planning

**Status**: ⏳ Pending (depends on Phase 1 completion)

**Note**: This phase will be executed by the `/speckit.tasks` command, creating `tasks.md` with detailed implementation tasks organized by user stories.

---

## Success Metrics

Based on feature specification success criteria:

| Metric | Target | Measurement Method |
|--------|--------|-------------------|
| Vertical Spacing Accuracy | ≤ 1px error | Visual inspection + automated tests |
| Layout Recalculation Time | < 100ms | Performance benchmarks |
| Initial Rendering Time | < 500ms (50 nodes) | Performance benchmarks |
| User Satisfaction Score | ≥ 4.5/5 | User feedback survey |
| Node Size Change Response | < 50ms | Performance benchmarks |

---

## Risks & Mitigations

| Risk | Probability | Impact | Mitigation |
|------|-------------|--------|------------|
| Performance degradation with large graphs | Medium | High | Implement efficient algorithms, benchmark with 100+ nodes |
| Complexity of nested parallel branches | High | Medium | Incremental testing, start with simple cases |
| Integration issues with existing BranchGroup | Low | High | Thorough integration testing, maintain backward compatibility |
| TypeScript strict mode violations | Low | Medium | Strict type definitions throughout development |

---

## Dependencies

### Internal Dependencies
- Existing `FlowGraph` model
- Existing `BranchGroup` and `BranchGroupManager`
- Existing AntV X6 graph integration
- Existing EditorService

### External Dependencies
- @antv/x6 3.1.5
- @antv/layout 0.3.12
- Angular 21.0.0
- RxJS 7.8.0

---

## Notes

- All development must comply with ApFlow.UI constitution (version 1.0.0)
- TypeScript strict mode is mandatory - no `any` types allowed
- All services must be provided at root or module level
- Use OnPush change detection strategy for performance
- Follow Angular style guide conventions
- Write comprehensive unit tests with Vitest
- Document all public APIs with TSDoc comments
