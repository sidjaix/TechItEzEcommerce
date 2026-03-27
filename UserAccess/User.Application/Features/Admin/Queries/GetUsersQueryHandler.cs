using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserAccess.Application.Dtos;
using UserAccess.Application.Mappers;
using UserAccess.Core.Entities;

namespace UserAccess.Application.Features.Admin.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<GetUsersQueryHandler> _logger;

        public GetUsersQueryHandler(UserManager<User> userManager, ILogger<GetUsersQueryHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            try
            {
                _logger.LogInformation("Getting all users");
                var users = await _userManager.Users.AsNoTracking().Select(x => x.MapToDto()).ToListAsync(cancellationToken);
                response.Result = users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all users");
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }
    }
}