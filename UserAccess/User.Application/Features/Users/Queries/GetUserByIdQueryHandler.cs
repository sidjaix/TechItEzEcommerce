using MediatR;
using Microsoft.Extensions.Logging;
using UserAccess.Application.Dtos;
using User.Application.Interfaces;

namespace UserAccess.Application.Features.Users.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResponseDto>
    {
        private readonly IIdentityRepository identityRepository;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;

        public GetUserByIdQueryHandler(IIdentityRepository identityRepository, ILogger<GetUserByIdQueryHandler> logger)
        {
            this.identityRepository = identityRepository;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            _logger.LogInformation("Attempting to get user by id {UserId}", request.UserId);

            var existingUser = await identityRepository.GetUserByIdAsync(request.UserId, cancellationToken);

            if (existingUser is null)
            {
                _logger.LogWarning("User with id {UserId} not found", request.UserId);
                response.Message = $"User does not exist with specified Id {request.UserId}";
                response.IsSuccess = false;
                return response;
            }
            response.Result = existingUser;
            return response;
        }
    }
}