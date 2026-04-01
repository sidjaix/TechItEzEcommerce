using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCommon.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        // "NameIdentifier" maps to the "sub" (subject) claim in standard JWTs
        var userIdString = user.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
        {
            throw new UnauthorizedAccessException("User ID claim is missing or invalid.");
        }

        return userId;
    }
}