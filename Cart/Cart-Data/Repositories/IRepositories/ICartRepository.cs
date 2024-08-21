using Cart_Core.Models;

namespace Cart_Data.Repositories.IRepositories;

public interface ICartRepository
{
    Task<CartModel> GetUserCartItems(string UserId);
    Task<AddToCartModel> AddToCart(AddToCartModel addToCartModel);
    Task<bool> DecreaseCartItem(int cartItemId);
    Task<bool> RemoveItemFromCart(int cartItemId);
}
