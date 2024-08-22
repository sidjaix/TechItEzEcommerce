using ApiServices.Models.Cart;
using ApiServices.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_Commerce.Controllers;

public class WishlistController(IWishlistService wishlistService, ITokenProvider tokenProvider) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        var wishlist = await wishlistService.GetWishlistItemsAsync(userId);
        return View(wishlist);
    }

    [HttpGet]
    public async Task<ActionResult> AddAndRemoveWishlistItem(int productId)
    {
        _ = await wishlistService.AddAndRemoveWishlistItemAsync(new AddToWishlistViewModel
        {
            ProductId = productId,
            UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
        });
        await UpdatewishlistItemCookie();
        return RedirectToAction("Index", "Product");
    }

    [HttpGet]
    public async Task<IActionResult> RemoveItemFromWishlist(int wishlistItemId)
    {
        var cart = await wishlistService.RemoveItemFromWishlist(wishlistItemId);
        await UpdatewishlistItemCookie();
        return RedirectToAction(nameof(Index));
    }

    private async Task UpdatewishlistItemCookie()
    {
        var wishlist = await wishlistService.GetWishlistItemsAsync(User.FindFirstValue(JwtRegisteredClaimNames.Sub));
        var itemCount = wishlist.WishlistItems.Count;
        tokenProvider.SetWishlistItemsCount(itemCount.ToString());
    }
}
