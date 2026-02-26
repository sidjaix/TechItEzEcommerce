using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User_Core;
using User_Core.Entities;
using UserAccess.Application.Dtos.Auth;
using UserAccess.Application.Mappers;

namespace UserAccess.Application.Features.Auth.Commands;

public class LoginCommandHandler : IRequestHandler<Commands.LoginCommand, Dtos.Auth.LoginResponseModel>
{
    private readonly UserManager<User_Core.Entities.User> _userManager;
    private readonly Dtos.Auth.JwtOptions _jwtOptions;

    public LoginCommandHandler(UserManager<User_Core.Entities.User> userManager, IOptions<Dtos.Auth.JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<Dtos.Auth.LoginResponseModel> Handle(Commands.LoginCommand request, CancellationToken cancellationToken)
    {
        var response = new Dtos.Auth.LoginResponseModel();
        var loginUser = await _userManager.FindByNameAsync(request.UserName);

        if (loginUser is null)
        {
            response.User = null;
            response.Token = string.Empty;
            return response;
        }

        bool isValid = await _userManager.CheckPasswordAsync(loginUser, request.Password);

        if (!isValid)
        {
            response.User = null;
            response.Token = string.Empty;
            return response;
        }

        var roles = await _userManager.GetRolesAsync(loginUser);
        var token = GenerateToken(loginUser, roles);

        response.User = loginUser.MapToDto();
        response.Token = token;
        return response;
    }

    private string GenerateToken(User_Core.Entities.User user, IEnumerable<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Name, user.Name!)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtOptions.Secret));

        SigningCredentials signingCred = new SigningCredentials(
            key: securityKey,
            algorithm: SecurityAlgorithms.HmacSha512Signature
        );

        var tokenExpiresOn = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresOn);

        SecurityToken securityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: tokenExpiresOn,
            signingCredentials: signingCred
        );
        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
}