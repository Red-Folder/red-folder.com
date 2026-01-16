# AGENTS.md - Development Guidelines for AI Coding Agents

This repository contains an ASP.NET Core 8.0 website project. When working on this project with AI coding agents, please follow these guidelines to ensure a smooth and effective development experience.

## 1. Development Workflow

### Always Use Development Mode During Iteration

* **Use `dotnet run` or `dotnet watch`** while iterating on the application for fast feedback with hot reload
* **Avoid `dotnet publish`** during interactive development sessions - this is only for production deployments
* For quick validation, use `dotnet build` rather than publish

### Testing Strategy

* **Run tests frequently** to catch issues early: `dotnet test --configuration Release`
* **Run specific test projects** when working on targeted areas:
  - `dotnet test tests/RedFolder.Blog.Unit.Tests` for blog-related changes
  - `dotnet test tests/RedFolder.WebSite.Integration.Tests` for integration testing
* **Check code coverage** when making significant changes: `dotnet test --collect:"XPlat Code Coverage"`
* Fix any test failures before considering work complete

## 2. Project Structure and Navigation

### Key Directories

```
src/
├── Red-Folder.com/          # Main web application (MVC)
│   ├── Controllers/         # MVC controllers
│   ├── Views/              # Razor views
│   ├── Models/             # Data models
│   ├── ViewModels/         # View models
│   ├── Services/           # Business logic
│   └── wwwroot/            # Static assets
├── Red-Folder.Website.Data/ # Data access layer
├── Red-Folder.Podcast/      # Podcast features
└── RedFolder.Blog/          # Blog features

tests/
├── RedFolder.Blog.Unit.Tests/           # Unit tests
└── RedFolder.WebSite.Integration.Tests/ # Integration tests
```

### Finding Your Way Around

* Use `dotnet sln list` to see all projects in the solution
* Check `.csproj` files to understand project dependencies
* Look in `Startup.cs` or `Program.cs` for service configuration and middleware setup

## 3. Dependency Management

### Adding or Updating Packages

1. **Always check for vulnerabilities** before adding dependencies
2. **Use specific version numbers** rather than wildcards
3. **Update project file** using `dotnet add package <PackageName>`
4. **Restore dependencies** with `dotnet restore`
5. **Rebuild and test** to ensure no breaking changes

### Current Known Issues

* The project has some dependency warnings (check `dotnet restore` output)
* Be cautious about updating System.Text.Json and Newtonsoft.Json due to known vulnerabilities being tracked

## 4. Build and Deployment

### Build Commands Reference

| Command | Purpose |
|---------|---------|
| `dotnet restore` | Restore NuGet packages |
| `dotnet build` | Build the solution |
| `dotnet build --configuration Release` | Build for release |
| `dotnet test` | Run all tests |
| `dotnet run --project src/Red-Folder.com` | Run the web application locally |
| `dotnet watch --project src/Red-Folder.com` | Run with hot reload |

### CI/CD Pipeline

* **GitHub Actions** handles CI/CD (see `.github/workflows/azure-deploy.yml`)
* **All branches** trigger build and test
* **Only `master` branch** deploys to Azure Web App
* Tests must pass before deployment
* Code coverage is uploaded to Codecov
* **For CI/CD changes**, use the **@devops-specialist** custom agent who has expertise in GitHub Actions workflows, Azure deployments, and pipeline optimization

## 5. Coding Standards

### C# Conventions

* Follow Microsoft C# coding conventions
* Use `async`/`await` for asynchronous operations
* Prefer dependency injection over static classes
* Use meaningful variable and method names
* Keep controllers thin - move business logic to services

### File Organization

* One class per file (with exceptions for small helper classes)
* Namespace should match folder structure
* Group related files together

### Testing Conventions

* Test files should mirror source file structure
* Use AAA pattern (Arrange, Act, Assert)
* Test method names should clearly describe what is being tested
* One logical assertion per test (when possible)

## 6. Common Pitfalls to Avoid

* **Don't run `dotnet publish` during development** - it's for deployment only
* **Don't commit secrets** - use environment variables or Azure Key Vault
* **Don't ignore test failures** - fix them or document why they're being skipped
* **Don't add dependencies without checking for vulnerabilities**
* **Don't make breaking changes without updating all callers**

## 7. Before Submitting Changes

### Pre-commit Checklist

- [ ] Run `dotnet restore` to ensure dependencies are synchronized
- [ ] Run `dotnet build --configuration Release` and fix any build errors
- [ ] Run `dotnet test --configuration Release` and ensure all tests pass
- [ ] Review your changes to ensure no secrets or sensitive data are included
- [ ] Verify that any new files are properly included in the project structure
- [ ] Update documentation if you've changed any public APIs or workflows

### For Pull Requests

- [ ] Provide clear description of what changed and why
- [ ] Reference any related issues
- [ ] Ensure CI/CD pipeline passes
- [ ] Add tests for new functionality
- [ ] Update relevant documentation

## 8. Getting Help

### Resources

* Project documentation: `README.md`
* CI/CD workflow: `.github/workflows/azure-deploy.yml`
* GitHub Copilot instructions: `.github/copilot-instructions.md`
* Path-specific instructions: `.github/instructions/`

### Troubleshooting

* If build fails, check `dotnet restore` output for dependency issues
* If tests fail, run them individually to isolate the problem
* If hot reload stops working, restart `dotnet watch`
* Check Application Insights in Azure for production issues

---

Following these guidelines will help ensure productive and successful AI-assisted development on this project. When in doubt, prefer to ask questions rather than make assumptions about the codebase.
