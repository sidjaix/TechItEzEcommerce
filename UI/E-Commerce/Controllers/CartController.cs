using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class CartController : Controller
    {
        // GET: CartController
        public ActionResult Index()
        {
            return View();
        }

    }
}
