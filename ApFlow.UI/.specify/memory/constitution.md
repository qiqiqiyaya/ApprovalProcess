<!--
Sync Impact Report:
Version Change: 0.0.0 → 1.0.0 (INITIAL VERSION)
Modified Principles: None (initial creation)
Added Sections:
  - I. TypeScript Strict Mode (Non-Negotiable)
  - II. Business-First Documentation
  - III. Component Architecture Standards
  - IV. AntV X6 Integration Discipline
  - V. Testing & Observability
  - VI. Cross-Cutting Code Quality
Removed Sections: None
Templates Status:
  ✅ plan-template.md - No updates needed (generic structure compatible)
  ✅ spec-template.md - No updates needed (generic structure compatible)
  ✅ tasks-template.md - No updates needed (generic structure compatible)
Follow-up TODOs: None
-->

# ApFlow.UI Constitution

## Core Principles

### I. TypeScript Strict Mode (NON-NEGOTIABLE)

All code MUST be written in TypeScript with strict mode enabled. The project uses TypeScript 5.7+ with the following compiler options enforced:

- `strict: true` - All strict type-checking options enabled
- `noImplicitAny: true` - Expressions with type 'any' implicitly have 'any' type is disallowed
- `strictNullChecks: true` - Null and undefined checks are enforced
- `noImplicitOverride: true` - Requires @Override annotation for overridden methods

**Rationale**: Type safety is the foundation of maintainable Angular applications. The combination of Angular 19's standalone components, reactive forms, and dependency injection requires strong typing to prevent runtime errors. The project targets enterprise-level approval flow systems where reliability is critical.

**Exceptions**: Only when absolutely necessary due to third-party library limitations, and MUST be accompanied by `TODO(<JIRA-ID>): Explain why 'any' is used and plan to replace with proper type within [X] days`.

---

### II. Business-First Documentation

Every component, service, module, and model MUST have a corresponding `*.business.md` file in its directory. These files are the source of truth for business logic understanding and MUST be created before or immediately alongside the code.

**Required Content in *.business.md**:
1. **Business Purpose**: What problem does this component/module solve?
2. **Core Functionality**: What does it actually do in business terms?
3. **Key Inputs/Outputs**: `@Input()`, `@Output()`, service API contracts
4. **Main Flow/Logic**: Brief description of key execution paths
5. **Dependencies**: Which other components, services, or modules it depends on
6. **Usage Example**: Short code snippet demonstrating typical usage (if applicable)

**Rationale**: The ApFlow.UI project is an enterprise approval flow system where business logic is complex and domain-specific. Code alone cannot capture business intent effectively. Business-first documentation ensures that AI agents and future developers understand the "why" before implementing the "how", reducing misinterpretation and architectural drift.

**Enforcement**: Code reviews MUST verify existence and completeness of `.business.md` files. Missing or incomplete business documentation is a blocking issue.

---

### III. Component Architecture Standards

Angular components MUST follow these structural and communication patterns:

**File Organization**:
- Each component MUST reside in its own directory with kebab-case naming: `approval-node/`
- Directory MUST contain:
  - `approval-node.component.ts`
  - `approval-node.component.html`
  - `approval-node.component.css`
  - `approval-node.component.spec.ts`
  - `approval-node.business.md` (MANDATORY)

**Naming Conventions**:
- **File names**: kebab-case (e.g., `approval-node.component.ts`)
- **Class names**: PascalCase with `Component` suffix (e.g., `ApprovalNodeComponent`)
- **Selectors**: `app-` prefix + kebab-case (e.g., `app-approval-node`)
- **Services**: PascalCase with `Service` suffix (e.g., `FlowDataService`)

**Communication Patterns**:
- **Parent-Child**: MUST use `@Input()` for data flow down, `@Output()` with `EventEmitter` for events up
- **Cross-Component (Non-Hierarchical)**: MUST use shared Services with `BehaviorSubject` or Angular `Signal`. `EventEmitter` is PROHIBITED for non-parent-child communication
- **Dependency Injection**: All services MUST be injected via constructor, never instantiated within components

**Component Design**:
- Components MUST be small and single-responsibility
- Pure presentation components SHOULD use `ChangeDetectionStrategy.OnPush`
- `*ngFor` loops MUST include `trackBy` functions for performance

**Rationale**: Consistent component architecture ensures maintainability, testability, and performance optimization. Clear communication patterns prevent tight coupling and make data flow traceable. The OnPush strategy combined with trackBy is essential for rendering complex approval flow graphs efficiently.

---

### IV. AntV X6 Integration Discipline

All AntV X6 graph operations MUST follow strict encapsulation patterns:

**Centralized Shape Registration**:
- Custom nodes and edges MUST be registered in dedicated services under `common/x6-shapes/`
- Registration MUST be done once at application startup, not in individual components

**Encapsulated Graph Operations**:
- All direct manipulations of `Graph` instances (add node, delete node, create edge, etc.) MUST be encapsulated in services (e.g., `GraphOperationService`)
- Components MUST call service methods, NEVER directly access `graph.addNode()`, `graph.deleteCell()`, etc.
- Graph instance management MUST be centralized in a dedicated service

**Initialization Timing**:
- X6 Graph instances MUST be initialized in `ngAfterViewInit` lifecycle hook, NEVER in `ngOnInit`
- DOM elements MUST be available before graph initialization

**Type Safety**:
- Custom node/edge shapes MUST have proper TypeScript interfaces defined
- Use declaration merging to extend AntV X6's type definitions for custom props
- Avoid type assertions; define proper interfaces for custom node data

**Rationale**: AntV X6 provides powerful graph capabilities but can lead to scattered, hard-to-maintain code if not properly encapsulated. Centralizing operations enables consistent validation, error handling, logging, and easier testing. Proper typing prevents runtime errors when working with complex graph data structures.

---

### V. Testing & Observability

**Testing Requirements**:
- Core business logic MUST achieve >80% test coverage
- Components MUST achieve >70% test coverage
- Test files MUST reside alongside source files: `component-name.component.spec.ts`
- Tests MUST use `TestBed` configuration with mocked dependencies
- When tests are requested, they MUST be written FIRST and MUST FAIL before implementation (TDD for critical paths)

**Observability Requirements**:
- Structured logging MUST be used for all significant operations
- Log format MUST include: timestamp, level, component, operation, context
- Error handling MUST be centralized through interceptors and error services
- HTTP calls MUST have timeout configuration and error tracking

**Performance Monitoring**:
- Critical user journeys (e.g., loading complex approval graphs) MUST have performance metrics
- Change detection strategy MUST be monitored for unnecessary re-renders

**Rationale**: Enterprise approval systems require high reliability and debuggability. Comprehensive tests prevent regressions, while structured logging enables rapid troubleshooting in production. Performance monitoring is essential for handling large workflow graphs.

---

### VI. Cross-Cutting Code Quality

**Type System Usage**:
- Prefer `interface` for object structure definitions
- Use `type` for union types, conditional types, and utility types
- Utility types like `Nullable<T>`, `DeepReadonly<T>` SHOULD be used for common transformations
- Prefer type inference over explicit types in internal implementation, but MUST use explicit types in public APIs

**Code Organization**:
- Prefer composition over inheritance
- Each function/method MUST do one thing well
- Complex logic MUST be extracted into named helper functions with descriptive names
- Comments MUST explain "why" not "what"

**Angular-Specific Practices**:
- Services SHOULD use `providedIn: 'root'` for singletons
- Lifecycle hooks MUST be implemented in standard order: `ngOnInit` → lifecycle methods → `ngOnDestroy`
- Reactive forms MUST use typed form controls with proper model binding
- RxJS operators MUST be used with proper type inference; avoid `any` in pipe chains

**Code Style**:
- Import statements MUST be grouped: Angular imports → third-party → application imports
- CSS MUST follow BEM naming convention (`.block__element--modifier`)
- `!important` is PROHIBITED in component styles
- Commit messages MUST follow Conventional Commits format: `feat(scope): description`

**Rationale**: Consistent code quality practices reduce cognitive load, enable better tooling support (IDE auto-complete, refactoring), and ensure the codebase remains approachable for new team members. Strict type enforcement combined with clear organization prevents subtle bugs in complex approval workflows.

---

## Technology Stack Constraints

**Mandatory Technologies**:
- **Framework**: Angular 19.2.0+ with standalone components disabled (`standalone: false` for component decorators)
- **Language**: TypeScript 5.7+ with strict mode enabled
- **Flow Graph Library**: AntV X6 2.18.1+ with Angular shape plugin 2.0.2+
- **UI Component Library**: ng-zorro-antd 19.3.1+
- **Form Handling**: @delon/form 19.2.0+
- **Build Tool**: Angular CLI 19.2.15+ with @angular-devkit/build-angular

**Development Tools**:
- **Testing**: Jasmine 5.6.0+, Karma 6.4.0+
- **Linting**: ESLint (configured in project)
- **Package Manager**: npm (package-lock.json committed)

**Rationale**: This technology stack is optimized for enterprise Angular applications with complex form handling and graph visualization needs. The specific versions are tested and validated for compatibility. Deviating from this stack requires explicit justification and compatibility testing.

---

## Development Workflow

**Branch Strategy**:
- Main branch: `main` (production-ready code)
- Feature branches: `feature/short-description` (e.g., `feature/parallel-approval`)
- Bugfix branches: `bugfix/short-description`
- Branches MUST be created from `main` and merged via pull request

**Code Review Requirements**:
- All code MUST undergo peer review before merging
- Review checklist MUST verify:
  - All `.business.md` files exist and are complete
  - No `any` types without TODO justification
  - Component architecture follows standards
  - X6 operations are properly encapsulated
  - Tests pass with adequate coverage
  - No `!important` in CSS
  - Linting passes (`ng lint`)

**Quality Gates**:
- Unit tests MUST pass before PR approval
- Linting MUST pass without errors
- Build MUST succeed (`ng build`)
- Business documentation MUST be complete

**Documentation Updates**:
- README.md MUST be updated for user-facing features
- API documentation MUST be updated for service changes
- Breaking changes MUST be documented in migration guides

**Rationale**: Structured development workflow with quality gates ensures code quality and reduces integration issues. Clear branch strategy and review process maintain codebase integrity while enabling parallel development.

---

## Governance

**Constitution Authority**:
This constitution supersedes all other coding standards, conventions, or personal preferences. All team members and AI agents MUST comply with its principles.

**Amendment Process**:
1. Proposed amendments MUST be documented with rationale
2. Amendments MUST be reviewed and approved by project maintainers
3. Amendments MUST include version bump and impact analysis
4. All affected code MUST be updated within a defined migration period

**Versioning Policy**:
- **MAJOR**: Backward-incompatible governance changes, principle removals, or redefinitions
- **MINOR**: New principle or section added, materially expanded guidance
- **PATCH**: Clarifications, wording improvements, typo fixes, non-semantic refinements

**Compliance Review**:
- Monthly reviews MUST verify compliance with constitution principles
- Non-compliant code MUST be addressed in subsequent sprints
- Violations MUST be documented with remediation plans

**Runtime Guidance**:
- Refer to `.trae/rules/project_rules.md` for detailed project-specific implementation guidance
- Refer to `.trae/documents/用户编码规范与重构指南优化.md` for Chinese-language coding standards
- Refer to individual `*.business.md` files for component-specific business context

**Rationale**: Clear governance ensures the constitution remains a living document that evolves with the project while maintaining consistency. Regular compliance reviews prevent architectural drift and uphold code quality standards.

---

**Version**: 1.0.0 | **Ratified**: 2026-03-02 | **Last Amended**: 2026-03-02
