---
applyTo: "**/*.js,**/*.css,**/*.html"
---

# Static Web Assets Instructions

## JavaScript

### Code Style
- Use modern ES6+ syntax (const/let, arrow functions, template literals)
- Prefer `const` over `let`, avoid `var`
- Use meaningful variable and function names
- Add comments for complex logic
- Keep functions small and focused

### DOM Manipulation
- Cache DOM queries when used multiple times
- Use event delegation for dynamic elements
- Remove event listeners when no longer needed
- Validate input before processing
- Handle errors gracefully

### Async Operations
- Use `async`/`await` for asynchronous operations
- Handle promise rejections properly
- Show loading states during async operations
- Provide user feedback for long operations

### Security
- Sanitize user input before displaying
- Use textContent instead of innerHTML for untrusted content
- Validate data on both client and server
- Avoid eval() and similar unsafe functions
- Use Content Security Policy (CSP)

## CSS

### Structure
- Use consistent naming conventions (BEM, SMACSS, or similar)
- Organize styles logically (layout, components, utilities)
- Avoid overly specific selectors
- Keep selectors performant
- Use classes instead of IDs for styling

### Responsive Design
- Mobile-first approach when possible
- Use relative units (rem, em, %) instead of fixed pixels
- Use media queries for responsive layouts
- Test on different screen sizes
- Consider touch interfaces

### Best Practices
- Avoid !important except as a last resort
- Don't use inline styles
- Group related properties together
- Use shorthand properties when appropriate
- Maintain consistent spacing and indentation

### Performance
- Minimize use of expensive properties (box-shadow, filters)
- Avoid universal selectors (*)
- Use efficient selectors
- Minimize reflows and repaints
- Consider critical CSS for above-the-fold content

### Browser Compatibility
- Test in major browsers
- Use vendor prefixes when needed
- Provide fallbacks for newer features
- Consider progressive enhancement

## HTML

### Semantic HTML
- Use appropriate semantic elements
- Proper heading hierarchy (h1-h6)
- Use lists (ul, ol) for list data
- Use tables only for tabular data
- Use form elements appropriately

### Accessibility
- Include alt text for images
- Use proper ARIA labels when needed
- Ensure keyboard navigation works
- Maintain good color contrast
- Provide text alternatives for non-text content

### Performance
- Minimize HTML size
- Load scripts at appropriate times (defer/async)
- Optimize images before including
- Use appropriate image formats (WebP, AVIF with fallbacks)
- Lazy-load images below the fold

### Best Practices
- Validate HTML syntax
- Use consistent indentation
- Close all tags properly
- Use lowercase for element names and attributes
- Quote attribute values

## Static Assets Organization

### File Structure
- Keep assets organized by type (js/, css/, images/)
- Use descriptive file names
- Version assets or use cache-busting
- Minify for production
- Bundle related assets

### Images
- Optimize image sizes and formats
- Provide multiple resolutions for responsive images
- Use SVG for icons and simple graphics
- Include width and height attributes
- Consider using CSS sprites for small icons

### Fonts
- Use web-safe fonts with fallbacks
- Limit number of font weights and styles
- Use font-display for loading strategy
- Consider system fonts for performance
- Subset fonts if possible
