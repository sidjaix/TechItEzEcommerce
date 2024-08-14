using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class BlogController : Controller
    {
        // GET: BlogController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BlogDetail()
        {
            return View();
        }

    }
}
