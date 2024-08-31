using ApiServices.Models.Cart;
using ApiServices.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class CartController(ICartService cartService, ITokenProvider tokenProvider) : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var cart = await cartService.GetCartItemsAsync(userId);
            return View(cart);
        }

        [HttpGet]
        public async Task<ActionResult> AddToCart(int productId, int quantity)
        {
            _ = await cartService.AddToCartAsync(new AddToCartViewModel
            {
                ProductId = productId,
                Quantity = quantity,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            });
            await UpdateCartDetailCookie();
            TempData["success"] = "Item added to cart";
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> DecreaseCartItem(int cartItemId)
        {
            var cart = await cartService.DecreaseCartItem(cartItemId);
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            await UpdateCartDetailCookie();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveItemFromCart(int cartItemId)
        {
            var cart = await cartService.RemoveItemFromCart(cartItemId);
            await UpdateCartDetailCookie();
            return RedirectToAction(nameof(Index));
        }

        private async Task UpdateCartDetailCookie()
        {
            var cart = await cartService.GetCartItemsAsync(User.FindFirstValue(JwtRegisteredClaimNames.Sub));
            var itemCount = cart.CartItems.Count;
            var itemSum = cart.CartItems.Sum(x => x.ItemTotal);
            var cartDetail = $"{itemCount},{itemSum}";
            tokenProvider.SetCartItemsCountAndTotalPrice(cartDetail);
        }
    }
}
