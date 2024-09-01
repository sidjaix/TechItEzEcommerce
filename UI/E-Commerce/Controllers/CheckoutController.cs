using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        // GET: CheckoutController
        public ActionResult Index()
        {
            return View();
        }

    }
}
