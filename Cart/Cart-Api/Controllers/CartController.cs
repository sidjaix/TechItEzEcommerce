using Cart_Core.Models;
using Cart_Data.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cart_Api.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [Authorize]
    public class CartController(ICartRepository cartRepository, ResponseDto response) : ControllerBase
    {
        /// <summary>
        /// Get user's cart items
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// 
        [HttpGet("GetUserCartItems/{userId}")]
        public async Task<IActionResult> GetUserCartItems(string userId)
        {
            var cart = await cartRepository.GetUserCartItems(userId);
            response.Result = cart;
            response.Message = cart.CartItems.Count > 0 ? "" : "Empty Cart";
            return Ok(response);
        }

        /// <summary>
        /// Add product to cart
        /// </summary>
        /// <param name="cartDetail"></param>
        /// <returns></returns>
        /// 
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartModel cartDetail)
        {
            try
            {
                cartDetail = await cartRepository.AddToCart(cartDetail);
                response.Message = "Added to cart";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }
            return Ok(response);
        }

        /// <summary>
        /// Decrese item count from cart
        /// </summary>
        /// <param name="cartItemId"></param>
        /// <returns></returns>
        [HttpPut("DecreaseCartItem/{cartItemId}")]
        public async Task<IActionResult> DecreaseCartItem(int cartItemId)
        {
            try
            {
                var isItemCountDecreased = await cartRepository.DecreaseCartItem(cartItemId);
                response.Message = isItemCountDecreased ? "Item updated" : string.Empty;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
            }
            return Ok(response);
        }

        /// <summary>
        /// Remove item from cart
        /// </summary>
        /// <param name="cartItemId"></param>
        /// <returns></returns>
        [HttpDelete("RemoveItemFromCart/{cartItemId}")]
        public async Task<IActionResult> RemoveItemFromCart(int cartItemId)
        {
            try
            {
                var isRemoved = await cartRepository.RemoveItemFromCart(cartItemId);
                response.Message = isRemoved ? "Item removed from cart" : string.Empty;
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
}
