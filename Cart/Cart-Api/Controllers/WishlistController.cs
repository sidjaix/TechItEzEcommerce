using Cart_Core.Models;
using Cart_Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Cart_Api.Controllers;

[Route("api/wishlist")]
[ApiController]
[Authorize]
public class WishlistController(IWishlistRepository wishlistRepository, ResponseDto response) : ControllerBase
{
    /// <summary>
    /// Get user's wishlist items
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// 
    [HttpGet("GetUserWishlistItems/{userId}")]
    public async Task<IActionResult> GetUserWishlistItems(string userId)
    {
        var wishlist = await wishlistRepository.GetUserWishlistItems(userId);
        response.Result = wishlist;
        response.Message = wishlist.WishlistItems.Count > 0 ? "" : "Empty wishlist";
        return Ok(response);
    }

    /// <summary>
    /// Add product to wishlist
    /// </summary>
    /// <param name="addToWishlist"></param>
    /// <returns></returns>
    /// 
    [HttpPost("AddAndRemoveWishlistItem")]
    public async Task<IActionResult> AddAndRemoveWishlistItem([FromBody] AddToWishlistModel addToWishlist)
    {
        try
        {
            addToWishlist = await wishlistRepository.AddAndRemoveWishlistItem(addToWishlist);
            response.Message = "Added to wishlist";
            response.Result = addToWishlist;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.IsSuccess = false;
        }
        return Ok(response);
    }

    /// <summary>
    /// Remove item from wishlist
    /// </summary>
    /// <param name="wishlistItemId"></param>
    /// <returns></returns>
    [HttpDelete("RemoveItemFromWishlist/{wishlistItemId}")]
    public async Task<IActionResult> RemoveItemFromWishlist(int wishlistItemId)
    {
        try
        {
            var isRemoved = await wishlistRepository.RemoveItemFromWishlist(wishlistItemId);
            response.Message = isRemoved ? "Item removed from wishlist" : string.Empty;
            response.Result = isRemoved;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.IsSuccess = false;
        }
        return Ok(response);
    }
}
