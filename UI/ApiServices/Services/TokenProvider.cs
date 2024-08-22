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
        _contextAccessor.HttpContext?.Response.Cookies.Delete(ApplicationData.CartDetail);
        _contextAccessor.HttpContext?.Response.Cookies.Delete(ApplicationData.WishlistItemCount);
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

    public string GetCartItemsCountAndTotalPrice()
    {
        var cartDetail = string.Empty;
        bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(ApplicationData.CartDetail, out cartDetail);
        return hasToken is true ? cartDetail : null;
    }

    public void SetCartItemsCountAndTotalPrice(string cartDetail)
    {
        _contextAccessor.HttpContext?.Response.Cookies.Append(ApplicationData.CartDetail, cartDetail);
    }

    public string GetWishlistItemsCount()
    {
        var itemCount = string.Empty;
        bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(ApplicationData.WishlistItemCount, out itemCount);
        return hasToken is true ? itemCount : null;
    }

    public void SetWishlistItemsCount(string itemCount)
    {
        _contextAccessor.HttpContext?.Response.Cookies.Append(ApplicationData.WishlistItemCount, itemCount);
    }
}