# Branch Protection Rules Configuration

This document describes how to configure branch protection rules to ensure all tests pass before merging pull requests.

## Overview

The repository has three GitHub Actions workflows:

1. **PR Validation** (`pr-validation.yml`) - Runs on pull requests to master/main branches
   - Builds and tests the code
   - Validates code quality before merge
   - Must pass before PR can be merged

2. **Build and Test** (`build-and-test.yml`) - Reusable workflow
   - Contains common build, test, and artifact generation steps
   - Called by both PR validation and deployment workflows
   - Reduces duplication and ensures consistency

3. **Build and deploy ASP.Net Core app to an Azure Web App** (`azure-deploy.yml`) - Runs only on master branch
   - Builds and tests with code coverage
   - Generates deployment artifact
   - Deploys to Azure Web App (RFC-Website)

## Workflow Triggers

- **PR Validation**: Triggered on pull requests to `master` or `main` branches
- **Azure Deploy**: Triggered only on pushes to `master` branch
- **Both workflows** support manual triggering via `workflow_dispatch`

## Setting Up Branch Protection Rules

To require tests to pass before merging to the `master` branch, follow these steps:

### Steps to Configure

1. **Navigate to Repository Settings**
   - Go to your repository on GitHub
   - Click on **Settings**
   - Click on **Branches** in the left sidebar

2. **Add Branch Protection Rule**
   - Click **Add rule** (or **Add branch protection rule**)
   - In the "Branch name pattern" field, enter: `master`

3. **Configure Protection Settings**
   
   Enable the following options:

   - ✅ **Require a pull request before merging**
     - This ensures all changes go through a PR process
     - Optional: Enable "Require approvals" if you want code reviews
   
   - ✅ **Require status checks to pass before merging**
     - Check this box to enable status checks
     - In the search box that appears, search for and select:
       - `Validate PR / Build and Test` (from pr-validation.yml)
     - ✅ Check "Require branches to be up to date before merging"
   
   - ✅ **Do not allow bypassing the above settings** (recommended)
     - This ensures even administrators must follow the rules

4. **Save Changes**
   - Click **Create** or **Save changes** at the bottom

## What This Accomplishes

With these settings enabled:

- ✅ All code changes must go through a pull request
- ✅ All tests must pass before the PR can be merged
- ✅ The branch must be up-to-date with master before merging
- ✅ Failed tests will block the merge, preventing broken code from reaching master
- ✅ Deployment workflow only runs on master, never on feature branches
- ✅ No duplicate builds - PR validation runs on PRs, deployment runs on master

## Status Checks

The following status check will be required:

**Validate PR / Build and Test** - From the `pr-validation.yml` workflow
- Builds the solution
- Runs all unit and integration tests
- Validates the code compiles successfully
- Must pass before PR can be merged to master

## Deployment Process

After a PR is merged to master:

1. The `azure-deploy.yml` workflow automatically triggers
2. Code is built and tested with coverage
3. Code coverage is uploaded to Codecov
4. Deployment artifact is created with version information
5. Application is deployed to Azure Web App (RFC-Website)

## Testing the Configuration

To verify the branch protection is working:

1. Create a new branch
2. Make a change that breaks a test
3. Create a pull request
4. Observe that:
   - Tests run automatically via PR Validation workflow
   - The merge button is disabled until tests pass
   - You cannot merge until the test is fixed
   - Azure deployment workflow does NOT run on the PR branch

## Additional Recommendations

Consider enabling these additional protections:

- **Require linear history** - Prevents merge commits, keeping history clean
- **Include administrators** - Applies rules to repository administrators too
- **Restrict who can push to matching branches** - Limits direct pushes to master

## Workflow Architecture

```
Pull Request → pr-validation.yml → build-and-test.yml (run-coverage: false)
                                    ├── Build
                                    ├── Test
                                    └── Publish Test Results

Master Push  → azure-deploy.yml   → build-and-test.yml (run-coverage: true)
                                    ├── Build
                                    ├── Test
                                    ├── Coverage Report
                                    ├── Create Artifact
                                    └── Deploy to Azure
```

## Notes

- The reusable `build-and-test.yml` workflow eliminates duplication
- PR validation runs faster (no coverage generation)
- Master builds include full coverage reporting
- Deployment is completely isolated to master branch only
- Version tracking is included in deployed artifacts
- Test results are published as comments on PRs for easy visibility

## Troubleshooting

If status checks don't appear:

1. Make sure the workflows have run at least once on a PR
2. Refresh the branch protection settings page
3. The status check names must match exactly as they appear in GitHub Actions
4. Verify workflows are enabled in the repository settings
5. Check that the reusable workflow file (`build-and-test.yml`) exists

For more information, see:
- [GitHub Branch Protection Documentation](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-protected-branches/about-protected-branches)
- [GitHub Required Status Checks](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-protected-branches/about-protected-branches#require-status-checks-before-merging)
- [GitHub Reusable Workflows](https://docs.github.com/en/actions/using-workflows/reusing-workflows)
