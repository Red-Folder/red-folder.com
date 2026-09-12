# Staging promotion and artifact recovery (#33)

## Topology and costs

Read-only Azure inspection on 2026-09-07 found two separate Windows App Services:

| App | Resource group | Hosting plan |
| --- | --- | --- |
| RFC-Website | RFC-Website | RFC-Website, B1 Basic, one instance |
| RFC-Website-Staging | RFC-Website-Staging | The same RFC-Website B1 plan |

A similarly named RFC-Website-Staging F1 Free plan exists, but the staging app is attached to B1. This change does not move apps, create infrastructure, upgrade plans, or delete resources. Basic does not support deployment slots. Promotion and recovery deploy an artifact to a separate app; they are not slot swaps and may briefly interrupt service.

Reusing these apps requires no additional App Service instance charge. Both consume the same CPU/memory: check headroom before rehearsal and do not enable autoscaling or upgrade the tier as part of this change. Extra telemetry, outbound dependency calls, and GitHub artifact/log storage can incur usage charges. Standard GitHub-hosted runners are free for this public repository. Keep the existing 30-day deployment artifact retention and check account storage usage; no paid storage or monitoring service is introduced.

References: [Azure hosting plans](https://learn.microsoft.com/en-us/azure/app-service/overview-hosting-plans), [deployment slot requirements](https://learn.microsoft.com/en-us/azure/app-service/deploy-staging-slots), [GitHub Actions billing](https://docs.github.com/en/billing/concepts/product-billing/github-actions).

## Manual prerequisites before merging

The PR changes code only. An operator must complete the following setup before merging; missing configuration or a recovery baseline blocks deployment.

1. **Establish a verified production baseline.** Inspect the current production version and run the existing smoke suite. The latest inspected deployment, [run 34109231937](https://github.com/Red-Folder/red-folder.com/actions/runs/34109231937), uploaded commit `aef39e9c3b0cd66292a8387ae81f874c2911bb97` but failed early route checks with timeouts. This is not a verified baseline. Investigate readiness/dependency failures, then obtain a successful current-master build/deploy/smoke run using the existing workflow before merging this PR. Record its run ID and full SHA, and confirm `.net-app` is retained. Do not mark a failed run good manually or rebuild old code and call it the same artifact.
2. **Update staging runtime configuration.** Azure reported `netFrameworkVersion: v5.0` for staging. In the Azure portal, verify the stack is .NET 8 for the framework-dependent application and save the appropriate runtime setting. Confirm HTTPS works on `https://rfc-website-staging.azurewebsites.net` without redirecting to production. Keep ASPNETCORE_ENVIRONMENT set to Staging (Production also uses the safe error handler); do not use Development. Do not change the B1 plan.
3. **Review staging settings securely.** Compare configuration in Azure without copying values to GitHub comments, logs or artifacts. Review the settings below. Separate apps keep their own configuration, so there are no slot-sticky settings to configure.
4. **Create the GitHub `Staging` environment.** Permit only the `master` branch, with no tag rule. Add environment secret `AZURE_STAGING_PUBLISH_PROFILE` containing the staging app's publish profile. Add environment variable `STAGING_URL` set to `https://rfc-website-staging.azurewebsites.net` (or its verified canonical HTTPS hostname). This distinct secret prevents accidental fallback to production credentials.
5. **Protect `Production`.** Preserve its existing master-only branch restriction and add required reviewers for the initial rollout. On 2026-09-07 it had a branch policy but no reviewers. Choose a reviewer who can approve the run; if the owner must approve their own dispatch, do not enable prevent-self-review. Preserve the production `AZURE_WEBAPP_PUBLISH_PROFILE` secret, preferably scoped to Production. Leave `ENABLE_AUTOMATIC_ROLLBACK` absent or `false` until rehearsal evidence is recorded.
6. **Check recovery retention and capacity.** Ensure the baseline artifact is unexpired, download it before the retention deadline for emergency custody, and record its identity/digest. Check B1 CPU/memory and telemetry/storage budgets. Do not add a paid resource to resolve a failure without a separate cost decision.
7. **Drain historical deployment runs.** Ensure no old workflow revision is actively deploying before merging. Do not rerun pre-lock revisions. Merge through the protected PR process only after the required check passes.

| Configuration | Staging review |
| --- | --- |
| ConnectionStrings:RepoContext | Owner accepted the existing shared production connection as a temporary exception on 2026-09-12 because this legacy feature is unchanged and due for removal. Do not provision a database for this rollout. |
| BlogUrl and media/content settings | Ensure the GET smoke routes can render using approved existing content sources; inspect MediaRoot availability and access. |
| SendGridApiKey / SendGridFromEmailAddress | Disable real outbound sending or use an existing safe test configuration; smoke checks never submit the contact form. |
| ReCaptchaSecretKey | Use a suitable existing test/domain configuration if contact functionality is enabled; outside smoke coverage. |
| Application Insights connection/settings | Check destination, volume and sampling; do not create a new paid monitoring resource. |
| ASPNETCORE_ENVIRONMENT and runtime | Production exception handling, .NET 8 runtime; no developer diagnostics. |

Staging is a live app: read-only smoke requests alone do not protect it from other visitors or background work. Review access restrictions and outbound side effects before deployment. If restrictions require authentication, arrange an existing runner-accessible configuration; the smoke suite does not bypass authentication or certificate validation. Do not purchase private networking for this change.

The shared RepoContext exception is not database isolation: constructing this context calls Database.Migrate(), including when the repository API is requested. The owner explicitly accepted leaving it unchanged. The deployment smoke routes do not use that API. Revisit the exception if repository/database code changes before the feature is removed.

## Release policy

Build once, deploy the retained package to staging, use the smoke executable's `15 120` arguments to wait up to 120 seconds for readiness and the expected version, and run the complete smoke contract against the expected commit. Only then request Production approval. After approval, recheck current master and staging, capture the current production SHA and its verified retained artifact, then deploy the identical candidate package. Missing/expired recovery evidence fails before production mutation.

The workflow holds `azure-rfc-website-production` with `cancel-in-progress: false` through staging, approval, promotion, verification and recovery. The manual recovery workflow shares this group. Waiting approval therefore blocks subsequent deployment runs; reject obsolete approvals to release the lock. A newly merged commit does not interrupt an active Azure operation. A superseded candidate must not promote. Do not put the same concurrency lock inside a job already holding it.

On failed production verification, automatic rollback is allowed only when `ENABLE_AUTOMATIC_ROLLBACK` is exactly `true`. It redeploys the captured package and verifies the original SHA. The failed release remains failed even if recovery succeeds. With rollback disabled, the run reports that production may be serving a failed release and requires operator action. An upload failure, cancellation or timeout can leave uncertain Azure state: inspect Azure deployment activity and the live version before starting another recovery; do not assume cancellation stopped Azure.

## Rehearsal and enabling rollback

After merge, select **Recover or rehearse a verified release** (`recover-release.yml`) in GitHub Actions, click **Run workflow**, choose **master**, set `mode` to `rehearsal`, and supply `source_run_id` and `expected_sha` for a retained verified baseline. It uses only Staging: establish and verify the baseline, deploy and verify a different current-master candidate, deliberately fail the verification gate, then restore and verify the baseline. A verified rehearsal must show both versions and the recovery result. **The run intentionally remains failed** because it injects a failure; require `REHEARSAL VERIFIED` in the summary and successful restoration smoke checks. Do not treat a generic red run as rehearsal evidence or use identical candidate/baseline SHAs as evidence of restoration.

The ordinary production workflow may be waiting for approval while you want to rehearse. Reject that pending approval and allow the run to finish before dispatching rehearsal; both intentionally share the deployment lock. Do not cancel an active Azure deployment. After successful rehearsal, record the run URL, candidate and baseline SHAs, artifact IDs/digests and final staging version. Set Production variable `ENABLE_AUTOMATIC_ROLLBACK` to `true` only after reviewing this evidence. Dispatch the current master deployment and approve only after staging passes.

Rehearsal is not proof of production credentials, environment protections, capacity or recovery under an Azure outage. Retain evidence from the first controlled production promotion, including `/api/version` and successful smoke results. Test approval rejection, superseded candidates and failed staging checks without deliberately breaking production.

## Manual recovery and artifact expiry

Dispatch recovery mode on current **master**, supplying the original successful deployment run ID and its full expected SHA, and approve Production. The workflow validates the source rather than running historical scripts. It deploys the retained package and checks its original version. Recovery deliberately permits an older artifact; retrying an old deployment workflow is not this recovery path. Inspect pending automatic releases before recovery: a queued release can deploy after the recovery completes. Reject/clear obsolete pending releases and prepare the corrective PR before resuming normal deployment.

The manual workflow builds and tests current master to obtain trusted current smoke tooling (and the rehearsal candidate). A broken master build, unavailable dependency feed, GitHub outage or expired artifact can therefore block this automated recovery path. In that case, use the preserved known-good package with the existing Azure deployment process under operator control, verify the original SHA and smoke results, and record the intervention. Do not execute tooling from a historical downloaded release or rebuild the recovery package.

Artifacts expire after 30 days. This is an intentional cost ceiling, not indefinite recovery storage. Before expiry, maintain an operator-held copy of the known-good artifact and identity record under existing storage arrangements. The automated workflow requires a retained GitHub artifact and fails closed when it cannot retrieve it; a local copy is for documented operator-led emergency recovery only, not an automatic validation bypass. If a release remains live beyond retention, establish a fresh verified baseline through a separately reviewed recovery/bootstrap operation using the preserved exact package before enabling another normal promotion. Do not temporarily remove recovery checks or rerun historical workflows. Any move to longer retention or external storage requires a storage-cost review.

Rollback restores application files, not database contents, App Service configuration or third-party side effects. Framework/storage migrations and incompatible schema changes are outside #33 and require separate recovery planning.

## Setup progress (2026-09-12)

- PR #61 merged. Baseline [run 34703180902](https://github.com/Red-Folder/red-folder.com/actions/runs/34703180902) passed tests, deployment and production smoke verification for `2efe06b5628344c235cfef908908fdc0b091e06b`; production version matched. Artifact `.net-app` ID `10301360959` is retained until 2026-10-12T15:46:29Z, with digest `sha256:e78ba4405b0c3aae1af9e6d2afa58f21f37332327aff955efba13aad8e61b2c6`.
- Azure runtime update verified as v8.0. Staging environment mode is Staging. Owner confirmed blog/media review and disabled email configuration.
- Both GitHub environments have exact master branch rules. Owner confirmed staging secret/URL and disabled rollback variable setup; the CLI token cannot list environment secrets/variables (HTTP 403), so those values were not independently verified.
- Production reviewer gate verified after the owner saved it: required reviewer Red-Folder, prevent_self_review false, master-only branch rule preserved. Administrator bypass remains available; use normal approval/rejection rather than bypassing the gate.

## Verification record

Local verification after integrating PR #61 on 2026-09-12: `dotnet restore`, Release build and all 48 application tests passed. XPlat coverage was validated for the prerequisite. All 16 standard-library Python deployment-helper tests passed after removing the duplicate readiness implementation (`python -m unittest discover -s tools/deployment -p 'test_*.py' -v`). actionlint 1.7.12 passed the deployment, recovery and reusable build workflows (ShellCheck unavailable locally). Existing NuGet vulnerability, legacy framework and application-code warnings remain outside this change. The reusable build runs the new Python tests in PR and deployment validation.

Local and PR checks validate helper behavior and workflow syntax. Live staging deployment, reviewer gates, forced-failure restoration and production recovery require the manual setup above and are **not yet performed** by this PR. Add run URLs and observed outcomes here before closing #33.
