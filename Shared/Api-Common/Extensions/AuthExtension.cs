using ApiCommon.Configures;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ApiCommon.Extensions;

public static class AuthExtension
{
    /// <summary>
    /// This method sets up JWT Bearer authentication for the application. 
    /// It configures the authentication scheme to use JWT tokens and 
    /// specifies how the tokens should be validated using the JwtBearerOptionsSetup class.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddAppAuthentication(this IServiceCollection services)
    {
        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer();

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddAuthorization();

        return services;
    }
}