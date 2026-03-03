# Feature: Fixed Vertical Spacing for Flow Graph Layout

## Quick Summary

**Status**: Ready for Planning ✅
**Branch**: `002-fixed-vertical-spacing`
**Created**: 2026-03-04

### What is this feature?

This feature fixes a critical visual inconsistency in the flow graph layout where vertical spacing between nodes varies based on node heights. It ensures **all nodes maintain a consistent 50px vertical spacing** regardless of their individual sizes.

### Why is it needed?

- **Current Problem**: The existing `DagreLayout` with `ranksep: 35` creates irregular gaps between nodes of different sizes
- **User Impact**: Visual hierarchy is difficult to follow; users get distracted by inconsistent spacing
- **Constitution Compliance**: Required by Project Constitution Principle 3 (Layout Performance Optimization)

### What will it do?

1. Replace `DagreLayout` with a custom layout engine
2. Enforce fixed 50px vertical spacing using formula: `y = levelIndex * 50 + baseOffset`
3. Apply spacing consistently regardless of node height (20px → 200px nodes all use same spacing)
4. Maintain performance: <100ms layout calculation for 100-node graphs

### Key Success Metrics

- ✅ 100% of level pairs maintain exactly 50px vertical spacing
- ✅ Zero variance regardless of node size
- ✅ Layout completes in <100ms for 100-node graphs
- ✅ Maintains 60fps rendering performance

---

## Specification Details

### Main Documents

- **[Specification](./spec.md)** - Complete feature requirements, user scenarios, success criteria
- **[Quality Checklist](./checklists/requirements.md)** - Validation results showing spec is complete

### Functional Requirements (FR)

| ID | Description | Acceptance Criteria |
|----|-------------|---------------------|
| FR-1 | Fixed Vertical Spacing | Y = levelIndex × 50 + baseOffset, enforced for all nodes |
| FR-2 | Level Assignment | BFS algorithm assigns levels; parallel branches at same level |
| FR-3 | Position Calculation | Center nodes: x = layoutX - width/2, y = layoutY - height/2 |
| FR-4 | Layout Engine | Replace DagreLayout with custom implementation |
| FR-5 | Performance | <100ms calculation, 16ms debounce, switchMap for cancellation |
| FR-6 | Visual Consistency | Same baseline Y for all nodes in same level |
| FR-7 | Error Handling | User-friendly errors, cycle detection, validation |

### User Scenarios

1. **Opening flow graph** → All nodes display with uniform 50px vertical spacing
2. **Adding nodes of varying sizes** → Spacing remains 50px regardless of node height
3. **Rendering graph** → Positions calculated with strict formula enforcement
4. **Viewing parallel branches** → All parallel nodes maintain 50px spacing

### Edge Cases Covered

- Extreme height variations (20px ↔ 200px nodes)
- Empty levels (gaps after node deletion)
- Single-level graphs
- Deeply nested hierarchies (10+ levels)
- Large graphs (100+ nodes)
- Rapid consecutive updates (debounce optimization)

---

## Out of Scope

- Horizontal spacing customization (fixed at 75px default)
- Node size normalization (only spacing is fixed, sizes unchanged)
- Layout change animations (can be added later)
- User-configurable vertical spacing (fixed at 50px per constitution)
- Cycle detection/resolution (assumes DAG structure)
- Edge routing optimization (focus is node positioning)

---

## Dependencies

### Internal
- **Project Constitution Principle 3** - Defines 50px vertical spacing requirement
- **EditorService** - Current service managing graph rendering
- **FlowGraph/FlowNode Models** - Data models for graph structure
- **BranchGroupManager** - Parallel branch grouping

### External
- **AntV X6 3.1+** - Graph rendering library
- **@antv/layout 0.3+** - Layout library (reference)
- **RxJS** - Reactive patterns for performance
- **Angular 19+** - Component framework
- **TypeScript 5.7+** - Strict mode required

---

## Risks & Mitigations

| Risk | Likelihood | Impact | Mitigation |
|------|------------|--------|------------|
| Performance degradation | Medium | High | Caching, debounce, consider Web Worker |
| Incorrect level assignment | Medium | High | Comprehensive unit testing |
| Breaking existing interactions | Low | High | Maintain backward compatibility |
| Regression in positioning | Low | Medium | Visual regression testing |
| BranchGroupManager integration | Low | Medium | Integration testing |

---

## Next Steps

1. **Review Specification** - Read [spec.md](./spec.md) for complete details
2. **Verify Checklist** - Review [requirements.md](./checklists/requirements.md) for validation results
3. **Proceed to Planning** - Run `/speckit.plan` to create implementation plan
4. **Or Clarify First** - If needed, run `/speckit.clarify` to resolve any questions

---

## Files Created

```
.specify/specs/002-fixed-vertical-spacing/
├── spec.md                    # Complete feature specification
├── README.md                  # This file - quick overview
└── checklists/
    └── requirements.md        # Quality validation results
```

---

## Constitution Reference

This feature directly addresses **Project Constitution Principle 3: Layout Performance Optimization**, which states:

> **Vertical spacing between consecutive levels MUST be exactly 50px, regardless of individual node heights.**
> Node Y-coordinates MUST be calculated as: `y = levelIndex * 50 + baseOffset`

The current implementation in `editor.service.ts:61-92` is flagged as **non-compliant** because it uses `DagreLayout` with `ranksep: 35`, which varies spacing based on node dimensions.

---

**Ready for next phase: /speckit.plan** ✅
