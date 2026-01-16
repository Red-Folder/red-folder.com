---
applyTo: "**/*.cs"
---

# C# Source Code Instructions

## Coding Style

- Use modern C# features appropriate for the target framework:
  - .NET 8.0 projects: Use C# 12 features (file-scoped namespaces, global usings, nullable reference types)
  - .NET Standard 2.0 projects: Use C# 7.3 compatible syntax
  - .NET Standard 1.6 projects: Use C# 7.0 compatible syntax
- Follow Microsoft C# coding conventions
- Use meaningful names that reflect the purpose of the code
- Keep methods focused and small (ideally under 20-30 lines)
- Prefer composition over inheritance
- Use readonly fields and properties when values don't change

## Async/Await Guidelines

- Always use async/await for I/O-bound operations
- Don't use `.Result` or `.Wait()` - use await instead
- Use `Task.WhenAll` for parallel async operations
- Name async methods with `Async` suffix
- Avoid async void except for event handlers

## Dependency Injection

- Use constructor injection for required dependencies
- Register services in `Program.cs` or `Startup.cs`
- Prefer scoped lifetime for services that maintain state within a request
- Use transient lifetime for lightweight, stateless services
- Use singleton lifetime sparingly and only for thread-safe services

## Error Handling

- Use exceptions for exceptional circumstances, not flow control
- Catch specific exceptions rather than generic `Exception`
- Log exceptions with sufficient context for debugging
- Return meaningful error messages to users (without exposing internal details)
- Use try-catch at appropriate boundaries (controllers, service entry points)

## Null Safety

- Enable nullable reference types in project files
- Use null-coalescing operators (`??`, `??=`) where appropriate
- Validate inputs and throw `ArgumentNullException` when appropriate
- Use nullable value types (`int?`) when null has semantic meaning

## Documentation

- Add XML documentation comments for public APIs (classes, methods, properties)
- Include `<summary>`, `<param>`, and `<returns>` tags
- Document non-obvious behavior, side effects, or exceptions thrown
- Keep comments up-to-date when code changes

## Performance Considerations

- Use `StringBuilder` for multiple string concatenations
- Prefer `string.IsNullOrEmpty` or `string.IsNullOrWhiteSpace` over length checks
- Use `ConfigureAwait(false)` in library code that doesn't need synchronization context
- Consider using `ValueTask` for hot paths where allocations matter
- Use efficient LINQ queries and avoid multiple enumerations

## Security

- Always validate and sanitize user input
- Use parameterized queries to prevent SQL injection
- Encode output to prevent XSS attacks
- Don't log sensitive information (passwords, tokens, PII)
- Use secure random number generation for cryptographic purposes
