---
applyTo: "tests/**/*.cs"
---

# Test Code Instructions

## Test Structure

- Follow AAA pattern: Arrange, Act, Assert
- One logical assertion per test method when possible
- Use descriptive test method names that explain the scenario being tested
- Format: `MethodName_Scenario_ExpectedResult` or `Given_When_Then` style

## xUnit Conventions

- Use `[Fact]` for tests without parameters
- Use `[Theory]` with `[InlineData]` or `[MemberData]` for parameterized tests
- Use `Assert.Equal`, `Assert.True`, `Assert.False`, etc. for assertions
- Use `Assert.Throws<TException>` or `Assert.ThrowsAsync<TException>` for exception testing
- Use `[Trait]` attributes to categorize tests for filtering

## Test Organization

- Mirror the source code structure in test projects
- Group related tests in the same test class
- Use nested classes to organize tests for different aspects of functionality
- Keep test classes focused on testing a single class or component

## Mocking and Dependencies

- Use mocking frameworks (like Moq) for dependencies
- Mock only the dependencies needed for the test
- Verify important interactions with mocks
- Don't over-mock - test real implementations when reasonable
- Use test doubles (stubs, fakes) for simple scenarios

## Async Testing

- Use `async Task` for async test methods (not `async void`)
- Use `await` for all async operations in tests
- Test both successful and error scenarios for async methods
- Be careful with timing-dependent tests - avoid Thread.Sleep

## Integration Tests

- Use `WebApplicationFactory<TStartup>` for ASP.NET Core integration tests
- Set up test-specific configuration and services
- Clean up test data after tests run
- Use realistic test data that reflects actual usage
- Consider using test databases or in-memory databases

## Test Data

- Use realistic but not production data
- Make test data obvious and easy to understand
- Use constants or builder patterns for complex test objects
- Avoid magic numbers - use named constants
- Keep test data minimal - only what's needed for the test

## Assertions

- Use the most specific assertion available
- Provide meaningful assertion messages for clarity
- Test the most important aspects first
- Avoid multiple unrelated assertions in a single test
- Consider custom assertions for complex scenarios

## Code Coverage

- Aim for high coverage of critical paths and business logic
- Don't sacrifice test quality for coverage percentage
- Focus on testing behavior, not implementation details
- Cover edge cases and error conditions
- Document intentionally untested code

## Test Performance

- Keep unit tests fast (milliseconds, not seconds)
- Use appropriate test fixtures to share expensive setup
- Parallelize tests when possible (xUnit does this by default)
- Avoid unnecessary database or network operations
- Use in-memory implementations for fast tests

## Common Pitfalls

- Don't test framework code - focus on your business logic
- Avoid brittle tests that break with minor refactoring
- Don't use production configuration or services in tests
- Avoid shared mutable state between tests
- Don't catch exceptions in tests - let them fail
