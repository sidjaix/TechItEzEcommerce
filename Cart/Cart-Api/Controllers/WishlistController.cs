using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Cart_Api.Controllers;

[Route("api/wishlist")]
[ApiController]
[Authorize]
public class WishlistController : ControllerBase
{

}
