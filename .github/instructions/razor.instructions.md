---
applyTo: "**/*.cshtml,**/*.razor"
---

# Razor Views and Pages Instructions

## Razor Syntax

- Use `@` to switch between HTML and C# code
- Use `@{}` for multi-line C# blocks
- Use `@()` for explicit expressions
- Use `@:` for single-line plain text output
- Use `<text>` tags for multi-line plain text

## View Structure

- Start with `@model` directive to specify the model type
- Use `@using` directives at the top for namespaces
- Keep views focused on presentation logic only
- Move complex logic to controllers or view models
- Use partial views (`_PartialName.cshtml`) for reusable components

## Layout and Sections

- Use `_Layout.cshtml` for consistent site structure
- Define sections with `@section SectionName { }`
- Render sections with `@RenderSection("SectionName", required: false)`
- Use `@RenderBody()` in layouts to render the main content
- Keep layouts simple and focused

## Tag Helpers

- Prefer Tag Helpers over HTML Helpers for cleaner syntax
- Use `asp-` attributes for ASP.NET Core Tag Helpers
- Examples: `asp-controller`, `asp-action`, `asp-route-id`
- Use `asp-validation-for` for client-side validation
- Use `asp-append-version` for cache busting static files

## Model Binding and Validation

- Use strongly-typed models with `@model` directive
- Display validation messages with `asp-validation-summary`
- Use `asp-validation-for` on individual form fields
- Apply data annotations in view models for validation
- Use `ModelState.IsValid` in controllers before processing

## HTML Best Practices

- Use semantic HTML5 elements (`<header>`, `<nav>`, `<main>`, `<footer>`)
- Ensure proper heading hierarchy (h1, h2, h3)
- Add `alt` attributes to images for accessibility
- Use `label` elements properly associated with form inputs
- Include ARIA attributes when needed for accessibility

## Security

- Always encode output with `@` (Razor does this automatically)
- Use `@Html.Raw()` only for trusted content
- Never render unvalidated user input directly
- Use anti-forgery tokens on forms: `<form asp-antiforgery="true">`
- Sanitize any HTML content from users

## Performance

- Minimize server-side processing in views
- Use caching for expensive operations
- Lazy-load JavaScript when possible
- Minimize inline styles and scripts
- Use bundling and minification for assets

## Maintainability

- Keep views simple and readable
- Use descriptive variable names
- Add comments for complex view logic
- Organize files in logical folder structures
- Use ViewData and ViewBag sparingly - prefer strongly-typed models

## Component Reusability

- Create partial views for repeated UI elements
- Use view components for complex reusable functionality
- Pass models to partial views for data
- Keep partial views self-contained when possible
- Consider using display templates for consistent rendering
