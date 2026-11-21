# .NET Version Upgrade

## Upgrade Options

### Strategy
- Upgrade Strategy: All-at-Once

### Project Structure
- Package Management: Per-Project (defer CPM to post-migration)

### Compatibility
- Unsupported Packages: Resolve Inline (2 incompatible packages)
- Unsupported API Handling: Fix Inline

## Strategy
**Selected**: All-At-Once
**Rationale**: 3 SDK-style projects already on net8.0 with a shallow dependency graph can be upgraded in a single atomic pass.

### Execution Constraints
- Update all projects and shared build configuration in one coordinated pass.
- Restore and resolve package/API compatibility issues inline (no deferred stubs).
- Complete full solution build and test validation after code/package updates.

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0

## Source Control
- **Source Branch**: net10
- **Working Branch**: net10
- **Commit Strategy**: After Each Task
- **Branch Sync**: Auto (Merge)
