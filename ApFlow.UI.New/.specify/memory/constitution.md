<!--
Sync Impact Report:
- Version change: 0.0.0 → 1.0.0
- Modified principles: None (initial version)
- Added sections: Core Principles (5 principles), Architecture Standards, Development Workflow, Governance
- Removed sections: None
- Templates requiring updates:
  ✅ plan-template.md - Constitution Check section aligns with principles
  ✅ spec-template.md - User story independence aligns with Incremental Delivery principle
  ✅ tasks-template.md - Task organization by user stories aligns with principles
- Follow-up TODOs: None
-->

# ApFlow.UI Constitution

## Core Principles

### I. Angular Best Practices

ApFlow.UI MUST follow Angular framework conventions and best practices:
- Components MUST use Angular's change detection strategy efficiently
- Services MUST be providedIn at the root or specific module level for proper dependency injection
- Reactive Forms MUST be used for complex form scenarios with validation
- RxJS patterns MUST be followed for asynchronous operations
- Modules MUST be organized by feature (feature modules) to support lazy loading

**Rationale**: Adhering to Angular conventions ensures maintainability, performance, and leverage of framework capabilities.

### II. Visual Consistency (ng-zorro-antd)

UI components and styling MUST use ng-zorro-antd component library consistently:
- All UI components MUST use ng-zorro-antd components unless custom implementation is justified
- Design tokens and theme variables MUST be used for styling consistency
- Form components MUST use ng-zorro-antd form controls with proper validation feedback
- Layout MUST use ng-zorro-antd grid system and layout components

**Rationale**: ng-zorro-antd provides enterprise-grade UI components with consistent design language, reducing development effort and ensuring professional appearance.

### III. Flow Graph Visualisation (AntV X6)

Flow graph visualization MUST be implemented using AntV X6:
- All flow diagram rendering MUST use AntV X6 graph engine
- Custom node and edge renderers MUST extend AntV X6 base classes
- Graph interactions (drag, zoom, pan) MUST use AntV X6 built-in capabilities
- Graph state management MUST integrate with Angular change detection

**Rationale**: AntV X6 is specifically designed for diagram visualization with rich interactions, providing the foundation for approval flow editing and visualization.

### IV. Type Safety (TypeScript)

TypeScript strict mode MUST be enforced:
- All code MUST be compiled with TypeScript strict mode enabled
- Type definitions MUST be explicit - no implicit `any` types
- Interface definitions MUST be used for data models and contracts
- Generics MUST be used for reusable, type-safe components
- Null checks MUST be performed before accessing potentially null properties

**Rationale**: Type safety catches errors at compile time, improving code quality and reducing runtime bugs.

### V. Incremental Delivery

Features MUST be delivered incrementally by user stories with independent value:
- Each user story MUST be independently testable and deployable
- User stories MUST be prioritized (P1, P2, P3) to guide implementation order
- Each story MUST deliver measurable user value when completed
- Stories MUST avoid cross-dependencies that prevent independent completion
- MVP (Minimum Viable Product) MUST deliver the highest-priority user story first

**Rationale**: Incremental delivery enables faster feedback, reduces risk, and ensures users receive value early and often.

## Architecture Standards

### Project Structure

ApFlow.UI follows Angular best practices for project organization:
- `src/app/pages/` - Feature modules organized by business domain
- `src/app/models/` - Data models and interfaces
- `src/app/services/` - Shared services and business logic
- `src/app/common/` - Shared components, directives, pipes, and utilities

### Component Design

- Components MUST be single-responsible and reusable where applicable
- Container/presentational component pattern MUST be used for complex UI
- Smart components (containers) handle state and business logic
- Dumb components (presentational) receive data via @Input and emit events via @Output
- Components MUST declare change detection strategy (OnPush preferred for performance)

### State Management

- Component state: Use component properties for simple local state
- Shared state: Use RxJS BehaviorSubject and services for cross-component state
- Complex state: Consider NgRx or similar libraries if justified
- State updates MUST be immutable to support OnPush change detection

### Data Models

- Interfaces MUST be defined in `src/app/models/` directory
- Models MUST represent business entities clearly with appropriate naming
- Models MUST be typed with TypeScript interfaces or classes
- Serialization/deserialization logic MUST be centralized in services

## Development Workflow

### Code Quality

- Prettier MUST be used for code formatting (configured in package.json)
- Code MUST follow Angular style guide conventions
- TypeScript strict mode violations MUST be fixed before commit
- Linter warnings MUST be addressed before merging

### Testing

- Unit tests MUST be written for complex logic and services
- Component tests MUST verify component behavior and interactions
- Integration tests MUST verify user journeys across components
- Tests MUST be runnable via `ng test` command

### Documentation

- Component documentation MUST describe purpose, inputs, outputs, and usage
- Complex business logic MUST have inline code comments
- API services MUST document endpoints, parameters, and response formats

## Governance

### Amendment Procedure

Constitution amendments MUST follow this process:
1. Propose amendment with rationale and impact analysis
2. Document version change according to semantic versioning:
   - MAJOR: Backward incompatible principle removal or redefinition
   - MINOR: New principle or section added, or material expansion
   - PATCH: Clarifications, wording fixes, non-semantic refinements
3. Update constitution and propagate changes to dependent templates
4. Update version and last amended date

### Compliance Review

- All pull requests MUST verify compliance with constitution principles
- Principle violations MUST be documented and justified in plan.md
- Complexity MUST be justified when deviating from standards
- Code reviews MUST check adherence to Angular best practices and type safety

### Runtime Guidance

Use `.codebuddy/rules/ApFlow.UI_项目规则.mdc` for project-specific development guidance including:
- Technology stack specifics (Angular version, TypeScript version, dependencies)
- Project structure conventions
- Code organization patterns

**Version**: 1.0.0 | **Ratified**: 2026-03-03 | **Last Amended**: 2026-03-03
