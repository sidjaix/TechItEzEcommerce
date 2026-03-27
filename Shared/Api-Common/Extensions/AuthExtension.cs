using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api_Common.Extensions;

public static class AuthExtension
{
    public static IServiceCollection AddAppAuthetication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfigs = configuration.GetSection("JWT");
        var secret = jwtConfigs.GetValue<string>("Secret");
        var issuer = jwtConfigs.GetValue<string>("Issuer");
        var audience = jwtConfigs.GetValue<string>("Audience");
        var key = Encoding.ASCII.GetBytes(secret);

        services.AddAuthentication(x =>
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

        return services;
    }
}