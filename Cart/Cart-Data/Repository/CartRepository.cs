using Cart_Core;
using Cart_Core.Models;
using Cart_Data.Services.IServices;
using Cart_Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Cart_Core.Entities;

namespace Cart_Data.Repository;

public class CartRepository(CartDbContext db, IProductService productService) : ICartRepository
{
    public async Task<CartModel> GetUserCartItems(string UserId)
    {
        CartModel cart = new();
        var products = await productService.GetProductsAsync();
        var cartItemsQuery = from c in db.Carts
                             join ci in db.CartItems on c.CartId equals ci.CartId
                             where c.UserId == UserId
                             select new CartItemModel
                             {
                                 CartId = ci.CartId,
                                 CartItemId = ci.CartItemId,
                                 ProductId = ci.ProductId,
                                 Quantity = ci.Quantity,
                             };
        var cartItems = await cartItemsQuery.AsNoTracking().ToListAsync();

        cartItems = (from p in products
                     join ci in cartItems on p.ProductId equals ci.ProductId
                     select new CartItemModel
                     {
                         CartId = ci.CartId,
                         CartItemId = ci.CartItemId,
                         ProductId = ci.ProductId,
                         ProductName = p.ProductName,
                         SellingPrice = p.SellingPrice,
                         ImageUrl = p.ProductImages.FirstOrDefault(x => x.IsThumbnail)?.ProductImageUrl,
                         Quantity = ci.Quantity,
                     }).ToList();

        cart.UserId = UserId;
        cart.CartItems = cartItems;
        return cart;
    }

    public async Task<AddToCartModel> AddToCart(AddToCartModel addToCartModel)
    {
        var cart = await db.Carts.Include(x => x.CartItems)
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.UserId == addToCartModel.UserId);

        if (cart is null)
        {
            // Handle first-time cart creation
            cart = new Cart
            {
                UserId = addToCartModel.UserId
            };

            db.Carts.Attach(cart);

            cart.CartItems.Add(new CartItem
            {
                ProductId = addToCartModel.ProductId,
                Quantity = addToCartModel.Quantity,
                Cart = cart
            });
            await db.SaveChangesAsync();
            addToCartModel.CartId = cart.CartId;
        }
        else
        {
            var cartItem = cart.CartItems
            .FirstOrDefault(x =>
            x.ProductId == addToCartModel.ProductId &&
            x.CartId == cart.CartId);

            if (cartItem is null)
            {
                // Handle adding a new product to the cart
                cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = addToCartModel.ProductId,
                    Quantity = addToCartModel.Quantity,
                };
                db.CartItems.Add(cartItem);
                await db.SaveChangesAsync();
                addToCartModel.CartId = cart.CartId;
            }
            else
            {
                // Handle updating the quantity of an existing product
                cartItem.Quantity += addToCartModel.Quantity;
                db.CartItems.Update(cartItem);
                await db.SaveChangesAsync();
            }
            //addToCartModel.CartItem = (CartItemModel)cartItem.MapToDto();
        }
        return addToCartModel;
    }

    public async Task<bool> DecreaseCartItem(int cartItemId)
    {
        CartItem cartItem = await db.CartItems.FirstOrDefaultAsync(u => u.CartItemId == cartItemId);
        if (cartItem is not null)
        {
            int numberOfItems = await db.CartItems.Where(u => u.CartId == cartItem.CartId).CountAsync();

            if (cartItem.Quantity == 1)
            {
                await db.CartItems
                .Where(x => x.CartItemId == cartItemId)
                .ExecuteDeleteAsync();
                if (numberOfItems == 1)
                {
                    await db.Carts
                    .Where(u => u.CartId == cartItem.CartId)
                    .ExecuteDeleteAsync();
                }
            }
            else
            {
                cartItem.Quantity -= 1;
                db.CartItems.Update(cartItem);
                var numberOfRowAffected = await db.SaveChangesAsync();
                var isItemCountUpdated = numberOfRowAffected > 0;
                return isItemCountUpdated;
            }
        }
        return false;
    }

    public async Task<bool> RemoveItemFromCart(int cartItemId)
    {
        bool isItemDeleted = false;
        CartItem cartItem = await db.CartItems.FirstOrDefaultAsync(u => u.CartItemId == cartItemId);
        if (cartItem is not null)
        {
            int numberOfItems = await db.CartItems.Where(u => u.CartId == cartItem.CartId).CountAsync();

            isItemDeleted = await db.CartItems
            .Where(x => x.CartItemId == cartItemId)
            .ExecuteDeleteAsync() > 0;
            if (numberOfItems == 1)
            {
                await db.Carts
                .Where(u => u.CartId == cartItem.CartId)
                .ExecuteDeleteAsync();
            }
        }
        return isItemDeleted;
    }


}
