using MediatR;
using Microsoft.Extensions.Logging;
using UserAccess.Application.Dtos;
using UserAccess.Application.Interfaces;

namespace UserAccess.Application.Features.Users.Queries
{
    public class GetUserAddressesQueryHandler : IRequestHandler<GetUserAddressesQuery, ResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetUserAddressesQueryHandler> _logger;

        public GetUserAddressesQueryHandler(IUserRepository userRepository, ILogger<GetUserAddressesQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(GetUserAddressesQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            _logger.LogInformation("Attempting to get addresses for user {UserId}", request.UserId);

            var addresses = await _userRepository.GetUserAddressesAsync(request.UserId);

            if (addresses is null || addresses.Count == 0)
            {
                _logger.LogWarning("No addresses found for user {UserId}", request.UserId);
                response.Message = "Address not found";
                response.IsSuccess = false;
                return response;
            }

            response.Result = addresses;
            return response;
        }
    }
}