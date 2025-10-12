# Copilot Instructions for Wordler

## Project Overview

Wordler is a Wordle-solving aid built with Blazor WebAssembly. It helps users get unstuck by taking known letters (green/yellow clues from Wordle) and showing all possible letter combinations.

**Tech Stack:**
- .NET 6.0
- Blazor WebAssembly (client-side)
- C# 10
- xUnit for unit tests
- bUnit for Blazor component tests

## Project Structure

```
Wordler/
├── src/
│   ├── Wordler.Core/          # Core logic for letter mixing/permutation
│   └── Wordler.Web/           # Blazor WebAssembly app
│       ├── Components/        # Razor components
│       ├── Pages/            # Razor pages
│       ├── Shared/           # Shared layouts
│       └── wwwroot/          # Static assets
└── test/
    ├── Wordler.Core.UnitTests/        # Unit tests for core logic
    └── Wordler.Web.ComponentTests/    # Component tests using bUnit
```

## Code Style and Conventions

### General Guidelines
- Follow the .editorconfig settings in the repository
- Use 4 spaces for indentation in C# files
- Use 2 spaces for YAML and XML files
- Prefer CRLF line endings for consistency with Windows development
- Enable implicit usings and nullable reference types

### C# Style
- Use language keywords over BCL types (`int` not `Int32`)
- Don't use `this.` qualifier unless required
- Sort using directives with System namespaces first
- Separate import directive groups
- Use parentheses for clarity in arithmetic and binary operations
- Use meaningful variable names that reflect their purpose

### Blazor/Razor Style
- Keep component files focused and small
- Use `@code` blocks at the end of `.razor` files
- Use proper parameter binding with `[Parameter]` attributes
- Follow the existing component structure in the project

## Building and Testing

### Build Commands
```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Build specific project
dotnet build src/Wordler.Web/Wordler.Web.csproj

# Publish for deployment
dotnet publish -c:Release -o:publish -p:GHPages=true
```

### Testing
```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run specific test project
dotnet test test/Wordler.Core.UnitTests/
```

### Test Guidelines
- Write unit tests for core logic in `Wordler.Core.UnitTests`
- Write component tests for Blazor components in `Wordler.Web.ComponentTests`
- Use FluentAssertions for test assertions
- Follow the existing test naming pattern: descriptive method names that explain what's being tested
- Test classes should match the class they're testing (e.g., `MixerTests` for `Mixer`)

## Domain-Specific Context

### Key Concepts
- **Green Letters**: Letters in the correct position (use `.At()` extension)
- **Yellow Letters**: Letters in the word but wrong position (use `.NotAt()` extension)
- **Letter Mixing**: The core algorithm generates all valid letter combinations given the constraints

### Important Classes
- `Mixer`: Core algorithm for generating letter permutations based on constraints
- `LetterSpace`: Represents a letter and its position constraints
- `InputMixer`: Main Blazor component for user input
- `LetterInput`, `WordInput`: Sub-components for letter entry

## Common Tasks

### Adding a New Component
1. Create `.razor` file in `src/Wordler.Web/Components/`
2. Add corresponding `.razor.css` for scoped styles if needed
3. Register in `_Imports.razor` if it's a shared component
4. Write component tests in `test/Wordler.Web.ComponentTests/`

### Modifying Core Logic
1. Update logic in `src/Wordler.Core/`
2. Add/update unit tests in `test/Wordler.Core.UnitTests/`
3. Ensure all tests pass before committing
4. Keep the algorithm efficient - Wordle uses 5-letter words

### UI Changes
1. Modify Razor components in `src/Wordler.Web/`
2. Update CSS in `wwwroot/css/` or component-scoped `.razor.css` files
3. Test manually by running the app
4. Maintain the simple, clean UI aesthetic

## Deployment

The project uses GitHub Actions to:
1. Build and test on every push/PR to main
2. Publish to GitHub Pages automatically on main branch
3. Generate test reports

The workflow is defined in `.github/workflows/build.yml`.

## Best Practices

1. **Minimal Changes**: Make surgical, focused changes
2. **Test Coverage**: Add tests for new features
3. **Performance**: Keep letter mixing algorithm efficient
4. **Simplicity**: The app intentionally stays simple - don't over-engineer
5. **Accessibility**: Maintain semantic HTML and ARIA labels
6. **Mobile-Friendly**: Ensure UI works on mobile devices

## Notes for Copilot

- This is a side project/utility app - keep solutions straightforward
- The core value is the letter permutation algorithm - don't break it
- UI should remain clean and focused on the task
- The app is deployed to GitHub Pages as a static site
- No backend or database - everything runs client-side
- The FAQ section in `Index.razor` addresses common questions about the tool
