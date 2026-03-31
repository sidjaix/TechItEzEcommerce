using CartApplication.Commands;
using CartApplication.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CartApi.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Produces("application/json")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartController(IMediator mediator) => _mediator = mediator;

        // In a real app, CustomerId comes from the JWT token (User.Identity). 
        // For this POC, we accept it in the route/body for easy Swagger testing.

        /// <summary>
        /// Retrieves the cart for a given customer. In a real application, 
        /// the customer ID would typically be extracted from the authenticated user's JWT token rather than passed as a route parameter. 
        /// This endpoint returns the current state of the customer's cart, including all items and their details. 
        /// If the cart does not exist, it may return an empty cart or a not found response based on implementation.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        public async Task<ActionResult<CartDto>> GetCart(Guid customerId)
            => Ok(await _mediator.Send(new GetCartQuery(customerId)));

        /// <summary>
        /// Adds an item to the customer's cart. 
        /// The request body should contain the customer ID, product ID, and quantity.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("items")]
        public async Task<IActionResult> AddItem(AddItemToCartCommand command)
        {
            var result = await _mediator.Send(command);
            return result ? Ok() : BadRequest();
        }

        /// <summary>
        /// Removes an item from the customer's cart. 
        /// The request body should contain the customer ID and product ID.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> ClearCart(Guid customerId)
        {
            var result = await _mediator.Send(new ClearCartCommand(customerId));
            return result ? Ok() : NotFound();
        }
    }
}
