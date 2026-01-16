# red-folder.com

Source code for www.red-folder.com - An ASP.NET Core 8.0 MVC website.

## Overview

This repository contains the source code for the Red Folder website, a personal portfolio and blog site. The project is built with ASP.NET Core 8.0 and deployed to Azure.

## Tech Stack

- **Framework**: ASP.NET Core 8.0 (MVC)
- **Database**: Entity Framework Core with SQL Server
- **Testing**: xUnit
- **CI/CD**: GitHub Actions → Azure Web App
- **Cloud Services**: Azure (Web Apps, Application Insights)

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022, VS Code, or JetBrains Rider (optional)
- SQL Server or SQL Server Express (for local development)

### Building and Running

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build --configuration Release

# Run the web application
dotnet run --project src/Red-Folder.com/Red-Folder.com.csproj

# Run with hot reload during development
dotnet watch --project src/Red-Folder.com/Red-Folder.com.csproj
```

### Running Tests

```bash
# Run all tests
dotnet test --configuration Release

# Run specific test project
dotnet test tests/RedFolder.Blog.Unit.Tests --configuration Release

# Generate code coverage
dotnet test --collect:"XPlat Code Coverage"
```

## Project Structure

```
src/
├── Red-Folder.com/          # Main web application
├── Red-Folder.Website.Data/ # Data access layer
├── Red-Folder.Podcast/      # Podcast functionality
└── RedFolder.Blog/          # Blog functionality

tests/
├── RedFolder.Blog.Unit.Tests/           # Unit tests
└── RedFolder.WebSite.Integration.Tests/ # Integration tests
```

## AI Coding Agent Configuration

This repository is configured for use with GitHub Copilot and other AI coding agents. The configuration helps AI agents understand the project structure, coding conventions, and development workflows.

### Configuration Files

- **`.github/copilot-instructions.md`** - Repository-wide instructions for GitHub Copilot
- **`AGENTS.md`** - General development guidelines for AI agents
- **`.github/agents/`** - Custom specialized agents for specific tasks:
  - `test-writer.agent.md` - Expert in writing xUnit tests
  - `razor-specialist.agent.md` - Specialist in Razor views and front-end
  - `api-developer.agent.md` - Expert in API development and services
  - `security-auditor.agent.md` - Security vulnerability specialist
  - `database-expert.agent.md` - Entity Framework Core and database expert
  - `devops-specialist.agent.md` - Expert in GitHub Actions, CI/CD, and Azure deployments
- **`.github/instructions/`** - Path-specific instructions for different file types:
  - `csharp.instructions.md` - C# coding standards
  - `tests.instructions.md` - Testing guidelines
  - `config.instructions.md` - Configuration file standards
  - `razor.instructions.md` - Razor view conventions
  - `web-assets.instructions.md` - JavaScript/CSS/HTML guidelines
  - `workflows.instructions.md` - GitHub Actions workflow guidelines
  - `coding-agent.instructions.md` - Coding agent specific workflow

### Using AI Coding Agents

GitHub Copilot and compatible AI agents will automatically use these instructions when:
- Generating code completions
- Answering questions about the codebase
- Making changes via coding agent mode
- Reviewing pull requests

**Custom Agents** can be invoked for specialized tasks:
- Use **@test-writer** when working on tests
- Use **@razor-specialist** for views and front-end work
- Use **@api-developer** for controllers and services
- Use **@security-auditor** for security reviews
- Use **@database-expert** for database and EF Core work
- Use **@devops-specialist** for CI/CD pipelines, GitHub Actions workflows, and Azure deployments

For best results:
1. Keep the instruction files up-to-date with project conventions
2. Refer to `AGENTS.md` for development workflow guidelines
3. Use custom agents for specialized tasks
4. Review AI-generated code to ensure it follows project standards

### Supported AI Tools

This configuration works with:
- GitHub Copilot (in VS Code, Visual Studio, JetBrains IDEs)
- GitHub Copilot CLI
- Other AI coding agents that support AGENTS.md or similar formats

### Model Context Protocol (MCP)

For enhanced DevOps capabilities, see `.github/MCP_RECOMMENDATIONS.md` for:
- Recommended MCP servers for GitHub Actions and Azure integration
- Setup instructions and security considerations
- Implementation roadmap

## CI/CD Pipeline

The project uses GitHub Actions for continuous integration and deployment:

- **All branches**: Build, test, and generate code coverage
- **Master branch**: Deploy to Azure Web App (RFC-Website)
- **Coverage**: Results uploaded to Codecov

See `.github/workflows/azure-deploy.yml` for the complete workflow.

### Version Tracking

The deployed application includes version tracking to verify which code is running in production:

**Browser Access:**
- Visit `https://red-folder.com/home/version` to view version information in a user-friendly format

**API Access:**
- `GET https://red-folder.com/api/version` returns JSON with build metadata

**Version Information Includes:**
- Git commit SHA (with link to GitHub)
- Build timestamp
- Branch name
- Build number

During the deployment process, the GitHub Actions workflow generates a `version.json` file containing:
```json
{
  "commitSha": "abc123...",
  "shortCommitSha": "abc123d",
  "branchName": "master",
  "buildTime": "2026-01-16T14:30:00Z",
  "buildNumber": "42",
  "commitUrl": "https://github.com/Red-Folder/red-folder.com/commit/abc123..."
}
```

This allows easy verification of deployments and tracing production issues back to source code.

## Contributing

1. Create a feature branch from `master`
2. Make your changes following the project conventions
3. Ensure all tests pass: `dotnet test --configuration Release`
4. Ensure the build succeeds: `dotnet build --configuration Release`
5. Create a pull request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

Copyright (c) 2016 Mark Taylor

## Contact

For more information, visit [www.red-folder.com](https://www.red-folder.com)
