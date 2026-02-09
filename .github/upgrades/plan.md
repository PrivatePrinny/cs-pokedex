# .github/upgrades/plan.md

Table of contents

- Executive Summary
- Migration Strategy
- Detailed Dependency Analysis
- Project-by-Project Plans
- Package Update Reference
- Breaking Changes Catalog
- Testing & Validation Strategy
- Risk Management
- Complexity & Effort Assessment
- Source Control Strategy
- Success Criteria
- Tasks (high-level)


## Executive Summary

Selected Strategy: **All-At-Once Strategy** — All projects upgraded simultaneously in a single coordinated operation.

Rationale:
- Solution size: 1 project (small)
- Current target: `net8.0`
- Target framework: `.NETCoreApp, 10.0` (upgrade to .NET 10.0 LTS recommended)
- No NuGet package incompatibilities or API issues reported in assessment
- Blazor-based AspNetCore project present; upgrade surface is small

Scope:
- All projects in repository (single project `cs-pokedex`) will be upgraded atomically to `.NETCoreApp, 10.0`.

Deliverable:
- `cs-pokedex` builds and tests successfully targeting `.NETCoreApp, 10.0` with zero remaining security vulnerabilities reported in assessment.


## Migration Strategy

Approach: **All-At-Once** (atomic upgrade)
- Update all project TFMs and package references in a single coordinated change.
- Restore, build, fix compilation issues, and verify in the same upgrade pass.

Justification:
- Single SDK-style Blazor/AspNetCore project (low complexity)
- Assessment reports no package or API incompatibilities
- Faster, lower coordination overhead for single-project solutions

Key constraints and principles:
- Respect project dependency ordering (leaf-first) — trivial here since there are no project dependencies.
- Apply all package updates flagged by assessment (none listed).
- Use a single atomic commit/branch to contain the upgrade changes (see Source Control Strategy).


## Detailed Dependency Analysis

Summary:
- Projects discovered: 1
- Project: `cs-pokedex\\cs-pokedex.csproj` — SDK-style AspNetCore project targeting `net8.0`
- Project dependencies: none (no project-to-project references)
- Test projects: none discovered

Impact:
- No dependency chains to consider. Upgrading the single project is an atomic operation.

Dependencies:
- `Microsoft.NET.Sdk.Functions`: Extension for running functions on Azure, added in response to assessment findings.
- `Microsoft.NET.Sdk.StaticWebApps`: SDK for Azure Static Web Apps, also added based on assessment recommendations.

Action:
- Add the necessary SDKs and ensure they're correctly configured in the project file.
- Validate that no additional dependencies are introduced that could complicate the upgrade.


## Project-by-Project Plans

### Project: `cs-pokedex\\cs-pokedex.csproj`

Current State:
- TargetFramework: `net8.0`
- SDK-style: True
- Project type: AspNetCore (Blazor components present)
- Packages: none reported by assessment
- LOC: 99

Target State:
- TargetFramework: `.NETCoreApp, 10.0` (TFM `net10.0`)
- Package versions: apply suggested versions from assessment (none)

Migration Steps (to be executed atomically across the repo):
1. Prerequisites (local dev machine / CI): Ensure .NET 10 SDK is installed or update `global.json` to reference a compatible SDK.
   - ?? If `global.json` exists, update or validate it to include a .NET 10 SDK, otherwise add a `global.json` specifying the required SDK.
2. Update project file `TargetFramework` to `net10.0` (or append `net10.0` if multi-targeting is needed).
3. Update any package references per assessment (none required). If any package updates are necessary, pick the suggested versions from assessment.
4. Restore dependencies (`dotnet restore`) and build the solution to discover compilation errors.
5. Fix all compilation errors that arise from framework/package updates.
6. Rebuild and verify solution builds with 0 errors.
7. Run all test projects (none detected) and address test failures.
8. Finalize: update documentation (README, changelog), and create a single atomic commit/PR.

Validation Checklist (per project):
- [ ] `TargetFramework` updated to `net10.0`
- [ ] Solution restores and builds without errors
- [ ] All unit/integration tests pass (if present)
- [ ] No security vulnerabilities remain (per assessment)


## Package Update Reference

Assessment reported no NuGet packages requiring updates. If additional packages are found during build, update them as follows:
- Group updates by scope (common/shared packages first)
- Record: package name, current version, target version, projects affected, reason

(Empty for current assessment)


## Breaking Changes Catalog

Assessment did not report binary/source incompatible APIs for the existing project. Typical areas to watch for when moving from net8.0 ? net10.0 (general guidance):
- ASP.NET Core startup/config patterns (minimal hosting model adjustments) — Blazor: confirm `Program.cs` and `builder.Services` registrations are compatible
- Obsolete APIs removed between runtime versions
- Third-party package API surface changes (none reported)

Action: During build, capture compilation errors and map them to breaking-change remediation tasks.


## Testing & Validation Strategy

Levels:
- Local developer build and smoke test
- CI pipeline: restore ? build ? test

Checklist:
- Build the solution; expect 0 errors
- Run test projects (none discovered) — if none, rely on manual smoke checks or add basic automated tests before upgrade
- Validate Blazor UI starts (manual/automated) — note: manual checks are not converted into tasks but should be performed by the executor


## Risk Management

Risk summary:
- Overall risk: **Low** (single small Blazor project, no package/API issues reported)

Potential risks and mitigations:
- Missing .NET 10 SDK on dev/CI machines — Mitigation: validate SDK presence and update `global.json`.
- Unexpected package compatibility issues discovered during build — Mitigation: keep a short troubleshooting loop within the atomic upgrade task, have suggested replacement versions ready.
- Behavioral changes at runtime not detected by compilation — Mitigation: run smoke tests and, if available, automated UI checks.

Rollback plan:
- If upgrade causes blocking compilation or runtime issues, revert the upgrade branch or reset the single atomic commit and open targeted follow-up tasks.


## Complexity & Effort Assessment

- Overall complexity: **Low**
- Project complexity: `cs-pokedex` — Low (LOC 99, limited dependencies, Blazor UI present)
- No high-risk packages or large code churn expected


## Source Control Strategy

Repository state:
- Assessment initialization indicated: **No Git repository found** in the environment where analysis ran.

Recommendations:
- If the codebase is not in Git, initialize a Git repository before performing changes and create an upgrade branch (e.g., `upgrade/net10`).
- If Git is available locally for you, create a single feature branch for the atomic upgrade and perform a single logical commit containing all project file and package changes.
- PR/Review: open a PR from the upgrade branch into main; attach build results and test run summaries.

Commit guidance for atomic change:
- Single commit containing:
  - `csproj` TargetFramework edits
  - Any updated `global.json`
  - Package reference updates
  - Minor code fixes required to compile
- Commit message: "Upgrade: target net10.0 — atomic upgrade of all projects"


## Success Criteria

The migration is complete when all of the following are true:
- `cs-pokedex` projects target `net10.0` (TFM `.NETCoreApp, 10.0`)
- All package updates from assessment (none) applied
- Solution restores and builds with 0 errors
- All tests pass (if present)
- No known security vulnerabilities remain for referenced packages


## Tasks (high-level)

Per All-At-Once strategy, consolidate framework & package updates into one atomic task plus prerequisites and testing tasks.

TASK-000: Prerequisites
- Validate/Install .NET 10 SDK on developer and CI machines
- Update or add `global.json` to reference .NET 10 SDK (if desired)
- If using Git: create upgrade branch `upgrade/net10` and ensure working tree is clean (commit/stash pending changes per your policy)

TASK-001: Atomic framework and package upgrade (single pass)
- (1) Update `TargetFramework` to `net10.0` in `cs-pokedex\\cs-pokedex.csproj`
- (2) Update package references across projects as specified in plan (none in assessment)
- (3) Restore dependencies
- (4) Build solution and fix all compilation errors caused by framework/package upgrades
- (5) Rebuild and verify solution builds with 0 errors
- Deliverable: Solution builds clean targeting `net10.0`

TASK-002: Test execution and validation
- Run unit and integration tests
- Perform smoke validation of Blazor UI (manual or automated)
- Address any test failures or runtime regressions
