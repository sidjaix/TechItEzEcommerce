using Cart_Core.Models;

namespace Cart_Data.Repository.IRepository;

public interface IWishlistRepository
{
    Task<WishlistModel> GetUserWishlistItems(string UserId);
    Task<AddToWishlistModel> AddAndRemoveWishlistItem(AddToWishlistModel addToWishlist);
    Task<bool> RemoveItemFromWishlist(int wishlistItemId);
}
