# Implementation Plan: Fixed Vertical Spacing Layout Engine

**Feature ID**: 002-fixed-vertical-spacing  
**Status**: Phase 2 Complete - Planning  
**Branch**: 002-fixed-vertical-spacing  
**Last Updated**: 2026-03-04

---

## Quick Links

- 📖 [Full Implementation Plan](./plan.md)
- 🔬 [Research Document](./research.md)
- 📊 [Data Model](./data-model.md)
- 📄 [Interface Contract](./contracts/layout-engine-contract.md)
- 🚀 [Quick Start Guide](./quickstart.md)
- ✅ [Feature Specification](../../../specs/002-fixed-vertical-spacing/spec.md)

---

## Executive Summary

This implementation plan addresses **Project Constitution Principle 3** (Layout Performance Optimization) by replacing the current `DagreLayout` implementation with a custom layout engine that enforces **strict 50px vertical spacing** between consecutive node levels.

### The Problem

Current implementation violates the constitution:
```typescript
// ❌ Current (NON-COMPLIANT)
const dagreLayout = new DagreLayout({
  ranksep: 35,  // Violates 50px rule!
});
```

### The Solution

Create a custom layout engine with guaranteed 50px spacing:
```typescript
// ✅ New (COMPLIANT)
const layoutEngine = new FlowLayoutEngine();
const result = layoutEngine.layout(flowGraph, {
  verticalSpacing: 50,  // Enforced via literal type
});
```

---

## Architecture Overview

```
┌─────────────────────────────────────────────────────────┐
│                    EditorService                         │
│  - Manages FlowGraph state                               │
│  - Triggers layout recalculation                         │
│  - Applies layout to X6 graph                            │
└────────────────────┬────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────┐
│                 FlowLayoutEngine                         │
│  - BFS level assignment (O(V + E))                      │
│  - Position calculation with strict 50px spacing         │
│  - Automatic result caching                             │
│  - Type-safe API (no `any` types)                       │
└────────────────────┬────────────────────────────────────┘
                     │
         ┌───────────┴───────────┐
         ▼                       ▼
┌────────────────┐      ┌────────────────┐
│ BranchGroupMgr │      │  LayoutCache   │
│ - Parallel     │      │  - Map based   │
│   branches     │      │  - Auto        │
└────────────────┘      │    invalidation │
                       └────────────────┘
```

---

## Technical Highlights

### 1. Synchronous Layout Calculation

- **Approach**: Direct method calls, no async/RxJS complexity
- **Performance**: < 10ms for 100-node graphs (O(V + E))
- **Benefits**: Simple, predictable, easy to test and debug

### 2. Strict Type Safety

- ✅ No `any` types in public API
- ✅ Literal type for `verticalSpacing: 50`
- ✅ Comprehensive interfaces with TSDoc
- ✅ TypeScript strict mode compliant

### 3. Automatic Caching

- Cache key based on graph structure
- Automatic invalidation on changes
- Significant performance boost for repeated calculations

### 4. Parallel Branch Support

- Seamless integration with `BranchGroupManager`
- All nodes in a branch at same level
- Merge nodes at next level

---

## Compliance Summary

| Principle | Status | Evidence |
|-----------|--------|----------|
| **Principle 1**: Type Safety First | ✅ PASS | Strict TypeScript, no `any` types |
| **Principle 2**: Component Architecture | ✅ PASS | Service-based, singleton provider |
| **Principle 3**: Layout Performance | ✅ PASS | Enforces 50px spacing |
| **Principle 4**: Code Consistency | ✅ PASS | Prettier, TSDoc, naming conventions |
| **Principle 5**: Form Safety | N/A | Not applicable |
| **Principle 6**: RxJS Patterns | N/A | Not required for this implementation |

**Overall**: ✅ **FULLY COMPLIANT**

---

## Deliverables

### Phase 0: Research ✅

- [x] **research.md** (487 lines)
  - Custom layout engine architecture
  - BFS level assignment algorithm
  - Horizontal layout strategy
  - Synchronous calculation approach
  - TypeScript type safety strategy
  - Integration with `BranchGroupManager`
  - Caching strategy
  - Error handling and validation
  - Testing strategy
  - Backward compatibility approach

### Phase 1: Design & Contracts ✅

- [x] **data-model.md** (609 lines)
  - Entity definitions and relationships
  - Validation rules and constraints
  - State transitions
  - Constitution compliance mapping

- [x] **contracts/layout-engine-contract.md** (522 lines)
  - Public API specifications
  - Type definitions
  - Error handling contract
  - Layout caching contract
  - Testing contract
  - Migration guide

- [x] **quickstart.md** (635 lines)
  - Developer quick start guide
  - Basic usage examples
  - Configuration options
  - Error handling
  - Advanced usage
  - Testing examples
  - Performance optimization tips

### Phase 2: Planning ✅

- [x] **plan.md** (Updated - 22 tasks, 22 hours)
  - Detailed implementation tasks
  - Dependencies and time estimates
  - Risk mitigation strategies
  - Success criteria

---

## Implementation Tasks (22 Hours Total)

| ID | Task | Time | Dependencies |
|----|------|------|--------------|
| 1 | Create Layout Engine Service | 4h | None |
| 2 | Create Layout Models | 2h | None |
| 3 | Update EditorService | 2h | 1, 2 |
| 4 | Implement Layout Caching | 2h | 1, 2 |
| 5 | Add Unit Tests | 4h | 1, 2 |
| 6 | Add Integration Tests | 3h | 3, 4 |
| 7 | Update Documentation | 1h | All |
| 8 | Code Review and Refinement | 2h | All |
| 9 | Visual Regression Testing | 2h | All |
| **Total** | **22 hours** | - |

---

## Files to Create

```
src/app/pages/flow-graph/
├── services/
│   ├── flow-layout-engine.service.ts       # NEW - Main layout engine
│   ├── flow-layout-engine.service.spec.ts  # NEW - Unit tests
│   ├── editor.service.ts                   # MODIFY - Replace DagreLayout
│   └── editor.service.spec.ts              # MODIFY - Integration tests
└── models/
    └── layout.models.ts                    # NEW - Layout types and interfaces
```

---

## Success Criteria

**Quantitative**:
- ✅ 100% of consecutive level pairs maintain exactly 50px vertical spacing
- ✅ Layout calculation time: < 100ms for 100-node graphs
- ✅ Rendering performance: Maintains 60fps
- ✅ Test coverage: 95% for layout engine code

**Qualitative**:
- ✅ Users can predictably understand flow progression
- ✅ Graph hierarchy is immediately apparent
- ✅ Fully compliant with Project Constitution Principle 3
- ✅ No breaking changes to existing functionality

---

## Key Technical Decisions

| Decision | Rationale |
|----------|-----------|
| **Standalone FlowLayoutEngine service** | Modular, testable, follows Angular architecture |
| **BFS for level assignment** | Simple, correct, handles parallel branches |
| **Greedy horizontal layout** | Sufficient for typical flows, maintainable |
| **Synchronous layout calculation** | Simple, fast, predictable, easy to test |
| **Strict TypeScript interfaces** | Type safety per Principle 1 |
| **Integrate with BranchGroupManager** | Minimal changes, parallel support |
| **Automatic result caching** | Avoid redundant calculations |
| **Defensive error handling** | User-friendly, graceful degradation |
| **Comprehensive test suite** | 95% coverage target |
| **Maintain backward compatibility** | Seamless migration |

---

## Next Steps

### Option 1: Start Implementation

Execute tasks in order of dependency:

```bash
# Task 1: Create layout models (2h)
# Create src/app/pages/flow-graph/models/layout.models.ts

# Task 2: Create layout engine service (4h)
# Create src/app/pages/flow-graph/services/flow-layout-engine.service.ts

# Task 3: Update EditorService (2h)
# Replace DagreLayout with FlowLayoutEngine

# Task 4: Implement caching (2h)
# Add Map-based cache to FlowLayoutEngine

# Tasks 5-9: Testing and refinement (12h)
# Unit tests, integration tests, documentation, review, visual testing
```

### Option 2: Review Documentation

Before implementation, review the detailed documents:

- 📖 [Full Implementation Plan](./plan.md) - Complete task breakdown
- 🔬 [Research Document](./research.md) - Algorithm details and decisions
- 📊 [Data Model](./data-model.md) - Entity definitions
- 📄 [Interface Contract](./contracts/layout-engine-contract.md) - API specifications
- 🚀 [Quick Start Guide](./quickstart.md) - Developer guide

### Option 3: Clarify Questions

If you have questions about the implementation plan, please raise them before starting development.

---

## FAQ

### Q: Why not use RxJS for layout calculations?

**A**: The layout calculation is inherently fast (< 10ms for 100-node graphs) and deterministic. Adding RxJS complexity would be over-engineering for this use case. The synchronous approach with automatic caching provides:
- Simpler code (easier to understand and maintain)
- Predictable performance (no async overhead)
- Easier testing (no mocking of Observables)
- Same performance benefits (caching eliminates redundant calculations)

### Q: What if we need reactive updates in the future?

**A**: The layout engine is designed to be flexible. If reactive updates become necessary in the future:
1. The current synchronous API can be wrapped in RxJS operators
2. The caching mechanism can be integrated with RxJS `distinctUntilChanged`
3. The underlying algorithm remains unchanged

### Q: How does caching work with graph structure changes?

**A**: Cache keys are generated from the sorted list of node IDs and edge pairs. Any change to the graph structure (added/removed nodes or edges) generates a new cache key, automatically invalidating the old result.

### Q: Will this break existing functionality?

**A**: No. The implementation maintains:
- `EditorService.renderGraph()` signature
- `graph.fromJSON()` input format
- Node event subscriptions
- Branch group manager integration
- All existing node types

---

## Contact & Support

| Role | Team |
|------|------|
| Feature Owner | ApFlow.UI Development Team |
| Architecture Review | Architecture Team |
| Code Review | Peer Developers |
| QA Review | QA Team |

---

**Document Version**: 1.1.0  
**Status**: Phase 2 Complete - Planning  
**Next Phase**: Phase 3 - Implementation
