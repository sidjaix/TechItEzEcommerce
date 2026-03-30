using ApiCommon.Configures;
using ApiCommon.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiCommon.Extensions;

public static class AuthExtension
{
    /// <summary>
    /// This method sets up JWT Bearer authentication for the application. 
    /// It configures the authentication scheme to use JWT tokens and 
    /// specifies how the tokens should be validated using the JwtBearerOptionsSetup class.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns>WebApplicationBuilder</returns>
    public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration.GetSection("JWT");
        var secret = jwtSettings.GetValue<string>("Secret");
        var issuer = jwtSettings.GetValue<string>("Issuer");
        var audience = jwtSettings.GetValue<string>("Audience");
        var key = Encoding.ASCII.GetBytes(secret);
        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                ValidateAudience = true
            };
        });

        builder.Services.AddAuthorization();

        return builder; ;
    }
}