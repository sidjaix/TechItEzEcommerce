using CartApplication.DTOs;
using CartApplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [Authorize]
    public class CartController(ICartRepository cartRepository, ResponseDto response) : ControllerBase
    {

    }
}
