# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade `WebApi.sln` from .NET 8 to .NET 10.
**Scope**: 3 SDK-style projects (`Model`, `ViviliamAPI`, `ViviliamTest`) with shallow project dependencies.

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: Small modern-to-modern solution where a coordinated TFM/package/API update is simpler than phased migration.

## Upgrade Options

| Option | Selected | Why |
|--------|----------|-----|
| Upgrade Strategy | All-at-Once | All projects are already on modern .NET with low structural migration risk. |
| Package Management | Per-Project (defer CPM to post-migration) | Current project count does not justify CPM churn during this upgrade. |
| Unsupported Packages | Resolve Inline | Only two incompatible package cases were flagged, so inline resolution is practical. |
| Unsupported API Handling | Fix Inline | API changes should be addressed directly during the upgrade pass to avoid deferred stubs. |

## Tasks

### 01-upgrade-target-framework-and-tooling: Upgrade target frameworks and build/runtime configuration

Update all projects in `WebApi.sln` from `net8.0` to `net10.0`, including any shared SDK/runtime configuration files used by local and CI builds (for example `global.json`, `Directory.Build.*`, and pipeline build settings where present). This task covers framework and configuration alignment only.

The main concern is ensuring every project and shared build input agrees on .NET 10 so restore/build tooling behaves consistently. The task should also verify that no project is left on net8.0 by mistake.

**Done when**: All project and shared build configuration files in scope consistently target .NET 10 with no remaining `net8.0` target declarations.

### 02-resolve-compatibility-and-validate: Resolve compatibility issues and validate solution

Restore packages, apply minimal package/version/code fixes needed for .NET 10 compatibility, and keep changes idiomatic to the existing code style. This includes handling compile or package issues surfaced by the framework upgrade, then running full build validation and relevant tests.

Assessment signals indicate package update recommendations and source/behavioral compatibility findings concentrated in `ViviliamAPI`, so fixes should be focused and minimal rather than broad refactors.

**Done when**: Restore succeeds, solution builds without errors/warnings in modified projects, relevant tests run successfully, and remaining manual follow-ups are documented.
