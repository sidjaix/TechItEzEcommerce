using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User_Api.Services.IServices;
using User_Core;
using User_Core.Entities;
using User_Core.Models;

namespace User_Api.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly LoginResponseModel _response;
    private readonly JwtOptions _jwtOptions;
    private readonly ILogger<AuthService> _logger;
    public AuthService(ILogger<AuthService> logger, UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JwtOptions> jwtOptions)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtOptions = jwtOptions.Value;
        _response = new LoginResponseModel();
    }
    public async Task<LoginResponseModel> LoginAsync(LoginModel loginRequest)
    {
        var loginUser = await _userManager.FindByNameAsync(loginRequest.UserName);

        bool isValid = await _userManager.CheckPasswordAsync(loginUser, loginRequest.Password);
        _logger.LogInformation("User has found with details {@loginRequest}", loginRequest);
        if (loginUser is null || !isValid)
        {
            _response.User = default;
            _response.Token = string.Empty;
            return _response;
        }

        //if user was found , Generate JWT Token
        var roles = await _userManager.GetRolesAsync(loginUser);
        var token = GenerateToken(loginUser, roles);

        _response.User = loginUser.MapToDto();
        _response.Token = token;
        return _response;
    }

    public async Task<ResponseDto> RegisterAsync(RegisterModel model)
    {
        var isExistsUser = await _userManager.FindByNameAsync(model.Email);
        if (isExistsUser != null)
        {
            var res = new ResponseDto()
            {
                Result = null,
                IsSuccess = false,
                Message = "UserName Already Exists"
            };
            return res;
        }

        var user = new User
        {
            Name = model.Name,
            UserName = model.Email,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            var errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += error.Description;
            }
            var res = new ResponseDto()
            {
                IsSuccess = false,
                Message = errors
            };
            return res;
        }
        // Add a default 'USER' role to all new user
        var roleResult = await _userManager.AddToRoleAsync(user, RoleStore.USER);
        if (!roleResult.Succeeded)
        {
            var res = new ResponseDto()
            {
                IsSuccess = false,
                Message = $"Role '{RoleStore.USER}' does not Exist."
            };
            return res;
        }
        var response = new ResponseDto()
        {
            IsSuccess = true,
            Message = "User Register Successfully."
        };
        return response;
    }

    public string GenerateToken(User user, IEnumerable<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Name)
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
