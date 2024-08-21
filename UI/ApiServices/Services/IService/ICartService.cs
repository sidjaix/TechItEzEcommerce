using ApiServices.Models.Cart;

namespace ApiServices.Services.IService;

public interface ICartService
{
    Task<CartViewModel> GetCartItemsAsync(string userId);
    Task<AddToCartViewModel> AddToCartAsync(AddToCartViewModel addToCart);
    Task<bool> DecreaseCartItem(int productItemId);
    Task<bool> RemoveItemFromCart(int productItemId);
}
