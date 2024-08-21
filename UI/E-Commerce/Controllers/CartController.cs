using ApiServices.Models.Cart;
using ApiServices.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    public class CartController(ICartService cartService) : Controller
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
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> DecreaseCartItem(int cartItemId)
        {
            var cart = await cartService.DecreaseCartItem(cartItemId);
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveItemFromCart(int cartItemId)
        {
            var cart = await cartService.RemoveItemFromCart(cartItemId);
            return RedirectToAction(nameof(Index));
        }
    }
}
