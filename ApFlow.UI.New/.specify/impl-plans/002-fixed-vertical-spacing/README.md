# Implementation Plan: Fixed Vertical Spacing Layout Engine

**Feature ID**: 002-fixed-vertical-spacing  
**Branch**: 002-fixed-vertical-spacing  
**Status**: ✅ Phase 2 Complete - Planning  
**Last Updated**: 2026-03-04

---

## Quick Links

- 📋 **[Full Implementation Plan](./plan.md)** - Complete planning document with tasks and timelines
- 🔬 **[Research Document](./research.md)** - Research findings and design decisions
- 📊 **[Data Model](./data-model.md)** - Entity definitions and relationships
- 📄 **[Interface Contracts](./contracts/layout-engine-contract.md)** - Public API specifications
- 🚀 **[Quick Start Guide](./quickstart.md)** - Developer onboarding guide
- 📖 **[Feature Specification](../../../specs/002-fixed-vertical-spacing/spec.md)** - Original requirements
- 📜 **[Project Constitution](../../../memory/constitution.md)** - Development principles

---

## Executive Summary

This implementation plan details the development of a custom layout engine that enforces **strict 50px vertical spacing** between consecutive node levels in the ApFlow.UI flow graph editor, replacing the current `DagreLayout` implementation which violates the project constitution.

### Key Objectives

- ✅ Replace `DagreLayout` with custom `FlowLayoutEngine` service
- ✅ Enforce fixed 50px vertical spacing: `y = levelIndex * 50 + baseOffset`
- ✅ Implement BFS-based level assignment algorithm
- ✅ Achieve sub-100ms layout calculation for 100-node graphs
- ✅ Maintain backward compatibility with existing functionality
- ✅ Provide type-safe interfaces with no `any` types

### Current Status

| Phase | Status | Deliverables |
|-------|--------|--------------|
| Phase 0: Research | ✅ Complete | research.md (274 lines) |
| Phase 1: Design & Contracts | ✅ Complete | data-model.md, contracts/, quickstart.md |
| Phase 2: Planning | ✅ Complete | plan.md (567 lines) |
| Phase 3: Implementation | ⏳ Pending | 9 tasks, 22 hours estimated |
| Phase 4: Testing | ⏳ Pending | Unit tests, integration tests, visual regression |
| Phase 5: Deployment | ⏳ Pending | Code review, merge, release notes |

---

## Architecture Overview

### Current Implementation (Non-Compliant)

```typescript
// editor.service.ts:61-92
const dagreLayout = new DagreLayout({
  type: 'dagre',
  rankdir: 'TB',
  ranksep: 35,          // ❌ Violates 50px rule
  nodesep: 75,
});
```

**Issues**:
- `ranksep: 35` does NOT enforce strict vertical spacing
- Spacing varies based on node dimensions
- Violates Project Constitution Principle 3

### Target Implementation (Compliant)

```typescript
@Injectable({ providedIn: 'root' })
export class FlowLayoutEngine implements ILayoutEngine {
  private readonly VERTICAL_SPACING = 50; // Fixed per constitution
  
  layout(graph: FlowGraph, config?: ILayoutConfig): ILayoutResult {
    const levels = this.assignLevels(graph);           // BFS
    return this.calculatePositions(graph, levels, config);
  }
}
```

**Benefits**:
- ✅ Strict 50px vertical spacing enforced
- ✅ Type-safe interfaces (no `any` types)
- ✅ RxJS integration with debouncing
- ✅ Caching for performance
- ✅ Parallel branch support

---

## Technical Highlights

### 1. BFS Level Assignment Algorithm

- **Complexity**: O(V + E) where V = nodes, E = edges
- **Approach**: Breadth-First Search starting from root nodes
- **Parallel Branches**: Treated as logical units via `BranchGroupManager`

### 2. Strict Vertical Spacing

- **Formula**: `y = levelIndex * 50 + baseOffset`
- **Enforcement**: Literal type `verticalSpacing: 50` (cannot be changed)
- **Centering**: `finalY = layoutY - height / 2`

### 3. RxJS Performance Optimization

- **Debouncing**: `debounceTime(16)` ≈ 60fps
- **Cancellation**: `switchMap` cancels previous calculations
- **Caching**: `Map<string, ILayoutResult>` stores results

### 4. Type Safety

- **Interfaces**: `ILayoutConfig`, `ILayoutResult`, `INodePosition`
- **No `any` types**: All public APIs fully typed
- **Error Types**: `LayoutError` with `LayoutErrorCode` enum

---

## Constitution Compliance

| Principle | Status | Notes |
|-----------|--------|-------|
| **Principle 1: Type Safety First** | ✅ PASS | Strict TypeScript, no `any` types |
| **Principle 2: Component Architecture** | ✅ PASS | Service follows Angular patterns |
| **Principle 3: Layout Performance** | ✅ PASS | Fixed 50px spacing enforced |
| **Principle 4: Code Consistency** | ✅ PASS | Prettier, naming conventions |
| **Principle 5: Form Safety** | N/A | Not applicable |
| **Principle 6: RxJS Patterns** | ✅ PASS | Typed observables, proper cleanup |

**Overall**: ✅ **FULLY COMPLIANT**

---

## Implementation Tasks

### Task Breakdown (22 hours total)

| ID | Task | Time | Dependencies |
|----|------|------|--------------|
| 1 | Create Layout Engine Service | 4h | None |
| 2 | Create Layout Models | 2h | None |
| 3 | Update EditorService | 2h | 1, 2 |
| 4 | Implement RxJS Integration | 2h | 1, 3 |
| 5 | Add Unit Tests | 4h | 1, 2 |
| 6 | Add Integration Tests | 3h | 3, 4 |
| 7 | Update Documentation | 1h | All |
| 8 | Code Review and Refinement | 2h | All |
| 9 | Visual Regression Testing | 2h | All |

### Files to Create

```
src/app/pages/flow-graph/
├── services/
│   ├── flow-layout-engine.service.ts       # NEW
│   ├── flow-layout-engine.service.spec.ts  # NEW
│   ├── editor.service.ts                   # MODIFY
│   └── editor.service.spec.ts              # MODIFY
├── models/
│   └── layout.models.ts                    # NEW
└── README.md                               # MODIFY
```

---

## Success Criteria

### Quantitative Metrics

- ✅ **100%** of consecutive level pairs maintain exactly 50px vertical spacing
- ✅ **< 100ms** layout calculation for 100-node graphs
- ✅ **60fps** rendering performance during updates
- ✅ **95%** test coverage for layout engine code

### Qualitative Outcomes

- ✅ Users can predictably understand flow progression
- ✅ Graph hierarchy is immediately apparent
- ✅ Fully compliant with Project Constitution Principle 3
- ✅ No breaking changes to existing functionality

---

## Gate Evaluation Results

| Gate | Status | Rationale |
|------|--------|-----------|
| Gate 1: Constitution Compliance | ✅ PASSED | All principles fully compliant |
| Gate 2: Technical Feasibility | ✅ PASSED | Well-understood algorithms, no unknowns |
| Gate 3: Performance Requirements | ✅ PASSED | O(V + E) complexity, well under 100ms target |
| Gate 4: Backward Compatibility | ✅ PASSED | API unchanged, migration path documented |
| Gate 5: Test Coverage | ✅ PASSED | 95% target specified, comprehensive tests planned |
| Gate 6: Timeline & Resources | ✅ PASSED | Reasonable effort, no blockers |

**Overall**: ✅ **ALL GATES PASSED** - Ready to proceed to implementation.

---

## Getting Started

### For Developers

1. **Read the Quick Start Guide**: [quickstart.md](./quickstart.md)
   - Learn how to use the layout engine
   - See code examples and patterns
   - Troubleshooting tips

2. **Review the Data Model**: [data-model.md](./data-model.md)
   - Understand entity definitions
   - Learn validation rules
   - See type definitions

3. **Check the Interface Contract**: [contracts/layout-engine-contract.md](./contracts/layout-engine-contract.md)
   - Public API specifications
   - Error handling
   - RxJS integration

### For Project Managers

1. **Review the Full Plan**: [plan.md](./plan.md)
   - Detailed task breakdown
   - Timeline estimates
   - Risk mitigation strategies

2. **Check Gate Evaluation**: [plan.md#gate-evaluation](./plan.md#gate-evaluation)
   - Constitution compliance
   - Technical feasibility
   - Resource requirements

### For Code Reviewers

1. **Review the Research**: [research.md](./research.md)
   - Design decisions and rationale
   - Alternatives considered
   - Trade-offs made

2. **Check Type Safety**: [data-model.md](./data-model.md)
   - Interface definitions
   - Validation rules
   - No `any` types

---

## Related Documents

| Document | Path | Description |
|----------|------|-------------|
| Feature Specification | `specs/002-fixed-vertical-spacing/spec.md` | Original requirements and success criteria |
| Project Constitution | `memory/constitution.md` | Development principles (especially Principle 3) |
| Current Implementation | `src/app/pages/flow-graph/services/editor.service.ts` | Non-compliant DagreLayout usage |
| Branch Group Manager | `src/app/pages/flow-graph/models/branch-group-manager.ts` | Parallel branch handling |

---

## Glossary

| Term | Definition |
|------|------------|
| **BFS** | Breadth-First Search - graph traversal algorithm for level assignment |
| **DAG** | Directed Acyclic Graph - graph with directed edges and no cycles |
| **Level** | Horizontal layer in hierarchical layout; all nodes share same Y baseline |
| **Vertical Spacing** | Distance between baseline Y-coordinates of consecutive levels (fixed at 50px) |
| **Horizontal Spacing** | Distance between adjacent nodes in same level (default 75px) |
| **Layout Engine** | Service that calculates optimal node positions in graph |
| **SwitchMap** | RxJS operator that cancels previous observable when new one arrives |
| **Debounce** | Technique to delay processing until input stabilizes |

---

## FAQ

### Why replace DagreLayout?

**Answer**: `DagreLayout` uses `ranksep` parameter which varies spacing based on node dimensions, violating the project constitution's strict 50px vertical spacing requirement. The custom `FlowLayoutEngine` enforces fixed spacing regardless of node size.

### Why use BFS for level assignment?

**Answer**: BFS naturally assigns levels by visiting nodes layer by layer, guaranteeing that all children of a node are at level + 1. It's simple, efficient (O(V + E)), and handles parallel branches correctly.

### How is performance ensured?

**Answer**: 
- BFS complexity: O(V + E) → ~10ms for 100-node graphs
- RxJS `debounceTime(16)` prevents excessive recalculations
- Caching eliminates redundant computations
- Web Worker can be added later if needed

### Is this backward compatible?

**Answer**: Yes. The public API of `EditorService.renderGraph()` remains unchanged. The internal implementation is replaced without affecting external consumers.

### What about parallel branches?

**Answer**: The layout engine integrates with `BranchGroupManager` to treat parallel branches as logical units, ensuring all nodes in a branch are at the same level with 50px spacing.

---

## Next Steps

1. ✅ **Review and Approve Plan** (Current)
   - Stakeholder review
   - Approve or request changes

2. ⏳ **Proceed to Phase 3: Implementation**
   - Execute tasks 1-9 in dependency order
   - Update task status as work progresses

3. ⏳ **Phase 4: Testing**
   - Unit tests (95% coverage target)
   - Integration tests
   - Visual regression testing

4. ⏳ **Phase 5: Deployment**
   - Code review
   - Merge to main
   - Update documentation

---

## Contact & Support

| Role | Contact |
|------|---------|
| Feature Owner | ApFlow.UI Development Team |
| Architecture Review | Architecture Team |
| Questions | Open issue on project repository |

**Feature ID**: 002-fixed-vertical-spacing  
**Branch**: 002-fixed-vertical-spacing  
**Last Updated**: 2026-03-04

---

**Ready to implement! 🚀**
