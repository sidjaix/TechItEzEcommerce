using Cart_Core.Models;

namespace Cart_Data.Repository.IRepository;

public interface ICartRepository
{
    Task<CartModel> GetUserCartItems(string UserId);
    Task<AddToCartModel> AddToCart(AddToCartModel addToCartModel);
    Task<bool> DecreaseCartItem(int cartItemId);
    Task<bool> RemoveItemFromCart(int cartItemId);
}
