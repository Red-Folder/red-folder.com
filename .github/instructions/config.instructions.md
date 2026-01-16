---
applyTo: "**/*.csproj,**/*.json,**/*.yml,**/*.yaml"
---

# Configuration Files Instructions

## Project Files (.csproj)

- Use SDK-style project format (not legacy format)
- Specify `TargetFramework` explicitly (currently net8.0)
- Keep package references up-to-date and secure
- Use `<PackageReference>` instead of packages.config
- Group related items with ItemGroup elements
- Include version numbers for all package references
- Document reasons for specific version constraints

## JSON Configuration (appsettings.json, package.json, etc.)

- Follow consistent indentation (2 or 4 spaces)
- Use camelCase for property names in JavaScript/TypeScript JSON
- Use PascalCase for .NET configuration JSON
- Validate JSON syntax before committing
- Don't include secrets or connection strings - use environment variables
- Use meaningful configuration section names
- Add comments via separate .md files if needed (JSON doesn't support comments)

## YAML Configuration (.yml, .yaml)

- Use consistent 2-space indentation
- Avoid tabs - use spaces only
- Quote strings that might be interpreted as numbers or booleans
- Use `|` or `>` for multi-line strings when appropriate
- Validate YAML syntax before committing
- Add comments to explain complex configurations

## GitHub Actions Workflows

- Keep workflow files in `.github/workflows/`
- Use meaningful workflow and job names
- Pin action versions to specific commits or tags for security
- Use secrets for sensitive data
- Add descriptive comments for complex steps
- Test workflow changes in a branch before merging
- Use matrix strategies for testing multiple configurations
- Cache dependencies to speed up builds

## Environment-Specific Configuration

- Use `appsettings.json` for default values
- Use `appsettings.{Environment}.json` for environment overrides
- Use environment variables for secrets and deployment-specific values
- Document required environment variables
- Provide example configuration files (like `appsettings.example.json`)

## Data Files (JSON)

- Validate against schema if one exists
- Use consistent formatting and indentation
- Keep data files small and focused
- Consider using database for large or frequently changing data
- Document the structure and purpose of data files

## Configuration Best Practices

- Never commit secrets, API keys, or connection strings
- Use Azure Key Vault or similar for production secrets
- Keep development and production configurations separate
- Document all configuration options
- Use strong typing for configuration (bind to classes in .NET)
- Validate configuration at application startup
- Provide sensible defaults where possible
