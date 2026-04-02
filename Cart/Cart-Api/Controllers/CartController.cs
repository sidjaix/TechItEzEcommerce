using ApiCommon.Extensions;
using CartApplication.Commands;
using CartApplication.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        // Frontend only sends the Address now!
        public record AddItemRequest(Guid VariantId, string ProductName, decimal UnitPrice, int Quantity);
        public CartController(IMediator mediator) => _mediator = mediator;

        // In a real app, CustomerId comes from the JWT token (User.Identity). 
        // For this POC, we accept it in the route/body for easy Swagger testing.

        /// <summary>
        /// Retrieves the active cart for the currently logged-in customer.
        /// </summary>
        /// <returns>CartDto</returns>
        [HttpGet("GetCart")]
        public async Task<ActionResult<CartDto>> GetCart()
        {
            var customerId = User.GetUserId();
            return Ok(await _mediator.Send(new GetCartQuery(customerId)));
        }

        /// <summary>
        /// Adds an item to the customer's cart. 
        /// The request body should contain the customer ID, product ID, and quantity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem(AddItemRequest request)
        {
            var customerId = User.GetUserId();
            var command = new AddItemToCartCommand(customerId, request.VariantId, request.ProductName, request.UnitPrice, request.Quantity);
            var result = await _mediator.Send(command);
            return result ? Ok() : BadRequest();
        }

        /// <summary>
        /// Clears the customer's cart by removing all items.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("ClearCart")]
        public async Task<IActionResult> ClearCart()
        {
            var customerId = User.GetUserId();
            var result = await _mediator.Send(new ClearCartCommand(customerId));
            return result ? Ok() : NotFound();
        }
    }
}
