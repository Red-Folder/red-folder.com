# AI Coding Agent Configuration Summary

## Overview

This repository has been configured for optimal use with GitHub Copilot and other AI coding agents. The configuration provides context, conventions, and guidelines to help AI agents understand the project and assist with development tasks.

## Configuration Files Added

### 1. Repository-Wide Instructions
**File**: `.github/copilot-instructions.md`

This file provides GitHub Copilot with:
- Project overview and technology stack
- Build and test commands
- Code style and conventions
- Testing guidelines
- Project structure
- Security considerations
- Deployment information

**Used by**: GitHub Copilot in all IDEs, GitHub Copilot CLI, coding agents

### 2. General Agent Guidelines
**File**: `AGENTS.md`

Following the AGENTS.md standard (https://agents.md), this file provides:
- Development workflow best practices
- Project navigation guidance
- Dependency management guidelines
- Build and deployment commands
- Coding standards
- Common pitfalls to avoid
- Pre-commit checklist

**Used by**: AI agents that support the AGENTS.md format, GitHub Copilot coding agents

### 3. Custom Agents
**Directory**: `.github/agents/`

Custom agents are specialized versions of GitHub Copilot coding agent tailored to specific development tasks. This project includes:

#### `test-writer.agent.md`
- **Purpose**: Write and maintain xUnit tests for the ASP.NET Core application
- **Tools**: read, edit, create, search
- **Expertise**: Unit testing, integration testing, AAA pattern, test organization, Moq, code coverage

#### `razor-specialist.agent.md`
- **Purpose**: Create and maintain Razor views and front-end components
- **Tools**: read, edit, create, search
- **Expertise**: Razor syntax, Tag Helpers, MVC patterns, accessibility, security, HTML/CSS

#### `api-developer.agent.md`
- **Purpose**: Build ASP.NET Core controllers, services, and API endpoints
- **Tools**: read, edit, create, search
- **Expertise**: MVC controllers, service layer, dependency injection, error handling, async/await, RESTful APIs

#### `security-auditor.agent.md`
- **Purpose**: Identify and fix security vulnerabilities
- **Tools**: read, search, edit
- **Expertise**: OWASP guidelines, XSS prevention, CSRF protection, input validation, authentication, dependency vulnerabilities

#### `database-expert.agent.md`
- **Purpose**: Work with Entity Framework Core and database design
- **Tools**: read, edit, create, search
- **Expertise**: EF Core, migrations, query optimization, repository patterns, async data access, SQL Server

#### `devops-specialist.agent.md`
- **Purpose**: Manage GitHub Actions workflows, CI/CD pipelines, and Azure deployments
- **Tools**: read, edit, create, search
- **Expertise**: GitHub Actions, CI/CD pipelines, Azure Web Apps, workflow optimization, deployment strategies, build automation, secrets management

**Used by**: GitHub Copilot coding agent, available in the agents dropdown in supported IDEs and on GitHub.com

### 4. Path-Specific Instructions
**Directory**: `.github/instructions/`

These files provide targeted guidance for specific file types:

#### `csharp.instructions.md`
- **Applies to**: All `.cs` files
- **Content**: C# coding style, async/await guidelines, dependency injection, error handling, null safety, documentation, performance, security

#### `tests.instructions.md`
- **Applies to**: All test files in `tests/` directory
- **Content**: Test structure, xUnit conventions, test organization, mocking, async testing, integration tests, test data, assertions, code coverage

#### `config.instructions.md`
- **Applies to**: `.csproj`, `.json`, `.yml`, `.yaml` files
- **Content**: Project file standards, JSON configuration, YAML guidelines, GitHub Actions workflows, environment-specific configuration

#### `razor.instructions.md`
- **Applies to**: `.cshtml` and `.razor` files
- **Content**: Razor syntax, view structure, layouts, tag helpers, model binding, HTML best practices, security, performance

#### `web-assets.instructions.md`
- **Applies to**: `.js`, `.css`, `.html` files
- **Content**: JavaScript best practices, CSS structure and conventions, HTML semantics, accessibility, performance, browser compatibility

#### `workflows.instructions.md`
- **Applies to**: `.github/workflows/**/*.yml`, `.github/workflows/**/*.yaml` files
- **Content**: GitHub Actions workflow structure, YAML syntax, job configuration, actions and steps, environment variables, secrets management, caching, .NET patterns, deployment, security, performance optimization

#### `coding-agent.instructions.md`
- **Applies to**: All files (but only for coding agents, not code review)
- **Content**: Workflow steps, testing requirements, common patterns, security checklist, performance considerations, completion checklist

### 4. Updated README
**File**: `README.md`

Enhanced documentation includes:
- Project overview
- Tech stack details
- Getting started guide
- Build and test instructions
- Project structure
- AI coding agent configuration explanation
- CI/CD pipeline information

## How AI Agents Use These Files

### GitHub Copilot (All Editors)
1. Automatically reads `.github/copilot-instructions.md` for repository context
2. Applies path-specific instructions from `.github/instructions/` based on the file being edited
3. Uses context to generate more accurate code completions and suggestions

### GitHub Copilot Coding Agents
1. Reads all instruction files when making changes
2. Follows workflow guidelines from `AGENTS.md`
3. Uses `coding-agent.instructions.md` for task execution guidance
4. Applies path-specific rules when editing different file types
5. **Can be invoked as specialized custom agents** from `.github/agents/` for specific tasks like writing tests, creating views, building APIs, security audits, database work, or CI/CD pipeline management

### GitHub Copilot Custom Agents
1. Available in the agents dropdown in GitHub.com, VS Code, JetBrains IDEs, Eclipse, and Xcode
2. Each custom agent has specialized knowledge and tools for specific tasks
3. Invoke specific agents for focused work:
   - **@test-writer** for creating or improving tests
   - **@razor-specialist** for front-end and view work
   - **@api-developer** for controller and API development
   - **@security-auditor** for security reviews
   - **@database-expert** for EF Core and database tasks
   - **@devops-specialist** for GitHub Actions workflows, CI/CD pipelines, and Azure deployments

### GitHub Copilot Code Review
1. Reads repository-wide and path-specific instructions
2. Skips `coding-agent.instructions.md` (marked with `excludeAgent: "code-review"`)
3. Uses instructions to provide context-aware code reviews

### Other AI Tools
- Tools supporting AGENTS.md format will read `AGENTS.md`
- Some tools may support GitHub Copilot instruction formats
- Custom agents can be configured to use these files

## Benefits

### For Developers
- Consistent code generation that follows project conventions
- Better code suggestions aligned with project architecture
- Faster onboarding for new developers (AI can guide them)
- Reduced need to manually specify context in prompts

### For AI Agents
- Clear understanding of project structure and conventions
- Knowledge of build, test, and deployment processes
- Awareness of security requirements and best practices
- Guidance on common patterns used in the codebase

### For Code Quality
- More consistent code style across AI-assisted changes
- Better adherence to security and performance best practices
- Improved test coverage through testing guidelines
- Reduced technical debt from AI-generated code

## Maintenance

### Keeping Instructions Current
- Update instructions when project conventions change
- Review and update after major refactoring
- Add new path-specific instructions for new file types
- Document new build or test commands as they're added

### Best Practices
- Keep instructions concise and actionable
- Use examples where helpful
- Focus on project-specific conventions (not general knowledge)
- Update README when configuration changes significantly

## Testing the Configuration

### Verify GitHub Copilot Can Read Instructions
1. Open the repository in VS Code (or your preferred IDE)
2. Open a C# file
3. Ask GitHub Copilot Chat: "What coding conventions should I follow?"
4. Copilot should reference the instructions from the configuration files

### Verify Coding Agent Behavior
1. Use GitHub Copilot coding agent to make a change
2. Observe that it follows the workflows outlined in `AGENTS.md`
3. Check that it runs tests before considering work complete
4. Verify it follows the security checklist

## Resources

- **GitHub Copilot Documentation**: https://docs.github.com/en/copilot
- **Custom Instructions Guide**: https://docs.github.com/en/copilot/customizing-copilot/adding-custom-instructions-for-github-copilot
- **AGENTS.md Standard**: https://agents.md
- **MCP (Model Context Protocol)**: https://modelcontextprotocol.io

## Future Enhancements

Potential additions to consider:
- ~~Custom agents for specific tasks~~ ✅ Completed: Added 6 specialized agents
- MCP server configuration for external tool integration
- Additional path-specific instructions for other file types
- Repository-specific skills or capabilities
- Integration with project-specific tools and workflows

## Questions or Issues?

If you encounter issues with the AI agent configuration:
1. Check that your IDE has the latest GitHub Copilot extension
2. Verify the configuration files are properly formatted
3. Ensure frontmatter syntax in instruction files is correct
4. Check GitHub Copilot settings to ensure custom instructions are enabled

For bugs or improvements to the configuration, please open an issue in the repository.
