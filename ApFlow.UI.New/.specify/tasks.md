# Implementation Tasks: Fixed Vertical Spacing Layout Engine

**Feature ID**: 002-fixed-vertical-spacing  
**Branch**: 002-fixed-vertical-spacing  
**Date Generated**: 2026-03-04  
**Status**: Ready for Implementation

---

## Table of Contents

1. [Phase 1: Setup](#phase-1-setup)
2. [Phase 2: Foundational](#phase-2-foundational)
3. [Phase 3: Core Layout Engine](#phase-3-core-layout-engine)
4. [Phase 4: Integration](#phase-4-integration)
5. [Phase 5: Testing](#phase-5-testing)
6. [Phase 6: Polish & Documentation](#phase-6-polish--documentation)
7. [Dependencies](#dependencies)
8. [Parallel Execution Examples](#parallel-execution-examples)
9. [Implementation Strategy](#implementation-strategy)

---

## Phase 1: Setup

**Goal**: Initialize project structure and verify prerequisites

**Independent Test Criteria**: Project structure ready, TypeScript strict mode verified

---

- [ ] T001 Verify TypeScript strict mode is enabled in tsconfig.json at d:/My Respository/ApprovalProcess/ApFlow.UI.New/tsconfig.json
- [ ] T002 Verify AntV X6 is installed in package.json (version 3.1+)
- [ ] T003 Verify Angular 19+ is installed in package.json
- [ ] T004 Verify ng-zorro-antd is installed in package.json (version 21+)
- [ ] T005 Create directory structure for layout models at d:/My Respository/ApprovalProcess/ApFlow.UI.New/src/app/pages/flow-graph/models/

---

## Phase 2: Foundational

**Goal**: Create data models and type definitions required by all user stories

**Independent Test Criteria**: All type definitions compile without errors, factory functions validate correctly

---

- [ ] T006 [P] Create layout.models.ts file at d:/My Respository/ApprovalProcess/ApFlow.UI.New/src/app/pages/flow-graph/models/layout.models.ts
- [ ] T007 Define ILayoutConfig interface with verticalSpacing: 50 literal type in layout.models.ts
- [ ] T008 Define INodePosition interface with centering offset constraints in layout.models.ts
- [ ] T009 Define ILayoutResult interface with nodePositions Map in layout.models.ts
- [ ] T010 Define INodeLevel interface with baseY calculation in layout.models.ts
- [ ] T011 Define ILayoutEngine interface with layout(), assignLevels(), calculatePositions() methods in layout.models.ts
- [ ] T012 Define ILayoutCache interface in layout.models.ts
- [ ] T013 Define LayoutErrorCode enum with all error codes in layout.models.ts
- [ ] T014 Define LayoutError class extending Error in layout.models.ts
- [ ] T015 Export DEFAULT_LAYOUT_CONFIG constant with verticalSpacing: 50 in layout.models.ts
- [ ] T016 Implement createNodePosition() factory function with validation in layout.models.ts
- [ ] T017 Implement createNodeLevel() factory function with validation in layout.models.ts
- [ ] T018 Implement createLayoutResult() factory function with validation in layout.models.ts
- [ ] T019 Export all types and interfaces from layout.models.ts
- [ ] T020 Add TSDoc comments to all public interfaces and types in layout.models.ts

---

## Phase 3: Core Layout Engine

**Story Goal**: Implement the FlowLayoutEngine service with BFS level assignment and fixed vertical spacing

**Independent Test Criteria**: Layout engine calculates correct positions with 50px spacing for test graphs

---

### User Story: Core Layout Engine

**Priority**: P1 (Critical)  
**Description**: Replace DagreLayout with custom layout engine enforcing 50px vertical spacing

---

- [ ] T021 Create flow-layout-engine.service.ts file at d:/My Respository/ApprovalProcess/ApFlow.UI.New/src/app/pages/flow-graph/services/flow-layout-engine.service.ts
- [ ] T022 Add @Injectable({ providedIn: 'root' }) decorator to FlowLayoutEngine class
- [ ] T023 Implement FlowLayoutEngine class implementing ILayoutEngine interface
- [ ] T024 Implement layout() method with cache lookup in flow-layout-engine.service.ts
- [ ] T025 Implement graph validation in layout() method (check null, empty, valid FlowGraph instance)
- [ ] T026 Implement assignLevels() method with BFS algorithm starting from root nodes in flow-layout-engine.service.ts
- [ ] T027 Implement cycle detection in assignLevels() method and throw LayoutError if detected
- [ ] T028 Implement calculatePositions() method with strict 50px vertical spacing in flow-layout-engine.service.ts
- [ ] T029 Implement centering offset calculation (x = layoutX - width/2, y = layoutY - height/2) in calculatePositions()
- [ ] T030 Implement horizontal layout with greedy algorithm using config.horizontalSpacing in calculatePositions()
- [ ] T031 Implement LayoutCache class with Map<string, ILayoutResult> storage in flow-layout-engine.service.ts
- [ ] T032 Implement cache key generation based on graph structure (sorted node IDs + sorted edge pairs)
- [ ] T033 Implement cache lookup before layout calculation in layout() method
- [ ] T034 Implement cache storage after successful layout calculation in layout() method
- [ ] T035 Implement clearCache() public method in FlowLayoutEngine class
- [ ] T036 Implement getCacheStats() public method in FlowLayoutEngine class
- [ ] T037 Add error handling with LayoutError for all error codes (INVALID_GRAPH, CYCLE_DETECTED, etc.)
- [ ] T038 Add TSDoc comments to all public methods in flow-layout-engine.service.ts
- [ ] T039 Add inline comments explaining BFS algorithm complexity (O(V + E))
- [ ] T040 Verify FlowLayoutEngine compiles without TypeScript errors

---

## Phase 4: Integration

**Story Goal**: Integrate FlowLayoutEngine into EditorService, replacing DagreLayout

**Independent Test Criteria**: EditorService renders graphs with 50px vertical spacing, no breaking changes

---

### User Story: EditorService Integration

**Priority**: P1 (Critical)  
**Description**: Replace DagreLayout with FlowLayoutEngine in EditorService.renderGraph()

---

- [ ] T041 Read existing editor.service.ts at d:/My Respository/ApprovalProcess/ApFlow.UI.New/src/app/pages/flow-graph/services/editor.service.ts
- [ ] T042 Import FlowLayoutEngine from './flow-layout-engine.service' in editor.service.ts
- [ ] T043 Import ILayoutConfig and ILayoutResult types from '../models/layout.models' in editor.service.ts
- [ ] T044 Inject FlowLayoutEngine in EditorService constructor
- [ ] T045 Remove import { DagreLayout } from '@antv/layout' from editor.service.ts
- [ ] T046 Locate renderGraph() method and replace DagreLayout instantiation
- [ ] T047 Replace DagreLayout.layout() call with flowLayoutEngine.layout() call in renderGraph()
- [ ] T048 Update renderGraph() to use ILayoutResult format instead of DagreLayout format
- [ ] T049 Ensure backward compatibility with graph.fromJSON() method
- [ ] T050 Add try-catch block around layout calculation in renderGraph()
- [ ] T051 Add user-friendly error message for LayoutError in catch block
- [ ] T052 Verify renderGraph() method signature unchanged (no breaking changes)
- [ ] T053 Test EditorService compilation without errors
- [ ] T054 Verify existing event subscriptions are preserved in renderGraph()

---

## Phase 5: Testing

**Story Goal**: Achieve 95% test coverage for layout engine and integration

**Independent Test Criteria**: All tests pass, coverage report shows 95%+ for layout engine code

---

### User Story: Unit Tests

**Priority**: P1 (Critical)  
**Description**: Comprehensive unit tests for FlowLayoutEngine

---

- [ ] T055 Create flow-layout-engine.service.spec.ts file at d:/My Respository/ApprovalProcess/ApFlow.UI.New/src/app/pages/flow-graph/services/flow-layout-engine.service.spec.ts
- [ ] T056 [P] Write test for layout() method with valid linear graph (3 nodes in sequence)
- [ ] T057 [P] Write test for layout() method with valid parallel branches (2 parallel branches)
- [ ] T058 [P] Write test for layout() method with null graph (expect LayoutError with INVALID_GRAPH)
- [ ] T059 [P] Write test for layout() method with empty graph (expect LayoutError with INVALID_GRAPH)
- [ ] T060 [P] Write test for assignLevels() method with simple graph (root + 2 children)
- [ ] T061 [P] Write test for assignLevels() method with parallel branches (all branch nodes at same level)
- [ ] T062 [P] Write test for assignLevels() method with cycle (expect LayoutError with CYCLE_DETECTED)
- [ ] T063 [P] Write test for calculatePositions() method with nodes of varying heights (20px, 100px, 200px)
- [ ] T064 [P] Write test for calculatePositions() method verifying 50px vertical spacing between all consecutive levels
- [ ] T065 [P] Write test for calculatePositions() method verifying centering offset (x = layoutX - width/2)
- [ ] T066 [P] Write test for caching behavior (cache hit on second call with same graph)
- [ ] T067 [P] Write test for caching invalidation (different graph structure generates different key)
- [ ] T068 [P] Write test for clearCache() method
- [ ] T069 [P] Write test for getCacheStats() method
- [ ] T070 [P] Write test for error handling: LayoutError with INVALID_NODE_SIZE code
- [ ] T071 [P] Write test for error handling: LayoutError with ORPHAN_NODE code (if applicable)
- [ ] T072 Write test for configuration options (horizontalSpacing, baseYOffset, centerGraph)
- [ ] T073 Write test for graph with single level (start and end nodes at same level)
- [ ] T074 Write test for deeply nested hierarchy (10+ levels)
- [ ] T075 Write test for large graph performance (100 nodes, verify < 100ms calculation time)
- [ ] T076 Run unit tests and verify all pass
- [ ] T077 Generate test coverage report
- [ ] T078 Verify test coverage is 95% or higher for flow-layout-engine.service.ts

### User Story: Integration Tests

**Priority**: P2 (High)  
**Description**: Integration tests for EditorService with new layout engine

---

- [ ] T079 Update editor.service.spec.ts at d:/My Respository/ApprovalProcess/ApFlow.UI.New/src/app/pages/flow-graph/services/editor.service.spec.ts
- [ ] T080 [P] Write integration test for renderGraph() with new layout engine
- [ ] T081 [P] Write integration test for caching mechanism across multiple renderGraph() calls
- [ ] T082 [P] Write integration test for BranchGroupManager integration (parallel branches)
- [ ] T083 [P] Write integration test for error handling and user notifications
- [ ] T084 Write integration test for backward compatibility (existing graph structures work)
- [ ] T085 Run integration tests and verify all pass

---

## Phase 6: Polish & Documentation

**Goal**: Refine code, update documentation, and ensure quality

**Independent Test Criteria**: Code passes linting, documentation is complete

---

- [ ] T086 Run TypeScript compiler with strict mode and fix any errors
- [ ] T087 Run Prettier formatter on all modified files
- [ ] T088 Run ESLint and fix any warnings
- [ ] T089 Review TSDoc completeness for all public APIs
- [ ] T090 Add inline comments for complex algorithms (BFS, caching logic)
- [ ] T091 Update README.md at d:/My Respository/ApprovalProcess/ApFlow.UI.New/src/app/pages/flow-graph/README.md with layout engine information
- [ ] T092 Add usage examples to flow-graph README.md
- [ ] T093 Update changelog (if exists) with breaking changes
- [ ] T094 Verify all files follow naming conventions (kebab-case for files, PascalCase for classes)
- [ ] T095 Verify all interfaces use I prefix (ILayoutConfig, ILayoutResult, etc.)
- [ ] T096 Perform manual code review of flow-layout-engine.service.ts
- [ ] T097 Perform manual code review of layout.models.ts
- [ ] T098 Perform manual code review of editor.service.ts changes

---

## Dependencies

### User Story Completion Order

```
Phase 1: Setup
    ↓
Phase 2: Foundational (Layout Models)
    ↓
Phase 3: Core Layout Engine
    ↓
Phase 4: Integration (EditorService)
    ↓
Phase 5: Testing
    ↓
Phase 6: Polish & Documentation
```

### Critical Dependencies

| Task | Depends On | Reason |
|------|------------|--------|
| T006-T020 | None | Layout models are foundational |
| T021-T040 | T006-T020 | Layout engine requires type definitions |
| T041-T054 | T021-T040 | Integration requires layout engine implementation |
| T055-T078 | T021-T040 | Unit tests require layout engine implementation |
| T079-T085 | T041-T054 | Integration tests require EditorService integration |
| T086-T098 | All previous tasks | Polish requires complete implementation |

### Parallel Execution Opportunities

**Within Phase 2 (Foundational)**:
- T007-T014: Interface definitions can be created in parallel
- T015-T018: Factory functions can be implemented in parallel
- T019-T020: Export and documentation can be done in parallel

**Within Phase 3 (Core Layout Engine)**:
- T024-T030: Core layout methods can be implemented in parallel after service setup
- T031-T036: Cache implementation can be done in parallel with core methods
- T037-T040: Error handling and documentation can be done in parallel

**Within Phase 5 (Testing)**:
- T056-T067: Unit tests can be written in parallel (different test cases)
- T068-T075: Additional unit tests can be written in parallel
- T080-T085: Integration tests can be written in parallel

---

## Parallel Execution Examples

### Example 1: Phase 2 (Foundational) - Parallel Interface Definitions

```bash
# Terminal 1
- Create ILayoutConfig interface (T007)

# Terminal 2
- Create INodePosition interface (T008)

# Terminal 3
- Create ILayoutResult interface (T009)

# Terminal 4
- Create INodeLevel interface (T010)
```

### Example 2: Phase 3 (Core Layout Engine) - Parallel Method Implementation

```bash
# Terminal 1
- Implement assignLevels() method (T026-T028)

# Terminal 2
- Implement calculatePositions() method (T029-T030)

# Terminal 3
- Implement LayoutCache class (T031-T036)
```

### Example 3: Phase 5 (Testing) - Parallel Test Cases

```bash
# Terminal 1
- Write tests for layout() method (T056-T059)

# Terminal 2
- Write tests for assignLevels() method (T060-T062)

# Terminal 3
- Write tests for calculatePositions() method (T063-T066)

# Terminal 4
- Write tests for caching (T066-T069)
```

---

## Implementation Strategy

### MVP (Minimum Viable Product) Scope

**MVP delivers**: Core layout engine with 50px vertical spacing for linear graphs

**MVP Tasks**:
- Phase 1: Setup (T001-T005)
- Phase 2: Foundational (T006-T020)
- Phase 3: Core Layout Engine (T021-T040)
- Phase 4: Integration (T041-T054)
- Minimal testing (T056-T065: basic layout and positioning tests)
- Basic polish (T086-T090)

**MVP Excludes**:
- Comprehensive edge case testing
- Integration tests
- Full documentation updates
- Performance benchmarking

### Incremental Delivery

**Sprint 1** (MVP): Linear graph layout with fixed spacing
- Core layout engine implementation
- Basic EditorService integration
- Basic unit tests

**Sprint 2**: Enhanced features
- Parallel branch support
- Caching implementation
- Comprehensive testing

**Sprint 3**: Quality and polish
- Integration tests
- Performance optimization
- Documentation updates

### Risk Mitigation

| Risk | Mitigation Strategy |
|------|---------------------|
| Performance degradation | Implement caching early (T031-T036), benchmark with large graphs (T075) |
| Incorrect level assignment | Thorough BFS testing (T060-T062), cycle detection (T062) |
| Breaking changes | Maintain backward compatibility (T049, T084), integration tests (T079-T085) |
| Test coverage < 95% | Target 95% from start, add tests incrementally, use coverage report (T077-T078) |

---

## Task Summary

| Phase | Task Count | Estimated Time | Parallel Opportunities |
|-------|-----------|----------------|------------------------|
| Phase 1: Setup | 5 | 1h | 2 (T002-T005) |
| Phase 2: Foundational | 15 | 2h | 8 (T007-T020) |
| Phase 3: Core Layout Engine | 20 | 4h | 6 (T024-T040) |
| Phase 4: Integration | 14 | 2h | 3 (T052-T054) |
| Phase 5: Testing | 31 | 7h | 20 (T056-T075, T080-T085) |
| Phase 6: Polish | 13 | 2h | 5 (T086-T098) |
| **Total** | **98** | **18h** | **44** |

---

## Success Criteria

### Quantitative Metrics

- ✅ 100% of consecutive level pairs maintain exactly 50px vertical spacing
- ✅ Layout calculation time: < 100ms for 100-node graphs
- ✅ Test coverage: 95% for layout engine code
- ✅ Zero TypeScript compilation errors (strict mode)

### Qualitative Outcomes

- ✅ Graph hierarchy is immediately apparent due to consistent spacing
- ✅ Fully compliant with Project Constitution Principle 3
- ✅ No breaking changes to existing functionality
- ✅ Code is maintainable and well-documented

---

**End of Implementation Tasks**

**Document Version**: 1.0.0  
**Status**: Ready for Implementation  
**Next Step**: Execute Phase 1 tasks (T001-T005)
