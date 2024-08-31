using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Models;
using ApiServices.Services.IService;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using E_Commerce.Utility;

namespace E_Commerce.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService productService;
    private readonly ICategoryService categoryService;
    private readonly ICartService _cartService;
    private readonly IWishlistService _wishlistService;
    private readonly ITokenProvider _tokenProvider;

    public HomeController(ILogger<HomeController> logger,
    IProductService productService,
    ICategoryService categoryService,
    ICartService cartService,
    IWishlistService wishlistService,
    ITokenProvider tokenProvider
    )
    {
        _logger = logger;
        this.productService = productService;
        this.categoryService = categoryService;
        _cartService = cartService;
        _wishlistService = wishlistService;
        _tokenProvider = tokenProvider;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (TempData["RedirectedFromAccount"] is not null && (bool)TempData["RedirectedFromAccount"])
        {
            await SetCartDetailInCookie();
        }
        return View();
    }

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    private async Task SetCartDetailInCookie()
    {
        var cart = await _cartService.GetCartItemsAsync(User.FindFirstValue(JwtRegisteredClaimNames.Sub));
        var itemCount = cart.CartItems.Count;
        var itemSum = cart.CartItems.Sum(x => x.ItemTotal);
        var cartDetail = $"{itemCount},{itemSum}";
        _tokenProvider.SetCartItemsCountAndTotalPrice(cartDetail);

        var wishlist = await _wishlistService.GetWishlistItemsAsync(User.FindFirstValue(JwtRegisteredClaimNames.Sub));
        var wishlistItemCount = wishlist.WishlistItems.Count;
        _tokenProvider.SetWishlistItemsCount(wishlistItemCount.ToString());
    }
}
