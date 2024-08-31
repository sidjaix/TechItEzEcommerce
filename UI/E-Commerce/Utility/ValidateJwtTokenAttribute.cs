using ApiServices.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace E_Commerce.Utility;

public class ValidateJwtTokenAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Retrieve IConfiguration using the IServiceProvider
        var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

        var jwtSecretKey = configuration["JWT:Secret"];
        var issuer = configuration["JWT:Issuer"];
        var audience = configuration["JWT:Audience"];
        var token = context.HttpContext.Request.Cookies[ApplicationData.JwtTokenCookie];

        var tokenHandler = new JwtSecurityTokenHandler();

        if (string.IsNullOrEmpty(token))
        {
            // If there's no token, redirect to login
            context.Result = new RedirectToActionResult("Login", "Account", null);
            return;
        }

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateLifetime = true, // Validate the token expiry
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero // No tolerance on the token expiration

            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                context.HttpContext.Response.Cookies.Delete(ApplicationData.JwtTokenCookie);
                context.HttpContext.Response.Redirect("/Account/Login");
                return;
            }
        }
        catch
        {
            context.HttpContext.Response.Cookies.Delete(ApplicationData.JwtTokenCookie);
            context.HttpContext.Response.Redirect("/Account/Login");
            return;
        }
    }
}
