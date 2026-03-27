using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAccess.Application.Dtos;
using Entity = UserAccess.Core.Entities;
using UserAccess.Application.Mappers;
using User.Application.Interfaces;
using UserAccess.Application.Interfaces;

namespace UserAccess.Application.Features.Auth.Commands;

public class LoginCommandHandler(IIdentityRepository identityRepository, IOptions<JwtOptions> jwtOptions) : IRequestHandler<LoginCommand, LoginResponseModel>
{
    public async Task<LoginResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = new LoginResponseModel();
        bool isValid = await identityRepository.ValidateUserPasswordAsync(request.UserName, request.Password);
        if (!isValid)
        {
            response.User = default;
            response.Token = string.Empty;
            return response;
        }

        var user = await identityRepository.GetUserRolesAsync(request.UserName);
        var token = GenerateToken(user);

        response.User = user;
        response.Token = token;
        return response;
    }

    private string GenerateToken(UserModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(jwtOptions.Value.Secret);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Name, user.Name!)
        };

        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtOptions.Value.Secret));

        SigningCredentials signingCred = new SigningCredentials(
            key: securityKey,
            algorithm: SecurityAlgorithms.HmacSha512Signature
        );

        var tokenExpiresOn = DateTime.UtcNow.AddMinutes(jwtOptions.Value.ExpiresOn);

        SecurityToken securityToken = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            claims: claims,
            expires: tokenExpiresOn,
            signingCredentials: signingCred
        );
        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
}