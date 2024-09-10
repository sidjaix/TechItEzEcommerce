using Cart_Core;
using Cart_Core.Entities;
using Cart_Core.Models;
using Cart_Data.Repository.IRepository;
using Cart_Data.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Cart_Data.Repository;

public class WishlistRepository(CartDbContext db, IProductService productService) : IWishlistRepository
{
    public async Task<WishlistModel> GetUserWishlistItems(string UserId)
    {
        WishlistModel wishlist = new();
        var products = await productService.GetProductsAsync();
        var wishlistItemsQuery = from c in db.Wishlists
                                 join ci in db.WishlistItems on c.WishlistId equals ci.WishlistId
                                 where c.UserId == UserId
                                 select new WishlistItemModel
                                 {
                                     WishlistId = ci.WishlistId,
                                     WishlistItemId = ci.WishlistItemId,
                                     ProductId = ci.ProductId
                                 };
        var wishlistItems = await wishlistItemsQuery.AsNoTracking().ToListAsync();

        wishlistItems = (from p in products
                         join ci in wishlistItems on p.ProductId equals ci.ProductId
                         select new WishlistItemModel
                         {
                             WishlistId = ci.WishlistId,
                             WishlistItemId = ci.WishlistItemId,
                             ProductId = ci.ProductId,
                             ProductName = p.ProductName,
                             SellingPrice = p.SellingPrice,
                             ImageUrl = p.ImageUrl
                         }).ToList();

        wishlist.UserId = UserId;
        wishlist.WishlistItems = wishlistItems;
        return wishlist;
    }
    public async Task<AddToWishlistModel> AddAndRemoveWishlistItem(AddToWishlistModel addToWishlist)
    {
        var wishlist = await db.Wishlists.Include(x => x.WishlistItems)
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.UserId == addToWishlist.UserId);

        if (wishlist is null)
        {
            // Handle first-time wishlist creation
            wishlist = new Wishlist
            {
                UserId = addToWishlist.UserId
            };

            db.Wishlists.Attach(wishlist);

            wishlist.WishlistItems.Add(new WishlistItem
            {
                ProductId = addToWishlist.ProductId,
                Wishlist = wishlist
            });
            await db.SaveChangesAsync();
            addToWishlist.WishlistId = wishlist.WishlistId;
        }
        else
        {
            var wishlistItem = wishlist.WishlistItems
            .FirstOrDefault(x =>
            x.ProductId == addToWishlist.ProductId &&
            x.WishlistId == wishlist.WishlistId);

            if (wishlistItem is null)
            {
                // Handle adding a new product to the wishlist
                wishlistItem = new WishlistItem
                {
                    WishlistId = wishlist.WishlistId,
                    ProductId = addToWishlist.ProductId
                };
                db.WishlistItems.Add(wishlistItem);
                await db.SaveChangesAsync();
                addToWishlist.WishlistId = wishlist.WishlistId;
            }
            else
            {

                db.WishlistItems
                .Where(x => x.ProductId == wishlistItem.ProductId)
                .ExecuteDelete();

                if (wishlist.WishlistItems.Count == 1)
                {
                    await db.Wishlists
                    .Where(u => u.WishlistId == wishlistItem.WishlistId)
                    .ExecuteDeleteAsync();
                }
            }
        }
        return addToWishlist;
    }
    public async Task<bool> RemoveItemFromWishlist(int WishlistItemId)
    {
        bool isItemDeleted = false;
        WishlistItem wishlistItem = await db.WishlistItems.FirstOrDefaultAsync(u => u.WishlistItemId == WishlistItemId);
        if (wishlistItem is not null)
        {
            int numberOfItems = await db.WishlistItems.Where(u => u.WishlistId == wishlistItem.WishlistId).CountAsync();

            isItemDeleted = await db.WishlistItems
            .Where(x => x.WishlistItemId == WishlistItemId)
            .ExecuteDeleteAsync() > 0;
            if (numberOfItems == 1)
            {
                await db.Wishlists
                .Where(u => u.WishlistId == wishlistItem.WishlistId)
                .ExecuteDeleteAsync();
            }
        }
        return isItemDeleted;
    }
}
