using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserAccess.Application.Dtos;
using Entity = UserAccess.Core.Entities;
using UserAccess.Application.Mappers;

namespace UserAccess.Application.Features.Users.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResponseDto>
    {
        private readonly UserManager<Entity.User> _userManager;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;

        public GetUserByIdQueryHandler(UserManager<Entity.User> userManager, ILogger<GetUserByIdQueryHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            _logger.LogInformation("Attempting to get user by id {UserId}", request.UserId);

            var existingUser = await _userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (existingUser is null)
            {
                _logger.LogWarning("User with id {UserId} not found", request.UserId);
                response.Message = $"User does not exist with specified Id {request.UserId}";
                response.IsSuccess = false;
                return response;
            }

            response.Result = existingUser.MapToDto();
            return response;
        }
    }
}