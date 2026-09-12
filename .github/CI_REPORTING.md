# CI tooling and reporting

The required merge check remains `Validate PR / Build and Test`. Each build runs the full Release test suite once. Master builds add XPlat coverage collection to that same invocation; PR validation does not collect coverage. Both test projects already include the coverage collector.

## Reporting policy

Codecov is retained for history, but its availability does not gate production. Its upload has a bounded timeout and reports a warning on failure. Coverage collection, conversion and retention on successful coverage builds remain required. No coverage threshold is introduced.

TRX files replace the separate `Test Results` check and PR comments. Build jobs and callers require only `contents: read`; no reporting write permission or fork PR secret is required. Test failures remain authoritative: test execution never uses `continue-on-error`. Diagnostic upload failure is non-blocking and visible in the job summary.

| Artifact | Contents | Retention | Failure policy |
| --- | --- | --- | --- |
| test-results | TRX files | 14 days | Best effort, including failed tests |
| raw-coverage | Collector Cobertura files | 14 days | Best effort, including failed tests |
| coverage-report | Merged Cobertura and HTML | 14 days | Required for successful coverage builds |
| .net-app | Published application and version.json | 30 days | Required for deployment |
| production-smoke | Tested smoke executable | 30 days | Required for deployment |

Download coverage-report and open index.html for human review. Codecov consumes its Cobertura.xml. Reports are scoped to the current invocation's test-results directory. Missing or invalid coverage is a build failure, not an optional service failure.

## Tool maintenance

External actions are pinned to full commit SHAs with release comments. Trusted publishers are GitHub Actions (`actions`), Microsoft Azure (`azure`), and Codecov (`codecov`). The repository maintainer reviews upstream release notes, security advisories, commit provenance, runtime requirements and downloaded dependencies before accepting updates. Weekly Dependabot PRs propose GitHub Actions changes; they are not automatically merged.

ReportGenerator is restored from `.config/dotnet-tools.json`, never installed globally. Version 5.5.11 supports .NET 8; its release notes and NuGet vulnerability metadata were checked on 2026-09-07 (no vulnerability entry was returned). Review and update this manifest and the pinned Codecov CLI version manually alongside action updates. Preserve Codecov's binary signature verification.

Action/tool pins do not freeze GitHub-hosted runner images, the .NET 8 servicing channel, or all application NuGet dependencies. Framework upgrades and Azure authentication changes are separate work.

## Verification

1. Run actionlint, dependency restore, Release build and the complete Release suite. For coverage validation, use `dotnet test --no-build --configuration Release --logger trx --results-directory test-results --collect:"XPlat Code Coverage"`, then `dotnet tool restore` and local ReportGenerator.
2. Confirm the PR check retains its exact required name and executes the suite once with read-only permissions.
3. Manually dispatch the Azure workflow on the feature branch to exercise coverage and both deployable artifacts; the master-only deployment guard must skip deployment.
4. In an isolated verification branch, induce a failing test and a diagnostic publishing failure; verify the test step and required check fail. Then restore passing tests and induce a Codecov upload failure; verify a warning and successful build. Never change the production guard or expose secret values for these tests.
5. Verify a missing coverage report or required artifact fails the build. Record hosted run links in the PR and distinguish exercised cases from untested ones.

Deployment serialization and superseded-commit checks continue across staging, Production approval, promotion and recovery. See [branch protection](BRANCH_PROTECTION.md), [production deployment](PRODUCTION_DEPLOYMENT.md), and the mandatory [staging and rollback setup](STAGING_AND_ROLLBACK.md). Deployment artifacts retain the existing 30-day limit; missing recovery artifacts block promotion.
