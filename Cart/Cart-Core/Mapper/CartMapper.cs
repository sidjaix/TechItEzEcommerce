using Cart_Core.Entities;
using Cart_Core.Models;

namespace Cart_Core.Mapper;

public static class CartMapper
{
    public static object MapToEntity(this object obj)
    {
        if (obj is CartModel)
        {
            var cartModel = (CartModel)obj;
            return new Cart
            {
                CartId = cartModel.CartId,
                UserId = cartModel.UserId
            };
        }
        var cartItem = (CartItemModel)obj;
        return new CartItem
        {
            CartItemId = cartItem.CartItemId,
            CartId = cartItem.CartId,
            ProductId = cartItem.ProductId,
            Quantity = cartItem.Quantity
        };
    }

    public static object MapToDto(this object obj)
    {
        if (obj is Cart)
        {
            var cartModel = (Cart)obj;
            return new CartModel
            {
                CartId = cartModel.CartId,
                UserId = cartModel.UserId
            };
        }
        var cartItem = (CartItem)obj;
        return new CartItemModel
        {
            CartItemId = cartItem.CartItemId,
            CartId = cartItem.CartId,
            ProductId = cartItem.ProductId,
            Quantity = cartItem.Quantity
        };
    }
}
