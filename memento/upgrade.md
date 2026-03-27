# .NET Solution Upgrade Guide

You are working on a .NET solution containing multiple projects:

- UserAccess API
- Product API
- Cart API
- Order API
- UI project
- Consumer
- Shared

Environment:

- macOS (M2)
- VS Code
- Target SDK: .NET 10 only

Task:

1. Scan the entire solution and identify:
   - Target frameworks used in each project
   - NuGet package versions
   - Any deprecated or incompatible packages
2. Highlight projects not targeting .NET 10
3. List all upgrade risks

Output:

- Table of projects with current vs target framework
- List of incompatible dependencies
- Suggested upgrade plan (step-by-step)

# Upgrade all NuGet dependencies across the solution to versions compatible with .NET 10.

Requirements:

- Replace deprecated packages
- Upgrade to latest stable versions
- Ensure compatibility with ASP.NET Core 10
- Maintain backward compatibility where possible

Output:

- Updated package references per project
- List of replaced/deprecated packages
- Any breaking changes to handle

# Refactor all API projects to use a consistent Program.cs structure for .NET 10.

Requirements:

- Use minimal hosting model
- Add Serilog integration
- Add Swagger/OpenAPI
- Add global exception handling middleware
- Add dependency injection structure

Output:

- Clean, production-ready Program.cs template
- Reusable pattern for all APIs
