using ApiServices.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace E_Commerce.Utility;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _jwtSecretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public TokenValidationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _jwtSecretKey = configuration["JWT:Secret"];
        _audience = configuration["JWT:Audience"];
        _issuer = configuration["JWT:Issuer"];
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Cookies[ApplicationData.JwtTokenCookie];
        if (context.Request.Path.StartsWithSegments("/") || context.Request.Path.StartsWithSegments("/Account/Login") || context.Request.Path.StartsWithSegments("/Account/Register"))
        {
            await _next(context);
            return;
        }
        if (!string.IsNullOrEmpty(token))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateLifetime = true, // Validate the token expiry
                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    ValidateAudience = true,
                    ValidAudience = _audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero // No tolerance on the token expiration

                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    context.Response.Cookies.Delete(ApplicationData.JwtTokenCookie);
                    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    context.Response.Redirect("/Account/Login");
                    return;
                }
            }
            catch
            {
                context.Response.Cookies.Delete(ApplicationData.JwtTokenCookie);
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                context.Response.Redirect("/Account/Login");
                return;
            }
        }

        await _next(context);
    }
}
