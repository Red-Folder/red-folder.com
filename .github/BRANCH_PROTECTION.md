# Master CI Merge Gate

## Verified configuration

Inspected on 2026-09-06 for [issue #30](https://github.com/Red-Folder/red-folder.com/issues/30), using an account with repository administration access. Protection is implemented by the active repository ruleset [PR merge only for master](https://github.com/Red-Folder/red-folder.com/rules/3696335) (ID `3696335`). Its target is `~DEFAULT_BRANCH`, currently `master`; changing the default branch changes this rule's target.

| Control | Effective setting |
| --- | --- |
| Pull request required | Yes; zero approving reviews required |
| Required status check | `Validate PR / Build and Test` |
| Check source | GitHub Actions, integration ID `15368` |
| Branch must be up to date | Yes (`strict_required_status_checks_policy: true`) |
| Bypass actors | None; current administrator reports `current_user_can_bypass: never` |
| Direct pushes | Blocked by the pull request requirement, with no bypass actors |
| Force pushes | Blocked (`non_fast_forward`) |
| Linear history | Required; use squash or rebase merging |

The exact required context was confirmed from actual GitHub check runs. `Test Results` is a separate reporting check, not the required merge gate. No protection settings were changed or weakened during verification.

The repository administrator confirmed that no classic branch protections are configured. The CLI token returned HTTP 403 when reading the classic protection endpoint, so the absence of classic rules is administrator-confirmed rather than independently API-verified. The active ruleset, effective branch rules, and empty bypass list were read successfully through the API.

## Enforcement evidence

Temporary [PR #50](https://github.com/Red-Folder/red-folder.com/pull/50) targets `master` from `codex/issue-30-merge-gate-verification`, based on master commit `7ebfc9e941c114c3462007c38618f6af295eb3e4`.

The PR was opened as a draft, then marked ready for review with the repository owner's approval: drafts are inherently unmergeable and cannot demonstrate that checks control merge eligibility. No merge was attempted.

| Phase | Commit | Required check and merge state |
| --- | --- | --- |
| Deliberately failing test | `ca0c98557e0079676fff52ff8a649c177bdb97bd` | [Run 34038697927](https://github.com/Red-Folder/red-folder.com/actions/runs/34038697927): build succeeded, Run Tests failed, required check failed; non-draft PR reported `BLOCKED` |
| Repaired test | `8e355481c5b09948356e30c852633635c3bf2522` | [Run 34038817910](https://github.com/Red-Folder/red-folder.com/actions/runs/34038817910): required check succeeded; non-draft PR reported `CLEAN` and `MERGEABLE` |

The failed run identified `MergeGateVerificationTests.MergeGate_DeliberateFailure_BlocksMerge` as the deliberate failure: one blog test failed, one passed, and all 18 integration tests passed. The repaired commit replaces that assertion with a passing assertion, preserving a PR diff for the mergeability check. This temporary test is not part of the documentation change.

After both runs, the Azure workflow runs endpoint filtered to the temporary branch returned `total_count: 0`. PR #50 was closed without merging and its remote branch deleted. Master remained at the original commit above. Direct and force pushes were verified from the enforced rules and absence of bypass actors; no destructive push was attempted.

Local validation of the final documentation branch: dependency restore and Release build succeeded; all 19 existing tests passed. Existing dependency vulnerability and legacy target-framework warnings remain outside this issue's scope.

## Workflow and deployment behavior

- `pr-validation.yml` runs for pull requests targeting `master` or `main`, and supports manual dispatch. It calls `build-and-test.yml` with coverage and deployment artifact creation disabled.
- The reusable workflow restores, builds, and tests the solution. Test failure fails the required `Validate PR / Build and Test` job. Test reporting runs even after a failure.
- `azure-deploy.yml` triggers automatically only on pushes to `master`. It also supports manual dispatch, which can start a build on another branch.
- The Azure deployment job requires a successful build and `github.ref == 'refs/heads/master'`. A manually dispatched feature-branch build cannot deploy through that job.
- The issue #31 workflow change serializes production deployment jobs and skips superseded commits. The approved Production environment must restrict deployments to the `master` branch. See [production deployment](PRODUCTION_DEPLOYMENT.md) for migration prerequisites, live verification status, and recovery; environment setup is not yet verified.

## Repeatable verification

1. Read the effective branch rules and ruleset details with an administrator account. Confirm the exact check context and GitHub Actions integration against a recent PR check run. Inspect classic branch protection separately; record access limitations rather than interpreting HTTP 403 as absence.
2. Create an isolated branch from current `master` and add a clearly identified, deliberately failing unit test. Open a temporary draft PR.
3. Mark the PR ready for review before assessing merge eligibility. Wait for the required check to finish and record its run URL, head SHA, test failure, and `mergeStateStatus: BLOCKED`. Do not attempt a merge or weaken protection.
4. Repair the assertion and push. Wait for the required check to succeed and for GitHub to recompute merge eligibility. Record the new SHA, run URL, and merge state. Resolve any unrelated blockers before claiming the gate cleared.
5. Query Azure workflow runs filtered to the temporary branch and verify that none ran. Do not manually dispatch deployment as part of this test.
6. Close the PR without merging and remove the temporary remote branch. Keep the PR and run links as evidence; exclude the temporary test from the final change.

Useful read-only commands (substitute the current PR number, ruleset ID, and branch):

```powershell
 gh api repos/Red-Folder/red-folder.com/rules/branches/master
 gh api repos/Red-Folder/red-folder.com/rulesets/3696335
 gh api repos/Red-Folder/red-folder.com/branches/master/protection
 gh pr checks 50 --repo Red-Folder/red-folder.com --required
 gh pr view 50 --repo Red-Folder/red-folder.com --json isDraft,headRefOid,mergeStateStatus,statusCheckRollup
 gh api 'repos/Red-Folder/red-folder.com/actions/workflows/azure-deploy.yml/runs?branch=codex%2Fissue-30-merge-gate-verification'
```

Do not store tokens, secrets, or unfiltered settings exports in repository evidence.
