using Cart_Core.Entities;
using Cart_Core.Models;

namespace Cart_Core.Mapper;

public static class CartMapper
{
    public static Cart MapToEntity(this CartModel cartModel)
    {
        return new Cart
        {
            CartId = cartModel.CartId,
            UserId = cartModel.UserId
            // CartItems = cartModel.CartItems.Select(x => new CartItem
            // {
            //     CartId = x.CartId,
            //     ProductId = x.ProductId,
            //     Quantity = x.Quantity,
            //     CartItemId = x.CartItemId
            // }).ToList()
        };
    }

    public static CartModel MapToDto(this Cart cart)
    {
        return new CartModel
        {
            CartId = cart.CartId,
            UserId = cart.UserId,
            CartItems = cart.CartItems.Select(x => new CartItemModel
            {
                CartId = x.CartId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                CartItemId = x.CartItemId
            }).ToList()
        };
    }
}
