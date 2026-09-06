---
applyTo: ".github/workflows/**/*.yml,.github/workflows/**/*.yaml"
---

# GitHub Actions Workflow Instructions

These instructions apply specifically to GitHub Actions workflow files in `.github/workflows/`.

## Workflow Structure

### File Organization
- Keep workflow files in `.github/workflows/` directory only
- Use descriptive file names (e.g., `azure-deploy.yml`, `pr-validation.yml`)
- One workflow per file
- Use `.yml` extension (preferred) or `.yaml`

### Naming Conventions
- Use meaningful workflow names that describe purpose
- Use descriptive job names that indicate what they do
- Use clear step names that explain the action being performed
- Follow the pattern: `name: Build and deploy ASP.Net Core app to an Azure Web App`

## YAML Syntax

### Indentation and Formatting
- Use 2-space indentation consistently
- No tabs - spaces only
- Align related items at same indentation level
- Add blank lines between major sections for readability
- Use quotes for strings containing special characters or starting with special chars

### Comments
- Add comments above complex steps to explain purpose
- Document non-obvious configuration choices
- Explain conditional logic
- Note any workarounds or special requirements
- Keep comments concise and relevant

## Workflow Events

### Trigger Configuration
```yaml
on:
  push:
    branches: [ "master", "develop" ]  # Explicit branches
  pull_request:
    branches: [ "master" ]
  workflow_dispatch:  # Enable manual triggers
```

### Common Triggers for This Project
- `push`: For build and deploy workflows
- `pull_request`: For PR validation
- `workflow_dispatch`: For manual workflow runs
- Avoid unnecessary triggers to save Actions minutes

## Jobs Configuration

### Job Structure
```yaml
jobs:
  job-name:
    runs-on: ubuntu-latest  # Use latest stable runner
    permissions:  # Explicit permissions (least privilege)
      contents: read
      checks: write
    steps:
      - name: Descriptive step name
        uses: action@v4  # Pin to major version or commit
```

### Job Dependencies
- Use `needs: [job-name]` to create job dependencies
- Build artifacts in one job, deploy in another
- Separate concerns (build, test, deploy)
- Allow parallel execution where possible

### Permissions
- Always specify permissions explicitly
- Use least privilege principle
- Common permissions:
  - `contents: read` - Read repository contents
  - `contents: write` - Push commits/tags
  - `checks: write` - Create check runs
  - `pull-requests: write` - Comment on PRs
  - `contents: none` - No content access

## Actions and Steps

### Action Versions
- **Always pin action versions** for security and stability
- Use major version tags: `@v4` (gets latest v4.x.x)
- For critical actions, pin to commit SHA
- Keep actions up-to-date with security patches
- Document why specific versions are used

### Common Actions for This Project
```yaml
- uses: actions/checkout@v4
- uses: actions/setup-dotnet@v4
  with:
    dotnet-version: ${{ env.DOTNET_VERSION }}
- uses: actions/cache@v4
- uses: actions/upload-artifact@v4
- uses: actions/download-artifact@v4
- uses: azure/webapps-deploy@v2
- uses: EnricoMi/publish-unit-test-result-action@v2
- uses: codecov/codecov-action@v3
```

### Step Naming
- Prefix with action being performed: "Build", "Test", "Deploy"
- Be specific: "Run Tests" not just "Tests"
- Match .NET terminology: "Restore dependencies", "Build", "Publish"

## Environment Variables

### Definition
```yaml
env:
  AZURE_WEBAPP_NAME: RFC-Website
  DOTNET_VERSION: '8.0.x'  # Quote to prevent YAML parsing as float
  AZURE_WEBAPP_PACKAGE_PATH: '.'
```

### Usage
- Define at workflow level for global scope
- Define at job level for job-specific variables
- Use `${{ env.VARIABLE_NAME }}` to reference
- Quote version numbers to prevent misinterpretation
- Use SCREAMING_SNAKE_CASE for env var names

## Secrets Management

### Using Secrets
```yaml
- uses: azure/webapps-deploy@v2
  with:
    publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
```

### Best Practices
- Never hardcode sensitive values
- Use GitHub Secrets for all credentials
- Reference with `${{ secrets.SECRET_NAME }}`
- Don't echo or log secrets
- Rotate secrets regularly
- Use descriptive secret names

### Current Secrets
- `AZURE_WEBAPP_PUBLISH_PROFILE` - Azure deployment credentials
- `CODECOV_TOKEN` - Codecov upload token

## Caching Strategy

### Dependency Caching
```yaml
- name: Set up dependency caching for faster builds
  uses: actions/cache@v4
  with:
    path: ~/.nuget/packages
    key: ${{ runner.os }}-nuget-${{ hashFiles('**/packages.lock.json') }}
    restore-keys: |
      ${{ runner.os }}-nuget-
```

### Cache Best Practices
- Cache package directories to speed up builds
- Use content-based cache keys (e.g., hash of lock files)
- Provide fallback restore keys
- Balance cache size vs. build time savings
- Document what is being cached and why

## .NET Specific Patterns

### Build Steps Order
1. Checkout code
2. Setup .NET SDK
3. Restore dependencies (with caching)
4. Build (with `--no-restore`)
5. Test (with `--no-build`)
6. Publish (if deploying)

### Command Flags
- Use `--no-restore` after restore to skip redundant restore
- Use `--no-build` for test step after build
- Use `--configuration Release` for production builds
- Use `--logger trx` for test result logging
- Use `--verbosity normal` for balanced output

### Test Reporting
```yaml
- name: Run Tests
  run: dotnet test --no-build --configuration Release --logger trx --results-directory "test-results"

- name: Publish Test Results
  uses: EnricoMi/publish-unit-test-result-action@v2
  if: always()  # Run even if tests fail
  with:
    files: test-results/**/*.trx
```

## Conditional Execution

### If Conditions
```yaml
- name: Upload artifact for deployment job
  if: github.ref == 'refs/heads/master'
  uses: actions/upload-artifact@v4

- name: Publish Test Results
  if: always()  # Run even on failure
```

### Common Conditions
- `if: github.ref == 'refs/heads/master'` - Only on master branch
- `if: always()` - Run regardless of previous step status
- `if: success()` - Run only if previous steps succeeded (default)
- `if: failure()` - Run only if previous step failed
- `if: ${{ always() }}` - Alternative syntax with explicit expression

## Artifacts

### Upload Artifacts
```yaml
- name: Upload artifact for deployment job
  uses: actions/upload-artifact@v4
  with:
    name: .net-app
    path: ${{env.DOTNET_ROOT}}/myapp
```

### Download Artifacts
```yaml
- name: Download artifact from build job
  uses: actions/download-artifact@v4
  with:
    name: .net-app
```

### Artifact Best Practices
- Use descriptive artifact names
- Upload only what's needed for downstream jobs
- Clean up artifacts automatically (default 90 days)
- Conditional upload based on branch or success
- Document artifact contents and purpose

## Deployment Patterns

### Separate Build and Deploy Jobs
```yaml
jobs:
  build:
    runs-on: ubuntu-latest
    steps: [build steps]
    
  deploy:
    if: github.ref == 'refs/heads/master'
    needs: build
    runs-on: ubuntu-latest
    steps: [deploy steps]
```

### Environment Configuration
```yaml
environment:
  name: 'Production'
  url: ${{ steps.deploy-to-webapp.outputs.webapp-url }}
```

### Deployment Best Practices
- Deploy only from protected branches
- Require successful tests before deployment
- Use separate jobs for isolation
- Set environment URLs for easy access
- Monitor deployments

### Reusable Workflows
Reusable workflows allow you to eliminate duplication across multiple workflows by extracting common steps into a shared workflow.

```yaml
# Reusable workflow (build-and-test.yml)
name: Build and Test

on:
  workflow_call:
    inputs:
      run-coverage:
        required: false
        type: boolean
        default: false
    secrets:
      CODECOV_TOKEN:
        required: false

jobs:
  build-and-test:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout code
        uses: actions/checkout@v4
      # ... more steps
```

```yaml
# Calling the reusable workflow
jobs:
  build:
    uses: ./.github/workflows/build-and-test.yml
    with:
      run-coverage: true
    secrets:
      CODECOV_TOKEN: ${{ secrets.CODECOV_TOKEN }}
```

**Benefits:**
- Reduces duplication and maintenance burden
- Ensures consistency across workflows
- Makes testing and updates easier
- Allows parameterization with inputs and secrets

**This Project Uses:**
- `build-and-test.yml` - Reusable workflow for building and testing
- Called by both `azure-deploy.yml` (with coverage) and `pr-validation.yml` (without coverage)

## Error Handling

### Fail Fast vs. Continue on Error
```yaml
- name: Run optional step
  continue-on-error: true
  run: dotnet format --verify-no-changes

- name: Critical step
  # No continue-on-error - fail entire workflow
  run: dotnet test
```

### Exit Codes
- Non-zero exit code fails the step (by default)
- Use `continue-on-error: true` for optional steps
- Check outputs with conditional steps
- Handle errors explicitly when needed

## Performance Optimization

### Speed Up Workflows
1. **Cache dependencies** - Reduce download time
2. **Use `--no-restore` and `--no-build`** - Skip redundant operations
3. **Run jobs in parallel** - When no dependencies exist
4. **Optimize test execution** - Run fast tests first
5. **Minimize artifact size** - Only include necessary files

### Matrix Strategies
```yaml
strategy:
  matrix:
    dotnet-version: ['8.0.x', '9.0.x']
    os: [ubuntu-latest, windows-latest]
```
- Use for testing multiple configurations
- Not currently used in this project (single .NET version)
- Consider for future multi-version support

## Security Best Practices

### Secure Workflows
- Pin action versions to prevent supply chain attacks
- Use `permissions` to limit access
- Never log secrets
- Validate inputs
- Use trusted actions only
- Review action source code for critical workflows
- Keep GitHub-hosted runners up to date

### Dependency Security
- Regularly update action versions
- Review security advisories
- Use Dependabot for automated updates
- Pin to specific commits for critical actions

## Debugging Workflows

### Enable Debug Logging
1. Add repository secret: `ACTIONS_STEP_DEBUG` = `true`
2. Add repository secret: `ACTIONS_RUNNER_DEBUG` = `true`
3. Re-run workflow to see debug logs

### Common Issues
- **Syntax errors**: Validate YAML before committing
- **Permission errors**: Check `permissions` configuration
- **Secret not found**: Verify secret name and scope
- **Cache issues**: Clear cache by changing key
- **Action not found**: Verify action name and version

### Testing Workflow Changes
1. Create feature branch
2. Modify workflow file
3. Push to trigger workflow
4. Review workflow run logs
5. Iterate based on results
6. Merge after validation

## Version Tracking

### Generate Build Metadata
```yaml
- name: Generate version.json
  run: |
    cat > output/version.json << EOF
    {
      "commitSha": "${{ github.sha }}",
      "shortCommitSha": "${GITHUB_SHA:0:7}",
      "branchName": "${{ github.ref_name }}",
      "buildTime": "$(date -u +'%Y-%m-%dT%H:%M:%SZ')",
      "buildNumber": "${{ github.run_number }}",
      "commitUrl": "${{ github.server_url }}/${{ github.repository }}/commit/${{ github.sha }}"
    }
    EOF
```

### GitHub Context Variables
- `${{ github.sha }}` - Commit SHA
- `${{ github.ref }}` - Full ref (refs/heads/master)
- `${{ github.ref_name }}` - Branch name only (master)
- `${{ github.run_number }}` - Workflow run number
- `${{ github.repository }}` - Owner/repo name
- `${{ github.actor }}` - User who triggered workflow

## Documentation

### When to Update Workflows
- Adding new build steps
- Changing deployment targets
- Updating action versions
- Modifying security configuration
- Adding new environments

### What to Document
- Complex conditional logic
- Non-standard configurations
- Workarounds for known issues
- Performance optimizations
- Required secrets and their purpose

### Where to Document
- Comments in workflow file for step-specific details
- `README.md` for general CI/CD information
- `AGENTS.md` for developer workflow guidance
- Custom agent files for specialized knowledge

## Validation Checklist

Before committing workflow changes:
- [ ] YAML syntax is valid (use linter)
- [ ] Action versions are pinned
- [ ] Secrets are properly referenced (no hardcoded values)
- [ ] Permissions follow least privilege
- [ ] Comments explain complex logic
- [ ] Caching is configured correctly
- [ ] Tests run before deployment
- [ ] Deployment is conditional on appropriate branch/conditions
- [ ] Error handling is appropriate
- [ ] Performance optimizations are in place

## Resources

### Official Documentation
- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [Workflow Syntax](https://docs.github.com/en/actions/reference/workflow-syntax-for-github-actions)
- [GitHub-hosted Runners](https://docs.github.com/en/actions/using-github-hosted-runners)
- [Security Hardening](https://docs.github.com/en/actions/security-guides/security-hardening-for-github-actions)

### Marketplace
- [GitHub Actions Marketplace](https://github.com/marketplace?type=actions)
- [Azure Actions](https://github.com/Azure/actions)
- [.NET Actions](https://github.com/actions/setup-dotnet)

### Project Specific
- Reusable workflow: `.github/workflows/build-and-test.yml`
- Main workflow: `.github/workflows/azure-deploy.yml`
- PR validation: `.github/workflows/pr-validation.yml`
- DevOps agent: `.github/agents/devops-specialist.agent.md`
