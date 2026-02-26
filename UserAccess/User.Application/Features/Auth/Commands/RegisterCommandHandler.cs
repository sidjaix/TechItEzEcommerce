using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using User_Core.Entities;
using UserAccess.Application.Dtos;
using UserAccess.Application.Dtos.User;

namespace UserAccess.Application.Features.Auth.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResponseDto>
{
    private readonly UserManager<User_Core.Entities.User> _userManager;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public RegisterCommandHandler(ILogger<RegisterCommandHandler> logger, UserManager<User_Core.Entities.User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<ResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var model = request.Register;
        var isExistsUser = await _userManager.FindByNameAsync(model.Email);
        if (isExistsUser != null)
        {
            _logger.LogWarning("Registration failed: Username {UserName} already exists.", model.Email);
            return new ResponseDto()
            {
                Result = null,
                IsSuccess = false,
                Message = "UserName Already Exists"
            };
        }

        var user = new User_Core.Entities.User
        {
            Name = model.Name,
            UserName = model.Email,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError("User creation failed for {UserName}: {Errors}", model.Email, errors);
            return new ResponseDto()
            {
                IsSuccess = false,
                Message = errors
            };
        }

        _logger.LogInformation("User {UserName} created successfully.", user.UserName);

        // Add a default 'USER' role to all new user
        var roleResult = await _userManager.AddToRoleAsync(user, RoleStore.USER);
        if (!roleResult.Succeeded)
        {
            _logger.LogError("Failed to add role '{Role}' to user {UserName}.", RoleStore.USER, user.UserName);
            // Even if role assignment fails, the user is already created.
            // Depending on business rules, you might want to compensate (delete user) or just log the error.
            // For now, we will treat it as a partial success but notify that role assignment failed.
            return new ResponseDto()
            {
                IsSuccess = false, // Or true, depending on desired outcome. Let's mark it as failure.
                Message = $"User created, but failed to assign role '{RoleStore.USER}'. Please contact support."
            };
        }

        _logger.LogInformation("Successfully assigned role '{Role}' to user {UserName}.", RoleStore.USER, user.UserName);

        return new ResponseDto()
        {
            IsSuccess = true,
            Message = "User registered successfully."
        };
    }
}