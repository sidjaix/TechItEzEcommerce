using CartApplication.DTOs;
using CartApplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartApi.Controllers;

[Route("api/wishlist")]
[ApiController]
[Authorize]
public class WishlistController() : ControllerBase
{

}
