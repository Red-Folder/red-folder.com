# Production deployment

> Issue #33 extends this workflow with staging, gated promotion and artifact recovery. Follow [staging and rollback](STAGING_AND_ROLLBACK.md) for the current release/recovery procedure and required manual setup. The #31/#32 verification notes below are historical evidence; the recovery policy below is superseded by that runbook.

## Agreed policy

For issue #31, the repository owner selected completion of an active deployment with replacement of older pending deployments, and approved the `Production` environment restricted to `master`.

The production workflow holds the fixed repository concurrency group `azure-rfc-website-production` with `cancel-in-progress: false` throughout build and deployment. One run proceeds and at most one waits; another arriving run replaces the pending run. Serializing production builds prevents an older, slower build from replacing a newer pending deployment. PR validation runs independently, and manual non-production runs use separate groups per ref. Every workflow deploying RFC-Website must share the production group. Do not add cancellation that could interrupt an active deployment, or repeat the same lock at job level inside a workflow already holding it.

Queue arrival order is not commit order. Immediately before calling Azure, the job reads the current `master` SHA and skips a superseded commit, recording the reason in the run summary. A failed lookup fails the job before deployment. A new merge after this check does not interrupt the active deployment; its successor waits. Skipped jobs are not evidence of a release: check the Azure step and `/api/version`.

Pending replacement follows run arrival, so retrying an obsolete revision can still replace the pending current-master run. Avoid obsolete retries; if this happens, dispatch the workflow on current `master` again to restore the pending release.

Pushes to `master` and authorized manual dispatches on `master` can deploy, after the build and tests succeed. Manual feature-branch runs cannot enter this deployment job. GitHub requires repository write access to dispatch; Production's branch policy must additionally permit only the branch `master`, with no tag rules.

## Environment migration prerequisite

Do not merge the environment reference change before completing these settings:

1. Inspect Development's current approvals, wait timer, bypass setting, branch rules, secrets, and variables. Preserve any existing protection when creating Production.
2. Create `Production` under repository Settings > Environments. Select **Selected branches and tags**, and add a **Branch** rule matching exactly `master`. Add no tag rules. Preserve existing approvals and restrictions.
3. Ensure `AZURE_WEBAPP_PUBLISH_PROFILE` is available to Production. A repository secret remains available; an environment-scoped secret must be provisioned securely in Production by its owner. Secret values cannot be retrieved through GitHub APIs. Preserve any other required environment variables and secrets.
4. Verify the resulting settings before merging. Retain Development during verification to preserve history and avoid deleting settings prematurely.

On 2026-09-06 the environments API showed Development with no protection rules and no deployment branch policy; Production did not exist. The CLI token received HTTP 403 when listing secrets/variables and creating Production. No live environment change was made. The browser was signed out. The repository owner confirmed that `AZURE_WEBAPP_PUBLISH_PROFILE` is a repository secret, so it remains available to Production. Any additional Development secrets/variables and Production setup still need verification.

## Verification and closure evidence

Issue #30 is closed. Effective master rules were rechecked on 2026-09-06: PR required, required `Validate PR / Build and Test` check with strict up-to-date policy, linear history, and force-push prevention. See [branch protection](BRANCH_PROTECTION.md).

After environment setup and successful PR validation:

1. Ensure earlier workflow revisions have no active deployment runs before merging; historical revisions do not contain the concurrency lock. Do not rerun pre-lock revisions.
2. Merge through the protected PR process. Confirm the deployment uses Production and the displayed environment URL reaches RFC-Website. Confirm `/api/version` matches the deployed SHA.
3. Dispatch safe runs on the same tested master SHA close together. Observe one workflow run holding the concurrency group and another pending. For replacement evidence, submit a third while the second is still pending. If timing prevents overlap, repeat using the same good SHA; do not introduce broken code or weaken protections.
4. Record run URLs, SHAs, Azure step start/end times, the pending and replaced run states, and the final endpoint SHA. Verify the active run completed and Azure step intervals did not overlap. Two successful runs alone do not prove pending replacement.
5. Dispatch the unchanged workflow on its feature branch and confirm the deploy job is skipped. Inspect Production's exact master-only branch rule independently; retain both pieces of evidence.
6. Add observed results here before closing #31. Local checks do not prove GitHub concurrency or live environment behavior.

Live verification: **pending environment setup and merge**. No test deployments have been triggered for #31.

Local validation on 2026-09-06: restore and Release build passed, all 19 tests passed, actionlint passed for the deployment workflow, and the new Bash block passed syntax validation. Existing dependency vulnerability, legacy framework, and code warnings remain outside this change.

## Recovery

Production deployment now includes a post-deployment smoke check against `https://www.red-folder.com`, using the same `GITHUB_SHA` written into the application's version metadata. The smoke executable comes from a separate artifact built and tested in the same workflow run. It checks readiness, retained public pages, the deployed commit and retired Activity routes with explicit request and step timeouts. A superseded release skips both deployment and smoke verification. See the [smoke contract](../tools/RedFolder.Smoke/README.md) for exact assertions and limitations.

The workflow succeeds only if deployment and smoke verification succeed. A failed post-deployment check reports failure but leaves the deployed release in place; there is no automatic rollback. Live verification of this new step requires a merged production run. Staging, gated promotion and tested rollback remain in #33.

- Failed build: repair through a PR; no deployment runs until tests pass.
- Failed smoke verification: inspect the fixed route diagnostics in the smoke step and its workflow summary, then check application telemetry and the deployed version. The deployment may already be serving traffic. Repair through a new PR or use the bad-release recovery below; do not treat a successful Azure upload as successful verification.
- Failed deployment: inspect the Azure step and current site version. Retry the current master workflow after resolving the cause. Superseded runs skip deployment.
- Bad release: create a revert PR, pass required checks, and merge. The new master SHA deploys the restored code. Rerunning an old SHA is deliberately not a rollback path.
- Unexpected queue state: inspect all runs sharing the group; cancel obsolete pending runs if needed. If an obsolete retry replaced the current-master pending run, dispatch current master again. Do not cancel an active Azure operation merely to free the lock. If an operator cancels a run, confirm the Azure operation has actually stopped before starting recovery.
- Environment rejection or missing credentials: repair Production's configuration without removing branch restrictions or approval rules, then retry current master.

References: [GitHub concurrency](https://docs.github.com/en/actions/how-tos/write-workflows/choose-when-workflows-run/control-workflow-concurrency), [deployment environments](https://docs.github.com/en/actions/how-tos/deploy/configure-and-manage-deployments/manage-environments).
