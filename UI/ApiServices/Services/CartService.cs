using ApiServices.Models;
using ApiServices.Models.Cart;
using ApiServices.Services.IService;
using ApiServices.Utility;
using ApiServices.Utility.Enums;
using Newtonsoft.Json;

namespace ApiServices.Services;

public class CartService(IBaseService baseService) : ICartService
{
    public async Task<CartViewModel> GetCartItemsAsync(string userId)
    {
        var cart = new CartViewModel();
        var request = new RequestDto
        {
            Url = $"{ApplicationData.CartApiBaseAddress}/api/cart/GetUserCartItems/{userId}",
            ApiMethod = ApiMethod.GET
        };
        var response = await baseService.SendAsync(request);
        if (response is not null && response.IsSuccess)
        {
            cart = JsonConvert.DeserializeObject<CartViewModel>(Convert.ToString(response.Result));
        }
        return cart;
    }

    public async Task<AddToCartViewModel> AddToCartAsync(AddToCartViewModel addToCart)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.CartApiBaseAddress}/api/cart/AddToCart",
            ApiMethod = ApiMethod.POST,
            Data = addToCart,
            ContentType = ContentType.Json
        };
        var response = await baseService.SendAsync(request);
        if (response is not null && response.IsSuccess)
        {
            addToCart = JsonConvert.DeserializeObject<AddToCartViewModel>(Convert.ToString(response.Result));
        }
        return addToCart;
    }

    public async Task<bool> DecreaseCartItem(int productItemId)
    {
        bool isItemUpdated = false;
        var request = new RequestDto
        {
            Url = $"{ApplicationData.CartApiBaseAddress}/api/cart/DecreaseCartItem/{productItemId}",
            ApiMethod = ApiMethod.PUT
        };
        var response = await baseService.SendAsync(request);
        if (response is not null && response.IsSuccess)
        {
            isItemUpdated = (bool)response.Result;
        }
        return isItemUpdated;
    }

    public async Task<bool> RemoveItemFromCart(int productItemId)
    {
        bool isItemRemoved = false;
        var request = new RequestDto
        {
            Url = $"{ApplicationData.CartApiBaseAddress}/api/cart/RemoveItemFromCart/{productItemId}",
            ApiMethod = ApiMethod.DELETE
        };
        var response = await baseService.SendAsync(request);
        if (response is not null && response.IsSuccess)
        {
            isItemRemoved = (bool)response.Result;
        }
        return isItemRemoved;
    }
}
