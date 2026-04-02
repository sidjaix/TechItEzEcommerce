using ApiCommon.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderApplication.DTOs;
using OrderApplication.Queries;

namespace OrderApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderController(IMediator mediator) : ControllerBase
    {
        // Frontend only sends the Address now!
        public record CreateOrderRequest(AddressDto ShippingAddress);

        /// <summary>
        /// Retrieves all orders for the currently logged-in customer.
        /// </summary>
        [HttpGet("GetCustomerOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetCustomerOrders()
        {
            var customerId = User.GetUserId(); // Ensure the user is authenticated and get their ID
            var orders = await mediator.Send(new GetCustomerOrdersQuery(customerId));

            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found for this customer.");
            }

            return Ok(orders);
        }

        /// <summary>
        /// Creates a new order for the logged-in customer based on their active cart and provided shipping address.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var customerId = User.GetUserId();

            // Assemble the command
            var command = new CreateOrderCommand(customerId, request.ShippingAddress);

            var orderId = await mediator.Send(command);

            return CreatedAtAction(nameof(GetCustomerOrders), new { customerId = command.CustomerId }, orderId);
        }
    }
}
