# cs-pokedex .NET 10.0 Upgrade Tasks

## Overview

This document tracks the execution of upgrading the `cs-pokedex` project to .NET 10.0. The work includes prerequisites verification, a single coordinated framework and package update with compilation fixes, followed by test validation.

**Progress**: 3/4 tasks complete (75%) ![75%](https://progress-bar.xyz/75)

---

## Tasks

### [✓] TASK-001: Verify prerequisites *(Completed: 2026-01-10 21:00)*
**References**: Plan §Project-by-Project Plans, Plan §Migration Strategy, Plan §Detailed Dependency Analysis

- [✓] (1) Verify required .NET 10 SDK is installed per Plan §Project-by-Project Plans
- [✓] (2) Runtime/SDK version meets minimum requirements (**Verify**)
- [✓] (3) If a `global.json` file exists, update or create it to reference a compatible .NET 10 SDK per Plan §Project-by-Project Plans
- [✓] (4) `global.json` references .NET 10 SDK or no `global.json` required (**Verify**)

### [✓] TASK-002: Atomic framework and package upgrade with compilation fixes *(Completed: 2026-01-10 21:01)*
**References**: Plan §Project-by-Project Plans, Plan §Migration Strategy, Plan §Package Update Reference, Plan §Breaking Changes Catalog

- [✓] (1) Update `TargetFramework` to `net10.0` in `cs-pokedex\cs-pokedex.csproj` per Plan §Project-by-Project Plans
- [✓] (2) Update package references per Plan §Package Update Reference (none reported; update if new issues discovered)
- [✓] (3) Restore dependencies per Plan §Migration Strategy
- [✓] (4) Build solution and fix all compilation errors caused by framework/package upgrades per Plan §Breaking Changes Catalog
- [✓] (5) Solution builds with 0 errors (**Verify**)

### [✓] TASK-003: Run test suite and validate upgrade *(Completed: 2026-01-10 22:01)*
**References**: Plan §Testing & Validation Strategy, Plan §Detailed Dependency Analysis

- [✓] (1) Run all test projects in the solution per Plan §Detailed Dependency Analysis
- [✓] (2) Fix any test failures (reference Plan §Breaking Changes Catalog for common issues)
- [✓] (3) Re-run tests after fixes
- [✓] (4) All tests pass with 0 failures OR no test projects present (**Verify**)

### [✗] TASK-004: Final commit
**References**: Plan §Source Control Strategy, Plan §Project-by-Project Plans

- [✗] (1) Commit all remaining changes with message: "TASK-004: Upgrade: target net10.0 - atomic upgrade of `cs-pokedex`"








