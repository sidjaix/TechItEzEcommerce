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
    public class OrderController(IMediator mediator) : ControllerBase
    {
        // Frontend only sends the Address now!
        public record CreateOrderRequest(AddressDto ShippingAddress);

        /// <summary>
        /// Retrieves the complete order history for a specific customer.
        /// </summary>
        [HttpGet("{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetCustomerOrders(Guid customerId)
        {
            var orders = await mediator.Send(new GetCustomerOrdersQuery(customerId));

            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found for this customer.");
            }

            return Ok(orders);
        }

        /// <summary>
        /// Creates a new immutable order record.
        /// </summary>
        [HttpPost]
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
