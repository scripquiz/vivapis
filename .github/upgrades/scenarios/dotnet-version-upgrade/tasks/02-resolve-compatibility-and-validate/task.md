# 02-resolve-compatibility-and-validate: Resolve compatibility issues and validate solution

Restore packages, apply minimal package/version/code fixes needed for .NET 10 compatibility, and keep changes idiomatic to the existing code style. This includes handling compile or package issues surfaced by the framework upgrade, then running full build validation and relevant tests.

Assessment signals indicate package update recommendations and source/behavioral compatibility findings concentrated in `ViviliamAPI`, so fixes should be focused and minimal rather than broad refactors.

## Scope Inventory
- **Projects affected**: `WebApi/ViviliamAPI.csproj`, `ViviliamTest/ViviliamTest.csproj` (plus dependent build impact on `Model`).
- **Distinct concerns**:
  - NuGet package version updates recommended for .NET 10.
  - Potential source/behavior changes concentrated in SQL client and ASP.NET pipeline areas.
  - Full restore/build/test validation after updates.
- **Package management mode**: Per-project `PackageReference` (no Central Package Management file detected).

## Research Findings
- `ViviliamAPI` has explicit package references currently pinned to .NET 8-era versions; assessment recommends upgrades for:
  - `Microsoft.EntityFrameworkCore` and `Microsoft.EntityFrameworkCore.SqlServer` → `10.0.15`.
  - `Microsoft.Extensions.Configuration` and `Microsoft.Extensions.Configuration.Json` → `10.0.15`.
  - `System.Configuration.ConfigurationManager` → `10.0.15`.
  - `Newtonsoft.Json` → `13.0.4`.
- `ViviliamTest` flags deprecated MSTest packages (`MSTest.TestAdapter`, `MSTest.TestFramework` 3.1.1), so test package updates may be needed for clean restore/build/test on net10.
- Assessment reported high source-incompatible counts in `ViviliamAPI` primarily around `System.Data.SqlClient`; actual compile diagnostics after restore/build will drive minimal code/package changes.
- No decomposition required: this task can be executed atomically as package updates + compile fix pass + validation.

**Done when**: Restore succeeds, solution builds without errors/warnings in modified projects, relevant tests run successfully, and remaining manual follow-ups are documented.
