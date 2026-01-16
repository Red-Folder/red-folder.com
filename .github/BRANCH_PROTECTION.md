# Branch Protection Rules Configuration

This document describes how to configure branch protection rules to ensure all tests pass before merging pull requests.

## Overview

The repository has two GitHub Actions workflows:
1. **Build and deploy ASP.Net Core app to an Azure Web App** (`azure-deploy.yml`) - Runs on all branches
2. **PR Validation** (`pr-validation.yml`) - Runs specifically on pull requests

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
       - `build` (from azure-deploy.yml)
       - `validate / Build and Test` (from pr-validation.yml)
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

## Status Checks

The following status checks will be required:

1. **build** - From the main `azure-deploy.yml` workflow
   - Builds the solution
   - Runs all unit and integration tests
   - Validates the code compiles successfully

2. **validate / Build and Test** - From the `pr-validation.yml` workflow
   - Dedicated PR validation workflow
   - Ensures tests pass before merge

## Testing the Configuration

To verify the branch protection is working:

1. Create a new branch
2. Make a change that breaks a test
3. Create a pull request
4. Observe that:
   - Tests run automatically
   - The merge button is disabled until tests pass
   - You cannot merge until the test is fixed

## Additional Recommendations

Consider enabling these additional protections:

- **Require linear history** - Prevents merge commits, keeping history clean
- **Include administrators** - Applies rules to repository administrators too
- **Restrict who can push to matching branches** - Limits direct pushes to master

## Notes

- The `azure-deploy.yml` workflow runs on all branches and deploys only from `master`
- The `pr-validation.yml` workflow is specifically designed for PR validation
- Test failures will be visible in the PR checks section
- Test results are published as comments on the PR for easy visibility

## Troubleshooting

If status checks don't appear:

1. Make sure the workflows have run at least once on a PR
2. Refresh the branch protection settings page
3. The status check names must match exactly as they appear in GitHub Actions
4. Verify workflows are enabled in the repository settings

For more information, see:
- [GitHub Branch Protection Documentation](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-protected-branches/about-protected-branches)
- [GitHub Required Status Checks](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-protected-branches/about-protected-branches#require-status-checks-before-merging)
