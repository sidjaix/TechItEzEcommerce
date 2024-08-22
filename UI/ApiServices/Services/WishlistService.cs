using ApiServices.Models;
using ApiServices.Models.Cart;
using ApiServices.Services.IService;
using ApiServices.Utility;
using ApiServices.Utility.Enums;
using Newtonsoft.Json;
using System;

namespace ApiServices.Services;

public class WishlistService(IBaseService baseService) : IWishlistService
{

    public async Task<WishlistViewModel> GetWishlistItemsAsync(string userId)
    {
        var wishlist = new WishlistViewModel();
        var request = new RequestDto
        {
            Url = $"{ApplicationData.CartApiBaseAddress}/api/wishlist/GetUserWishlistItems/{userId}",
            ApiMethod = ApiMethod.GET
        };
        var response = await baseService.SendAsync(request);
        if (response is not null && response.IsSuccess)
        {
            wishlist = JsonConvert.DeserializeObject<WishlistViewModel>(Convert.ToString(response.Result));
        }
        return wishlist;
    }

    public async Task<AddToWishlistViewModel> AddAndRemoveWishlistItemAsync(AddToWishlistViewModel addToWishlist)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.CartApiBaseAddress}/api/wishlist/AddAndRemoveWishlistItem",
            ApiMethod = ApiMethod.POST,
            Data = addToWishlist,
            ContentType = ContentType.Json
        };
        var response = await baseService.SendAsync(request);
        if (response is not null && response.IsSuccess)
        {
            addToWishlist = JsonConvert.DeserializeObject<AddToWishlistViewModel>(Convert.ToString(response.Result));
        }
        return addToWishlist;
    }

    public async Task<bool> RemoveItemFromWishlist(int wishlistItemId)
    {
        bool isItemRemoved = false;
        var request = new RequestDto
        {
            Url = $"{ApplicationData.CartApiBaseAddress}/api/wishlist/RemoveItemFromWishlist/{wishlistItemId}",
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
