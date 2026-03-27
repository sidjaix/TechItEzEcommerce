# Refactor the existing Azure Function project to align with clean architecture using the current layered solution.

Current Structure:

under UserAccess folder

- User-Core (Entities, Enums)
- User.Application (DTOs, MediatR, Mapper)
- User.Infrastructure (Repositories, DbContext, Migrations)
- User-Api (Controllers, Extensions)

Issues:

- Azure Function directly calls repository
- Tight coupling between layers

Target Changes:

1. Ensure User-Core Contains
   - Entities
   - Enums
2. Ensure Application layer contains:
   - DTOs
   - MediatR Commands/Queries/Handlers
   - Interfaces only
3. Refactor Azure Function:
   - Remove repository usage
   - Inject IMediator
   - Use mediator.Send()
4. Ensure handlers call IUserRepository
5. Maintain async/await pattern
6. Add structured logging using Serilog

Output:

- Updated folder structure
- Refactored Azure Function
- Updated MediatR handlers
- Clean separation of concerns
