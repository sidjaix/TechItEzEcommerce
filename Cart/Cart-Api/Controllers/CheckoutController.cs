using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cart_Api.Controllers
{
    [Route("api/checkout")]
    [ApiController]
    [Authorize]
    public class CheckoutController : ControllerBase
    {

    }
}
