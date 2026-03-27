using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UserAccess.Application.Dtos;
using UserAccess.Application.Mappers;
using UserAccess.Core.Entities;

namespace UserAccess.Application.Features.Users.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(UserManager<User> userManager, ILogger<UpdateUserCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var userData = request.User;

            _logger.LogInformation("Attempting to update user {UserId}", userData.UserId);

            var existingUser = await _userManager.FindByIdAsync(userData.UserId);
            if (existingUser is null)
            {
                _logger.LogWarning("User {UserId} not found", userData.UserId);
                response.IsSuccess = false;
                response.Message = "User does not exist.";
                return response;
            }

            existingUser.Name = userData.Name;
            existingUser.DateOfBirth = userData.DateOfBirth;
            existingUser.Gender = userData.Gender;
            existingUser.Email = userData.Email;
            existingUser.UserName = userData.UserName;
            existingUser.PhoneNumber = userData.PhoneNumber;

            var result = await _userManager.UpdateAsync(existingUser);

            if (!result.Succeeded)
            {
                _logger.LogError("Failed to update user {UserId}. Errors: {Errors}", userData.UserId, result.Errors);
                response.IsSuccess = false;
                response.Message = "Failed to update user.";
                response.Result = result.Errors;
                return response;
            }

            _logger.LogInformation("User {UserId} updated successfully", userData.UserId);
            response.Message = "User profile updated successfully.";
            response.Result = existingUser.MapToDto();
            return response;
        }
    }
}