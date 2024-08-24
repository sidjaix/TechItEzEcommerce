using ApiServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class OrderController(IOrderService orderService) : Controller
    {
        // GET: OrderController
        public async Task<ActionResult> Index()
        {
            //var orders = await orderService.GetOrdersAsync();

            return View();
        }

    }
}
