# Tasks: Uniform Vertical Spacing for Flow Graph Nodes

**Input**: Design documents from `/specs/001-uniform-vertical-spacing/`
**Prerequisites**: plan.md ✅, spec.md ✅, research.md ✅, data-model.md ✅
**Tests**: Unit tests are included as per feature specification (Vitest + Angular Test Bed)

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (US1, US2, US3)
- Include exact file paths in descriptions

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [X] T001 Create directory structure for layout services in src/app/pages/flow-graph/services/layout/
- [X] T002 [P] Create directory structure for layout components in src/app/pages/flow-graph/components/layout-config/
- [X] T003 [P] Create directory structure for layout unit tests in tests/unit/flow-graph/layout/

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

### Core Type Definitions

- [X] T004 [P] Create LayoutConfig interface in src/app/pages/flow-graph/models/layout-config.ts
- [X] T005 [P] Create LayoutResult interface in src/app/pages/flow-graph/models/layout-config.ts
- [X] T006 [P] Create NodePosition interface in src/app/pages/flow-graph/models/layout-config.ts
- [X] T007 [P] Create LayoutMetadata interface in src/app/pages/flow-graph/models/layout-config.ts
- [X] T008 [P] Create EdgeRoute and Point interfaces in src/app/pages/flow-graph/models/layout-config.ts

### Configuration Model Classes

- [X] T009 Create StrictLayoutConfig class with validation in src/app/pages/flow-graph/models/layout-config.ts
- [X] T010 [P] Define LayoutPreset interface and LAYOUT_PRESETS constant in src/app/pages/flow-graph/models/layout-config.ts

### Layout Calculation Types

- [X] T011 [P] Create LayoutEngine<TNode, TEdge> generic interface in src/app/pages/flow-graph/models/layout-config-extended.ts
- [X] T012 [P] Create LayoutErrorCode enum and LayoutError interface in src/app/pages/flow-graph/models/layout-config-extended.ts
- [X] T013 [P] Create isLayoutError and isLayoutResult type guards in src/app/pages/flow-graph/models/layout-config-extended.ts
- [X] T014 Create HierarchicalNode<T> interface in src/app/pages/flow-graph/models/layout-config-extended.ts

### Service Interface Definitions

- [X] T015 [P] Create ILayoutService interface in src/app/pages/flow-graph/models/layout-service-interfaces.ts
- [X] T016 [P] Create ILayoutConfigService interface in src/app/pages/flow-graph/models/layout-service-interfaces.ts
- [X] T017 [P] Create IVerticalSpacingCalculator interface in src/app/pages/flow-graph/models/layout-service-interfaces.ts

### Injection Tokens

- [X] T018 Create X6_GRAPH injection token in src/app/pages/flow-graph/models/layout-config-extended.ts

**Checkpoint**: Foundation ready - all type definitions complete, user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Display Uniform Vertical Spacing (Priority: P1) 🎯 MVP

**Goal**: Display uniform vertical spacing between all flow nodes, with hardcoded 50px spacing

**Independent Test**: Create a simple flow graph with start node → operation node → approval node → end node. Verify that the vertical spacing between each pair of adjacent nodes is exactly 50px, regardless of node sizes.

### Tests for User Story 1

> **NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [X] T019 [P] [US1] Create unit test file in tests/unit/flow-graph/layout/uniform-spacing-layout.spec.ts
- [X] T020 [P] [US1] Write test for layer assignment in tests/unit/flow-graph/layout/uniform-spacing-layout.spec.ts
- [X] T021 [P] [US1] Write test for node ordering in tests/unit/flow-graph/layout/uniform-spacing-layout.spec.ts
- [X] T022 [P] [US1] Write test for uniform vertical spacing calculation in tests/unit/flow-graph/layout/uniform-spacing-layout.spec.ts
- [X] T023 [P] [US1] Write test for simple linear flow graph (no branches) in tests/unit/flow-graph/layout/uniform-spacing-layout.spec.ts

### Implementation for User Story 1

#### Layout Engine Core

- [X] T024 [P] [US1] Create UniformSpacingLayout class skeleton in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts
- [X] T025 [P] [US1] Implement calculateLevels method using BFS in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts
- [X] T026 [P] [US1] Implement groupNodesByLevel method in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts
- [X] T027 [P] [US1] Implement calculatePositions method with uniform spacing in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts
- [X] T028 [US1] Implement layout method orchestrating three phases in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts

#### Layout Service Integration

- [X] T029 [P] [US1] Create LayoutService class skeleton in src/app/pages/flow-graph/services/layout/layout.service.ts
- [X] T030 [P] [US1] Implement BehaviorSubject for layout state in src/app/pages/flow-graph/services/layout/layout.service.ts
- [X] T031 [P] [US1] Implement calculateLayout$ observable with debounceTime(16) in src/app/pages/flow-graph/services/layout/layout.service.ts
- [X] T032 [P] [US1] Implement applyLayout method in src/app/pages/flow-graph/services/layout/layout.service.ts
- [X] T033 [US1] Implement triggerLayout method in src/app/pages/flow-graph/services/layout/layout.service.ts

#### FlowGraph Integration

- [X] T034 [P] [US1] Add applyLayout method to FlowGraph class in src/app/pages/flow-graph/models/flow-graph.ts
- [X] T035 [P] [US1] Add toLayoutNodes method to FlowGraph class in src/app/pages/flow-graph/models/flow-graph.ts
- [X] T036 [P] [US1] Add toLayoutEdges method to FlowGraph class in src/app/pages/flow-graph/models/flow-graph.ts
- [X] T037 [US1] Integrate LayoutService with EditorService in src/app/pages/flow-graph/services/editor.service.ts

#### Component Integration

- [X] T038 [US1] Subscribe to layout updates in EditorComponent in src/app/pages/flow-graph/components/editor/editor.component.ts
- [X] T039 [US1] Add triggerLayout call on node add/delete in EditorComponent in src/app/pages/flow-graph/components/editor/editor.component.ts

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently. Create a simple flow graph and verify 50px uniform spacing.

---

## Phase 4: User Story 2 - Maintain Visual Balance With Different Node Sizes (Priority: P2)

**Goal**: Maintain visual balance when nodes have different sizes, handling parallel branches correctly

**Independent Test**: Create a flow graph with small nodes (80×40) and large nodes (200×100). Verify that uniform spacing is maintained and parallel branches align vertically with consistent spacing.

### Tests for User Story 2

- [X] T040 [P] [US2] Write test for parallel branch alignment in tests/unit/flow-graph/layout/uniform-spacing-layout.spec.ts
- [X] T041 [P] [US2] Write test for nested parallel branches in tests/unit/flow-graph/layout/uniform-spacing-layout.spec.ts
- [X] T042 [P] [US2] Write test for mixed node sizes in tests/unit/flow-graph/layout/uniform-spacing-layout.spec.ts
- [X] T043 [P] [US2] Write test for empty branch handling in tests/unit/flow-graph/layout/uniform-spacing-layout.spec.ts

### Implementation for User Story 2

#### Parallel Branch Handling

- [X] T044 [P] [US2] Add handleParallelBranches method in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts
- [X] T045 [P] [US2] Add handleNestedBranches method in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts
- [X] T046 [US2] Integrate parallel branch handling into layout method in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts

#### Visual Balance Optimization

- [X] T047 [P] [US2] Implement barycenter ordering for node positioning in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts
- [X] T048 [P] [US2] Add branch group alignment logic in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts
- [X] T049 [US2] Optimize position calculation for variable node sizes in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts

#### BranchGroup Integration

- [X] T050 [P] [US2] Add validateUniformSpacing method to BranchGroup class in src/app/pages/flow-graph/models/flow-group.ts
- [X] T051 [P] [US2] Add getAllNodes method to BranchGroup class in src/app/pages/flow-graph/models/flow-group.ts
- [X] T052 [US2] Pass branch groups to layout engine in LayoutService in src/app/pages/flow-graph/services/layout/layout.service.ts

#### Performance Optimization

- [X] T053 [P] [US2] Add layout result caching to LayoutService in src/app/pages/flow-graph/services/layout/layout.service.ts
- [X] T054 [P] [US2] Add distinctUntilChanged to layout trigger in src/app/pages/flow-graph/services/layout/layout.service.ts
- [X] T055 [US2] Benchmark layout performance with 100 nodes in tests/unit/flow-graph/layout/layout.service.spec.ts

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently. Test with complex flow graphs containing parallel branches and mixed node sizes.

---

## Phase 5: User Story 3 - Configurable Spacing Distance (Priority: P3)

**Goal**: Allow users to configure vertical spacing distance with UI controls and presets

**Independent Test**: Use the configuration UI to change vertical spacing from 50px to 80px. Verify that all node-to-node spacing updates to 80px consistently across the entire flow graph.

### Tests for User Story 3

- [X] T056 [P] [US3] Write test for LayoutConfigService configuration updates in tests/unit/flow-graph/layout/layout-config.service.spec.ts
- [X] T057 [P] [US3] Write test for preset application in tests/unit/flow-graph/layout/layout-config.service.spec.ts
- [X] T058 [P] [US3] Write test for localStorage persistence in tests/unit/flow-graph/layout/layout-config.service.spec.ts
- [X] T059 [P] [US3] Write test for configuration validation in tests/unit/flow-graph/layout/layout-config.service.spec.ts

### Implementation for User Story 3

#### Configuration Service

- [X] T060 [P] [US3] Create LayoutConfigService class in src/app/pages/flow-graph/services/layout/layout-config.service.ts
- [X] T061 [P] [US3] Implement BehaviorSubject for configuration state in src/app/pages/flow-graph/services/layout/layout-config.service.ts
- [X] T062 [P] [US3] Implement updateConfig method in src/app/pages/flow-graph/services/layout/layout-config.service.ts
- [X] T063 [P] [US3] Implement resetToDefaults method in src/app/pages/flow-graph/services/layout/layout-config.service.ts
- [X] T064 [P] [US3] Implement localStorage persistence in src/app/pages/flow-graph/services/layout/layout-config.service.ts
- [X] T065 [P] [US3] Implement getPresets and applyPreset methods in src/app/pages/flow-graph/services/layout/layout-config.service.ts
- [X] T066 [US3] Integrate LayoutConfigService with LayoutService in src/app/pages/flow-graph/services/layout/layout.service.ts

#### Configuration UI Component

- [X] T067 [P] [US3] Create LayoutConfigComponent class in src/app/pages/flow-graph/components/layout-config/layout-config.component.ts
- [X] T068 [P] [US3] Create LayoutConfigComponent template in src/app/pages/flow-graph/components/layout-config/layout-config.component.html
- [X] T069 [P] [US3] Implement ReactiveForm with ng-zorro-antd components in src/app/pages/flow-graph/components/layout-config/layout-config.component.html
- [X] T070 [P] [US3] Add nz-input-number for vertical spacing (20-200) in src/app/pages/flow-graph/components/layout-config/layout-config.component.html
- [X] T071 [P] [US3] Add nz-input-number for horizontal spacing (50-500) in src/app/pages/flow-graph/components/layout-config/layout-config.component.html
- [X] T072 [P] [US3] Add nz-switch for animate toggle in src/app/pages/flow-graph/components/layout-config/layout-config.component.html
- [X] T073 [P] [US3] Add nz-select for preset selection in src/app/pages/flow-graph/components/layout-config/layout-config.component.html
- [X] T074 [US3] Implement onSubmit and onReset handlers in src/app/pages/flow-graph/components/layout-config/layout-config.component.ts

#### Editor Integration

- [X] T075 [US3] Add LayoutConfigComponent to EditorComponent template in src/app/pages/flow-graph/components/editor/editor.component.html
- [X] T076 [US3] Subscribe to config changes for layout updates in EditorComponent in src/app/pages/flow-graph/components/editor/editor.component.ts

**Checkpoint**: All user stories should now be independently functional. Test configuration UI by changing spacing values and applying presets.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

### Error Handling

- [X] T077 [P] Add circular dependency detection in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts
- [X] T078 [P] Add empty graph handling in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts
- [X] T079 [P] Add error handling and logging in src/app/pages/flow-graph/services/layout/layout.service.ts

### Animation Support

- [X] T080 [P] Implement layout transition animation using X6 cell.transition() in src/app/pages/flow-graph/services/layout/layout.service.ts
- [X] T081 [P] Add animation duration configuration support in src/app/pages/flow-graph/services/layout/layout.service.ts

### Multiple Subgraph Detection

- [X] T082 [P] Add multiple subgraph detection in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts
- [X] T083 [P] Add user notification for multiple subgraphs in src/app/pages/flow-graph/components/editor/editor.component.ts

### Performance & Testing

- [X] T084 [P] Add performance benchmarking for 100-node graphs in tests/unit/flow-graph/layout/uniform-spacing-layout.spec.ts
- [ ] T085 [P] Add integration tests for complex flow graphs in tests/integration/flow-graph/layout.spec.ts
- [ ] T086 [P] Add visual regression tests for layout consistency in tests/integration/flow-graph/layout.spec.ts

### Documentation

- [X] T087 [P] Add TSDoc comments to all public APIs in src/app/pages/flow-graph/services/layout/
- [X] T088 [P] Update README.md in src/app/pages/flow-graph/ with layout feature documentation
- [X] T089 [P] Create inline code comments for algorithm steps in src/app/pages/flow-graph/services/layout/uniform-spacing-layout.ts

### Code Quality

- [X] T090 [P] Run TypeScript compiler and fix any strict mode violations
- [X] T091 [P] Run ESLint and fix any issues in src/app/pages/flow-graph/services/layout/
- [X] T092 [P] Verify no `any` types in layout codebase
- [X] T093 [P] Verify all interfaces have TSDoc comments

### Validation

- [ ] T094 Run quickstart.md validation checklist
- [ ] T095 Verify all success criteria from spec.md are met
- [ ] T096 Run manual testing for all three user stories
- [ ] T097 Verify performance targets (<100ms recalculation, <500ms initial render for 50 nodes)

**Checkpoint**: Feature complete, polished, and ready for production deployment

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Story 1 (Phase 3)**: Depends on Foundational completion - No dependencies on other user stories
- **User Story 2 (Phase 4)**: Depends on Foundational completion - Builds on US1 but independently testable
- **User Story 3 (Phase 5)**: Depends on Foundational completion - Enhancement to US1/US2
- **Polish (Phase 6)**: Depends on all desired user stories being complete

### User Story Dependencies

- **User Story 1 (P1)**: Can start after Foundational (Phase 2) - No dependencies on US2 or US3
- **User Story 2 (P2)**: Can start after Foundational (Phase 2) - Builds on US1 but maintains independence
- **User Story 3 (P3)**: Can start after Foundational (Phase 2) - Enhances US1/US2 but independently testable

### Within Each User Story

- Tests MUST be written and FAIL before implementation (TDD approach)
- Models/Types before services
- Services before integration
- Core implementation before UI
- Story complete before moving to next priority

### Parallel Opportunities

#### Phase 1 (Setup)
All tasks can run in parallel:
- T001, T002, T003

#### Phase 2 (Foundational)
Type definitions (all can run in parallel):
- T004-T008 (interfaces)
- T009-T010 (classes)
- T011-T014 (calculation types)
- T015-T017 (service interfaces)
- T018 (injection token)

#### Phase 3 (User Story 1)
Tests (all can run in parallel):
- T019-T023

Layout Engine (all can run in parallel):
- T024-T027

Layout Service (all can run in parallel):
- T029-T032

FlowGraph Integration (all can run in parallel):
- T034-T036

#### Phase 4 (User Story 2)
Tests (all can run in parallel):
- T040-T043

Parallel Branch Handling (T044 can run in parallel with T045):
- T044, T045

Visual Balance (T047, T048 can run in parallel):
- T047, T048

BranchGroup Integration (all can run in parallel):
- T050-T051

Performance Optimization (all can run in parallel):
- T053, T054

#### Phase 5 (User Story 3)
Tests (all can run in parallel):
- T056-T059

Configuration Service (T060-T065 can run in parallel):
- T060, T061, T062, T063, T064, T065

Configuration UI (T067-T073 can run in parallel):
- T067, T068, T069, T070, T071, T072, T073

#### Phase 6 (Polish)
All tasks marked [P] can run in parallel:
- T077-T089, T090-T093

---

## Parallel Example: User Story 1

```bash
# Phase 2 - Run all type definition tasks in parallel:
Task T004: Create LayoutConfig interface
Task T005: Create LayoutResult interface
Task T006: Create NodePosition interface
Task T007: Create LayoutMetadata interface
Task T008: Create EdgeRoute and Point interfaces

# Phase 3 - Run all tests for User Story 1 together:
Task T019: Create unit test file
Task T020: Write test for layer assignment
Task T021: Write test for node ordering
Task T022: Write test for uniform spacing calculation
Task T023: Write test for simple linear flow graph

# After tests fail, implement layout engine (parallel):
Task T024: Create UniformSpacingLayout class skeleton
Task T025: Implement calculateLevels method
Task T026: Implement groupNodesByLevel method
Task T027: Implement calculatePositions method

# Implement layout service (parallel):
Task T029: Create LayoutService class skeleton
Task T030: Implement BehaviorSubject
Task T031: Implement calculateLayout$ observable
Task T032: Implement applyLayout method

# Integrate with FlowGraph (parallel):
Task T034: Add applyLayout method
Task T035: Add toLayoutNodes method
Task T036: Add toLayoutEdges method
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup (T001-T003)
2. Complete Phase 2: Foundational (T004-T018) - **CRITICAL: Blocks all stories**
3. Complete Phase 3: User Story 1 (T019-T039)
4. **STOP and VALIDATE**: Test User Story 1 independently
   - Create simple flow graph: Start → Operation → Approval → End
   - Verify uniform 50px spacing between all adjacent nodes
   - Confirm no spacing variation regardless of node sizes
5. Deploy/demo if ready

### Incremental Delivery

1. Complete Setup (Phase 1) + Foundational (Phase 2) → Foundation ready
2. Add User Story 1 (Phase 3) → Test independently → Deploy/Demo (MVP!)
3. Add User Story 2 (Phase 4) → Test independently → Deploy/Demo
4. Add User Story 3 (Phase 5) → Test independently → Deploy/Demo
5. Add Polish (Phase 6) → Final production release
6. Each story adds value without breaking previous stories

### Parallel Team Strategy

With multiple developers:

**Week 1-2**: All developers collaborate on Setup + Foundational
1. Team completes Phase 1 (T001-T003)
2. Team completes Phase 2 (T004-T018)

**Week 3-4**: After Foundational is done, split into parallel work:
- Developer A: User Story 1 (T019-T039)
- Developer B: User Story 2 (T040-T055) - Can start after Phase 2
- Developer C: User Story 3 (T056-T076) - Can start after Phase 2

**Week 5**: Team collaboration on Polish (T077-T097)

### Recommended Order for Single Developer

1. Phase 1: Setup (T001-T003)
2. Phase 2: Foundational (T004-T018) - **CRITICAL**
3. Phase 3: User Story 1 (T019-T039) - **MVP DELIVERABLE**
4. [STOP] Test and demo MVP
5. Phase 4: User Story 2 (T040-T055)
6. [STOP] Test and demo US1+US2
7. Phase 5: User Story 3 (T056-T076)
8. [STOP] Test and demo US1+US2+US3
9. Phase 6: Polish (T077-T097)

---

## Notes

- **[P] tasks** = different files, no dependencies - can run in parallel
- **[Story] label** maps task to specific user story for traceability (US1, US2, US3)
- Each user story should be independently completable and testable
- Verify tests fail before implementing (TDD approach)
- Commit after each task or logical group
- Stop at any checkpoint to validate story independently
- TypeScript strict mode enforced - no `any` types allowed
- All public APIs must have TSDoc comments
- Performance targets: <100ms recalculation, <500ms initial render (50 nodes)
- Avoid: vague tasks, same file conflicts, cross-story dependencies that break independence

---

**Feature**: Uniform Vertical Spacing for Flow Graph Nodes  
**Branch**: 001-uniform-vertical-spacing  
**Total Tasks**: 97  
**Estimated Duration**: 4-5 weeks (single developer) or 2-3 weeks (team)  
**MVP Tasks**: T001-T039 (User Story 1 only)
