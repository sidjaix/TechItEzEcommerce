using Cart_Core.Models;

namespace Cart_Data.Repositories.IRepositories;

public interface ICartRepository
{
    Task<CartModel> GetUserCartItems(string UserId);
    Task<CartModel> AddToCart(CartModel cartDetail);
}
