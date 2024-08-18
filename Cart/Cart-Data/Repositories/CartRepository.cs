using Cart_Core;
using Cart_Core.Models;
using Cart_Data.Services.IServices;
using Cart_Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Cart_Core.Entities;
using System.Threading.Tasks.Dataflow;
using Cart_Core.Mapper;
using System.Security.Cryptography;

namespace Cart_Data.Repositories;

public class CartRepository(CartDbContext db, IProductService productService) : ICartRepository
{
    public async Task<CartModel> GetUserCartItems(string UserId)
    {
        var products = await productService.GetProductsAsync();
        var cartItemsQuery = from c in db.Carts
                             join ci in db.CartItems on c.CartId equals ci.CartId
                             where c.UserId == UserId
                             select new CartItemModel
                             {
                                 CartId = c.CartId,
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
                         ImageUrl = p.ImageUrl,
                         Quantity = ci.Quantity,
                     }).ToList();

        var cart = new CartModel
        {
            UserId = UserId,
            CartItems = cartItems
        };
        return cart;
    }

    public async Task<CartModel> AddToCart(CartModel cartData)
    {
        var cartDetail = await db.Carts.Include(x => x.CartItems)
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.UserId == cartData.UserId);

        if (cartDetail is null)
        {
            var cart = cartData.MapToEntity();
            // Handle first-time cart creation
            db.Carts.Attach(cart);
            cart.CartItems.Add(new CartItem
            {
                ProductId = cartData.CartItem.ProductId,
                Quantity = cartData.CartItem.Quantity,
                Cart = cart
            });
            await db.SaveChangesAsync();
        }
        else
        {
            // var cartItem = (from ci in db.CartItems
            //                 where ci.ProductId == cartDetail.CartItem.ProductId
            //                 && ci.CartId == cartDetail.CartId
            //                 select new CartItem
            //                 {
            //                     CartId = ci.CartId,
            //                     CartItemId = ci.CartItemId,
            //                     ProductId = ci.ProductId,
            //                     Quantity = ci.Quantity
            //                 }).FirstOrDefaultAsync();

            var cartItem = cartDetail.CartItems
            .FirstOrDefault(x =>
            x.ProductId == cartData.CartItem.ProductId &&
            x.CartId == cartDetail.CartId);

            if (cartItem is null)
            {
                // Handle adding a new product to the cart
                cartItem = new CartItem
                {
                    CartId = cartDetail.CartId,
                    ProductId = cartData.CartItem.ProductId,
                    Quantity = cartData.CartItem.Quantity,
                };
                db.CartItems.Add(cartItem);
                await db.SaveChangesAsync();
            }
            else
            {
                // Handle updating the quantity of an existing product
                cartItem.Quantity += cartData.CartItem.Quantity;
                db.CartItems.Update(cartItem);
                await db.SaveChangesAsync();
            }
        }
        return cartData;
    }
}
