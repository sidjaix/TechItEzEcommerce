using App_Contracts.Order;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order_Core.Models;
using Order_Data.Repository.IRepository;
using System.Security.Claims;

namespace Order_Api.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderController(IOrderRepository orderRepository, ResponseDto response, IPublishEndpoint publishEndpoint) : ControllerBase
    {
        /// <summary>
        /// Get user's all orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await orderRepository.GetOrdersAsync(userId);
            response.Result = orders;
            return Ok(response);
        }

        /// <summary>
        /// Get Order in details
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("GetOrder/{orderId}")]
        public async Task<IActionResult> GetOrderAsync(int orderId)
        {
            var order = await orderRepository.GetOrderAsync(orderId);
            response.Result = order;
            return Ok(response);
        }

        /// <summary>
        /// Place Your Order
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        /// 
        [HttpPost("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(OrderCreateEvent newOrder)
        {
            await publishEndpoint.Publish(newOrder);
            response.Message = "Order has been placed !!";
            return Ok(response);
        }


    }
}
