# GitHub Copilot Instructions for red-folder.com

## Project Overview

This is an ASP.NET Core 8.0 website project for red-folder.com (www.red-folder.com). The solution contains:

- **Red-Folder.com**: Main ASP.NET Core MVC web application
- **Red-Folder.Website.Data**: Data access layer
- **Red-Folder.Podcast**: Podcast-related functionality
- **RedFolder.Blog**: Blog functionality
- **RedFolder.Blog.Unit.Tests**: Unit tests for blog functionality
- **RedFolder.WebSite.Integration.Tests**: Integration tests for the website

## Technology Stack

- **Framework**: .NET 8.0 (ASP.NET Core MVC)
- **Database**: Entity Framework Core with SQL Server
- **Testing**: xUnit
- **CI/CD**: GitHub Actions deploying to Azure Web App
- **Cloud**: Azure (Application Insights, Azure Web Apps)

## Build and Test Commands

### Restore Dependencies
```bash
dotnet restore
```

### Build
```bash
dotnet build --configuration Release
```

### Run Tests
```bash
dotnet test --configuration Release
```

### Run Specific Test Project
```bash
dotnet test tests/RedFolder.Blog.Unit.Tests --configuration Release
dotnet test tests/RedFolder.WebSite.Integration.Tests --configuration Release
```

### Code Coverage
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Run Application Locally
```bash
dotnet run --project src/Red-Folder.com/Red-Folder.com.csproj
```

## Code Style and Conventions

- Follow standard C# naming conventions (PascalCase for public members, camelCase for private fields with _ prefix)
- Use async/await for asynchronous operations
- Prefer dependency injection over static classes
- Write XML documentation comments for public APIs
- Keep controllers thin - business logic should be in services

## Testing Guidelines

- Write unit tests for business logic and services
- Write integration tests for API endpoints and full workflows
- Test files should be in the `tests/` directory
- Follow AAA pattern (Arrange, Act, Assert) in tests
- Use descriptive test method names that explain what is being tested

## Project Structure

- `src/` - Source code for all projects
- `tests/` - Test projects
- `wwwroot/` - Static web assets (CSS, JavaScript, images)
- `Views/` - Razor views
- `Controllers/` - MVC controllers
- `Models/` - Data models
- `ViewModels/` - View models
- `Services/` - Business logic services
- `Data/` - JSON data files for certifications, employment, services

## Dependencies

- Avoid adding new NuGet packages unless absolutely necessary
- Always check for security vulnerabilities in dependencies before adding
- Update packages carefully to avoid breaking changes
- Document why new dependencies are being added

## Security

- Never commit secrets or connection strings to source control
- Use Azure App Configuration or environment variables for sensitive data
- Sanitize user input to prevent XSS and injection attacks
- Follow OWASP security best practices

## Deployment

- All branches trigger CI build and test via GitHub Actions
- Only `master` branch deploys to Azure Web App (RFC-Website)
- Deployment uses Azure Web Apps Deploy action with publish profile
- Application Insights is configured for monitoring

## Additional Notes

- The project uses Application Insights for telemetry and monitoring
- SendGrid is used for email functionality
- The solution includes podcast and blog management features
- JSON files in `Data/` directories contain static content that can be updated
