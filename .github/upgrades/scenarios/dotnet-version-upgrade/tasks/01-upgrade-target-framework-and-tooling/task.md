# 01-upgrade-target-framework-and-tooling: Upgrade target frameworks and build/runtime configuration

Update all projects in `WebApi.sln` from `net8.0` to `net10.0`, including any shared SDK/runtime configuration files used by local and CI builds (for example `global.json`, `Directory.Build.*`, and pipeline build settings where present). This task covers framework and configuration alignment only.

The main concern is ensuring every project and shared build input agrees on .NET 10 so restore/build tooling behaves consistently. The task should also verify that no project is left on net8.0 by mistake.

## Scope Inventory
- **Projects affected**: `Model/Model.csproj`, `WebApi/ViviliamAPI.csproj`, `ViviliamTest/ViviliamTest.csproj`.
- **Distinct concerns**: target framework updates in SDK-style project files; repository-level SDK/build config scan for `.NET 8` references.
- **Change signals from assessment**:
  - Model: `Project.0002` (mandatory TFM update only).
  - ViviliamAPI: `Project.0002` mandatory plus additional potential API/package issues to address in follow-up task.
  - ViviliamTest: `Project.0002` mandatory plus optional deprecated package notices (follow-up task).

## Research Findings
- Current TFM declarations found in all three projects at `<TargetFramework>net8.0</TargetFramework>`.
- No `TargetFrameworks` multi-targeting declarations were found.
- No shared build/runtime config files were found in repo root for this workspace scan (`global.json`, `Directory.Build.props`, `Directory.Build.targets`, common CI pipeline definitions).
- This task will be atomic: only framework/config alignment edits, with compatibility/package/code fixes deferred to task `02-resolve-compatibility-and-validate`.

**Done when**: All project and shared build configuration files in scope consistently target .NET 10 with no remaining `net8.0` target declarations.
