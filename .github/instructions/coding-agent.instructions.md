---
applyTo: "**"
excludeAgent: "code-review"
---

# Coding Agent Specific Instructions

These instructions apply only to GitHub Copilot coding agents making changes to the codebase.

## Workflow Steps

### 1. Understanding the Task
- Read the issue or task description carefully
- Ask clarifying questions if requirements are unclear
- Identify which files and components need to be changed
- Consider the impact on existing functionality

### 2. Exploring the Codebase
- Use search to find relevant files and code
- Review existing patterns and conventions
- Check test files to understand expected behavior
- Look for similar implementations as reference

### 3. Planning Changes
- Create a minimal change plan
- Identify required test changes or additions
- Consider backward compatibility
- Plan for error handling and edge cases

### 4. Making Changes
- Make small, focused changes
- Follow existing code patterns and conventions
- Keep changes minimal - don't refactor unnecessarily
- Update tests alongside production code
- Add appropriate error handling

### 5. Validation
- Build the solution to check for compilation errors
- Run relevant tests to verify functionality
- Run full test suite before completing
- Check for any security vulnerabilities introduced
- Review changes before committing

### 6. Documentation
- Update code comments if behavior changes
- Update README or documentation if needed
- Add XML documentation for new public APIs
- Explain non-obvious decisions in commit messages

## Testing Requirements

### Always Test Before Completing
```bash
# Build the solution
dotnet build --configuration Release

# Run all tests
dotnet test --configuration Release

# Check code coverage if making significant changes
dotnet test --collect:"XPlat Code Coverage"
```

### Test Coverage Expectations
- Unit tests for new business logic
- Integration tests for new endpoints or features
- Update existing tests when behavior changes
- Test both success and failure scenarios

## Common Patterns in This Project

### Service Pattern
- Services are registered in `Program.cs` or `Startup.cs`
- Services use dependency injection
- Services contain business logic, not controllers
- Services should have interfaces for testing

### Controller Pattern
- Controllers handle HTTP concerns
- Controllers call services for business logic
- Controllers return appropriate status codes
- Controllers validate input

### Data Access Pattern
- Entity Framework Core for database access
- Repository pattern for data access
- DbContext is scoped to request lifetime
- Use async methods for database operations

### Error Handling Pattern
- Exceptions for exceptional situations
- Validation at multiple layers
- Meaningful error messages
- Appropriate HTTP status codes

## Security Checklist

Before completing any changes:
- [ ] No secrets or credentials in code
- [ ] User input is validated and sanitized
- [ ] SQL injection prevention (parameterized queries)
- [ ] XSS prevention (proper encoding)
- [ ] CSRF protection (anti-forgery tokens on forms)
- [ ] Authentication/authorization checks where needed
- [ ] Secure random number generation if needed
- [ ] No sensitive data in logs

## Performance Considerations

- Use async/await for I/O operations
- Avoid N+1 query problems
- Consider caching for expensive operations
- Minimize memory allocations in hot paths
- Use efficient LINQ queries

## Before Marking Complete

- [ ] All compilation errors fixed
- [ ] All tests passing
- [ ] Code follows project conventions
- [ ] Security considerations addressed
- [ ] Performance impact considered
- [ ] Documentation updated if needed
- [ ] Changes are minimal and focused
- [ ] No unintended side effects

## Getting Unstuck

If you encounter issues:
1. Check the error message carefully
2. Review similar working code in the project
3. Check the project documentation (README, AGENTS.md)
4. Look at test files for usage examples
5. Consult the GitHub Actions workflow for build/test commands
6. Ask for help if stuck after trying these steps
