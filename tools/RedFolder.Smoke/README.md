# Health and production smoke checks

The deployment pipeline also uses this contract for staging and recovery. See [staging and rollback](../../.github/STAGING_AND_ROLLBACK.md) for approval, warm-up, recovery enablement and required setup. References below to the absence of rollback describe the original #32 integration; #33 adds opt-in recovery after rehearsal.

Run from the repository root with the .NET 8 SDK (or a newer SDK that can target .NET 8) and .NET 8 runtime:

```sh
dotnet run --project tools/RedFolder.Smoke --configuration Release -- https://www.red-folder.com FULL_40_CHARACTER_COMMIT_SHA 15
```

Supply the canonical base URL, without a path, query, credentials or fragment, and the exact full commit SHA that was deployed. The optional per-request timeout defaults to 15 seconds and accepts 1–120 seconds. Redirects are failures, including HTTP-to-HTTPS and alternate hostname redirects: use the final HTTPS hostname in production. Requests are sequential, with no retries, and the default ten-request HTTP budget is approximately 150 seconds. DNS resolution can exceed very short timeouts on some platforms.

Exit codes: **0** all checks passed; **1** deployment check failed; **2** invalid arguments. Only fixed route names, statuses and diagnostic categories are printed. Response bodies, exception details and the supplied host/commit are never printed by the checks. No credentials are required and only GET requests are sent.

## Policy and coverage

- `/health`: 200 with `{"status":"Healthy"}` once the host has started; 503 with `{"status":"Degraded"}` before startup completes or while stopping, if the host is still accepting requests. Responses are not cacheable. This proves the application can serve the readiness endpoint, not dependency health. If the process cannot start or has stopped accepting requests, connection failure or timeout is the readiness failure signal.
- `/`, `/Blog`, `/Podcasts`, `/Projects`: exactly 200. Blog additionally requires its heading and blog tiles container. Redirects to error pages fail.
- `/api/version`: exactly 200 and `commitSha` matching the supplied full SHA. A missing local `version.json` deliberately fails this check; create the normal build metadata file in the application's content root for local end-to-end smoke runs.
- `/Activity` and all its descendants: deliberately **410 Gone**, without using the retired Activity service. Smoke checks sample the root and former Weekly, Books and Skills paths. Unknown routes outside Activity retain their existing routing behavior.

Blog content and podcast feed dependencies are exercised only as part of rendering their public pages, bounded by the smoke client's request timeout. They do not affect core `/health`. These checks do not establish dependency freshness, bypass application caches, verify every article/episode, or prove that a cancelled request stops server-side dependency work. Blog's markers verify the page structure, not the presence or freshness of individual articles. Contact, SendGrid, reCAPTCHA and mutating operations are intentionally excluded. No configuration, keys, connection strings, host internals or exception details are exposed by `/health`.

## Local and GitHub Actions use

Start the application with `dotnet run --project src/Red-Folder.com` and supply its listening base URL and local version file's SHA to the command above. Existing blog/podcast configuration must be available for those public pages to pass. Development HTTPS certificates must be trusted; the command does not disable certificate validation.

The production workflow runs the compiled smoke executable after a successful Azure deployment against `https://www.red-folder.com`, using `GITHUB_SHA` as the expected artifact commit. The build uploads the smoke executable separately from the website, and the deployment job downloads it outside the website package. Superseded deployments skip verification. Each request has a 15-second timeout, with an additional five-minute workflow step timeout.

A failed check fails the deployment job and records a verification failure in the workflow summary. The new release may already be live: failure does not automatically roll back the deployment. Inspect the failed route diagnostics and follow the [production recovery runbook](../../.github/PRODUCTION_DEPLOYMENT.md#recovery). Staging, promotion gates and tested rollback remain for #33; production smoke verification is included in #32.

Regression tests are included in the solution's normal `dotnet test --configuration Release` run, with no network calls. They cover readiness lifecycle responses, retired routes, and smoke success/failure for unexpected statuses, redirects, timeout, transport failure, malformed/missing metadata, incorrect commit, missing Blog markers and degraded readiness.

