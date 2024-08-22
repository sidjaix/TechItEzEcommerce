using ApiServices.Models.Cart;

namespace ApiServices.Services.IService;

public interface IWishlistService
{
    Task<WishlistViewModel> GetWishlistItemsAsync(string userId);
    Task<AddToWishlistViewModel> AddAndRemoveWishlistItemAsync(AddToWishlistViewModel addToCart);
    Task<bool> RemoveItemFromWishlist(int wishlistItemId);
}
