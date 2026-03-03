<!--
Sync Impact Report:
Version: 1.0.0 → 1.1.0
Modified Principles:
  - Principle 3: Layout Performance Optimization (revised for strict 50px vertical spacing)
Removed Sections:
  - Principle 5: Testing Discipline (temporarily removed)
Added Sections: N/A
Templates Requiring Updates:
  ⚠ .specify/templates/plan-template.md (needs creation)
  ⚠ .specify/templates/spec-template.md (needs creation)
  ⚠ .specify/templates/tasks-template.md (needs creation)
  ⚠ .specify/templates/commands/*.md (needs creation)
Follow-up TODOs:
  - Update editor.service.ts to use custom layout engine with strict 50px vertical spacing
  - Replace DagreLayout with custom Sugiyama-based layout implementation
-->

# ApFlow.UI Project Constitution

**Constitution Version:** 1.1.0
**Ratification Date:** 2026-03-03
**Last Amended Date:** 2026-03-03
**Status:** Active

---

## Purpose

This document establishes the non-negotiable development principles that govern the ApFlow.UI project, an Angular 19+ enterprise approval flow management application built with TypeScript strict mode, ng-zorro-antd, @delon/form, and AntV X6. All development activities MUST adhere to these principles to ensure type safety, maintainability, and performance.

---

## Principle 1: Type Safety First

**MUST enforce strict TypeScript configuration across the entire codebase.**

### Rules

- The project MUST use TypeScript strict mode with all compiler flags enabled:
  - `strict: true`
  - `noImplicitOverride: true`
  - `noPropertyAccessFromIndexSignature: true`
  - `noImplicitReturns: true`
  - `noFallthroughCasesInSwitch: true`
- All services, components, directives, and pipes MUST have explicit type annotations.
- Generic type parameters MUST be used instead of `any` type for reusable components and services.
- Custom type definitions (`.d.ts` files) MUST be provided for third-party libraries lacking proper TypeScript support.
- Type assertions MUST be minimized; prefer type guards and type inference.

### Rationale

Strict type safety catches errors at compile time rather than runtime, reducing bugs and improving code maintainability. This is critical for enterprise applications where reliability is paramount.

---

## Principle 2: Component Architecture Standards

**MUST follow Angular 19+ best practices for component architecture.**

### Rules

- Components MUST be organized by feature in `src/app/pages/` directory.
- Shared components and utilities MUST reside in `src/app/shared/` directory.
- Data models MUST be defined in `src/app/models/` directory.
- Services MUST be singleton via `@Injectable({ providedIn: 'root' })` unless explicitly scoped.
- All component inputs MUST be typed with explicit interfaces or type aliases.
- Component outputs MUST use typed `EventEmitter<T>` with specific event types.
- ng-zorro-antd components MUST be wrapped with typed business-specific components when reuse patterns emerge.

### Rationale

Clear architecture boundaries improve discoverability, reduce cognitive load, and enable easier testing and maintenance. Typed components prevent runtime errors from mismatched data contracts.

---

## Principle 3: Layout Performance Optimization

**MUST optimize AntV X6 graph layout for performance and strict visual consistency with unified vertical spacing.**

### Rules

**3.1 Strict Vertical Spacing Enforcement**

- Vertical spacing between consecutive levels MUST be exactly **50px**, regardless of individual node heights.
- Node Y-coordinates MUST be calculated as: `y = levelIndex * 50 + baseOffset`
- This spacing MUST be enforced even when nodes have varying heights (e.g., small action nodes vs. large container nodes).
- Horizontal spacing (nodesep) MAY be configurable but MUST default to 75px for consistency.
- **CRITICAL**: The implementation MUST NOT use `ranksep` or other parameters that could vary spacing based on node dimensions.

**3.2 Coordinate System and Alignment**

- Node positions MUST be centered on their layout coordinates.
- For nodes with width `w` and height `h` at layout position `(layoutX, layoutY)`:
  - Final X-coordinate: `x = layoutX - w / 2`
  - Final Y-coordinate: `y = layoutY - h / 2`
- This ensures consistent visual alignment regardless of node size variations.

**3.3 Parallel Branch Handling**

- Parallel branches MUST be assigned to the same level level.
- All nodes within a parallel branch MUST maintain the 50px vertical spacing rule relative to parent/child nodes.
- Branch width MUST be calculated dynamically to accommodate all nodes at a given level.

### Rationale

**Visual Consistency**: Strict 50px vertical spacing ensures consistent visual hierarchy regardless of node content or size. Users can predictably understand flow progression without being distracted by irregular spacing.

**Current Implementation Gap**: The existing `editor.service.ts:61-92` uses `DagreLayout` with `ranksep: 35`, which does NOT enforce strict vertical spacing and varies spacing based on node dimensions. This violates the 50px rule and MUST be replaced with a custom layout engine.

---

## Principle 4: Code Consistency and Style

**MUST maintain consistent code style and formatting.**

### Rules

- All code MUST follow Prettier configuration:
  - Print width: 100 characters
  - Single quotes for strings
  - Angular parser for HTML files
- File names MUST use kebab-case (e.g., `approval-flow.component.ts`).
- Classes MUST use PascalCase (e.g., `ApprovalFlowComponent`).
- Interfaces MUST use PascalCase with `I` prefix (e.g., `IApprovalNode`).
- All public APIs MUST have TSDoc comments with:
  - Description
  - Parameter types and descriptions
  - Return type and description
  - Usage examples where appropriate
- Comments MUST be concise and explain "why" not "what".
- Dead code MUST be removed before commit.

### Rationale

Consistent code style improves readability and reduces merge conflicts. Documentation ensures maintainability by future developers who may not have context of the original implementation.

---

## Principle 5: Form Safety with @delon/form

**MUST ensure type-safe dynamic forms using @delon/form.**

### Rules

- SFComponent instances MUST use generic parameters: `SFComponent<SchemaType, FormDataType>`.
- Schema definitions MUST be typed interfaces, not dynamic objects.
- Custom widgets MUST have type-safe props definitions.
- Form validation MUST use strongly-typed validators.
- Form data MUST be bound to typed models, preventing implicit `any`.

### Rationale

@delon/form provides powerful dynamic form capabilities, but without type safety it can become a source of runtime errors. Generic typing ensures form data matches expected schema contracts.

---

## Principle 6: RxJS Reactive Patterns

**MUST use type-safe RxJS patterns for reactive programming.**

### Rules

- Observable chains MUST use typed `pipe()` operators.
- Generic type inference MUST be preferred over explicit typing in pipe chains.
- Explicit type declarations MUST be used at public API boundaries.
- Subscription cleanup MUST be implemented via `takeUntilDestroyed()` or `AsyncPipe`.
- Error handling MUST be typed with specific error types where applicable.

### Rationale

Type-safe RxJS prevents runtime errors from mismatched observable emissions and ensures reactive flows are maintainable and testable.

---

## Governance

### Amendment Procedure

1. Any proposed amendment MUST be documented with clear rationale.
2. Amendments MUST be reviewed and approved by the project lead or architecture team.
3. Version MUST be incremented according to semantic versioning rules:
   - MAJOR: Backward incompatible governance/principle removals or redefinitions
   - MINOR: New principle/section added, materially expanded guidance, or principle numbering changes
   - PATCH: Clarifications, wording, typo fixes, non-semantic refinements
4. All amendments MUST update the `Last Amended Date` field.

### Versioning Policy

- Constitution version MUST follow semantic versioning (MAJOR.MINOR.PATCH).
- Template files MUST be kept in sync with constitution changes.
- Sync Impact Report MUST be included in each amendment as an HTML comment.

### Compliance Review

- Code reviews MUST check compliance with applicable principles.
- CI/CD pipelines SHOULD include automated checks where possible (e.g., strict mode violations).
- Non-compliant code MUST be addressed before merge.

---

## Principle Reference Mapping

This section maps technical concepts to governing principles for quick reference:

| Technical Concept | Governing Principle |
|-------------------|---------------------|
| TypeScript strict mode | Principle 1: Type Safety First |
| Generic type parameters | Principle 1: Type Safety First |
| Component organization | Principle 2: Component Architecture Standards |
| ng-zorro-antd usage | Principle 2: Component Architecture Standards |
| AntV X6 layout engine | Principle 3: Layout Performance Optimization |
| Strict 50px vertical spacing | Principle 3: Layout Performance Optimization |
| RxJS patterns in layouts | Principle 3: Layout Performance Optimization |
| Prettier configuration | Principle 4: Code Consistency and Style |
| TSDoc comments | Principle 4: Code Consistency and Style |
| @delon/form generics | Principle 5: Form Safety with @delon/form |
| Observable typing | Principle 6: RxJS Reactive Patterns |

---

## Appendix: Technical Stack Reference

This constitution governs development using the following technical stack:

- **Framework**: Angular 19+ with standalone components
- **Language**: TypeScript 5.7+ (strict mode)
- **UI Library**: ng-zorro-antd 21+
- **Form Library**: @delon/form (dynamic forms)
- **Graph Engine**: AntV X6 3.1+
- **Layout Engine**: @antv/layout 0.3+
- **Build Tool**: Angular CLI 21.0+
- **Package Manager**: npm 11.6.1
- **Test Runner**: Vitest (via Angular CLI)
- **Code Formatter**: Prettier

---

**End of Constitution**
