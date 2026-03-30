using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartApi.Controllers
{
    [Route("api/checkout")]
    [ApiController]
    [Authorize]
    public class CheckoutController : ControllerBase
    {

    }
}
