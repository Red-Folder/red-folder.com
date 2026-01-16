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
- **`.github/instructions/`** - Path-specific instructions for different file types:
  - `csharp.instructions.md` - C# coding standards
  - `tests.instructions.md` - Testing guidelines
  - `config.instructions.md` - Configuration file standards
  - `razor.instructions.md` - Razor view conventions
  - `web-assets.instructions.md` - JavaScript/CSS/HTML guidelines
  - `coding-agent.instructions.md` - Coding agent specific workflow

### Using AI Coding Agents

GitHub Copilot and compatible AI agents will automatically use these instructions when:
- Generating code completions
- Answering questions about the codebase
- Making changes via coding agent mode
- Reviewing pull requests

For best results:
1. Keep the instruction files up-to-date with project conventions
2. Refer to `AGENTS.md` for development workflow guidelines
3. Review AI-generated code to ensure it follows project standards

### Supported AI Tools

This configuration works with:
- GitHub Copilot (in VS Code, Visual Studio, JetBrains IDEs)
- GitHub Copilot CLI
- Other AI coding agents that support AGENTS.md or similar formats

## CI/CD Pipeline

The project uses GitHub Actions for continuous integration and deployment:

- **All branches**: Build, test, and generate code coverage
- **Master branch**: Deploy to Azure Web App (RFC-Website)
- **Coverage**: Results uploaded to Codecov

See `.github/workflows/azure-deploy.yml` for the complete workflow.

## Contributing

1. Create a feature branch from `master`
2. Make your changes following the project conventions
3. Ensure all tests pass: `dotnet test --configuration Release`
4. Ensure the build succeeds: `dotnet build --configuration Release`
5. Create a pull request

## License

[Add license information here]

## Contact

For more information, visit [www.red-folder.com](https://www.red-folder.com)
