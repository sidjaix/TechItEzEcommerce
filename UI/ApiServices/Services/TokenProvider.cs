using ApiServices.Services.IService;
using ApiServices.Utility;
using Microsoft.AspNetCore.Http;

namespace ApiServices.Services;

public class TokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public TokenProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public void ClearToken()
    {
        _contextAccessor.HttpContext?.Response.Cookies.Delete(ApplicationData.JwtTokenCookie);
    }

    public string GetToken()
    {
        string token = null;
        bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(ApplicationData.JwtTokenCookie, out token);
        return hasToken is true ? token : null;
    }

    public void SetToken(string jwtToken)
    {
        _contextAccessor.HttpContext?.Response.Cookies.Append(ApplicationData.JwtTokenCookie, jwtToken);
    }
}