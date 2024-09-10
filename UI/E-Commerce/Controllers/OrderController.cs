using ApiServices.Services.IService;
using E_Commerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class OrderController(IOrderService orderService) : Controller
    {
        public async Task<ActionResult> Index()
        {
            var orders = await orderService.GetOrdersAsync();

            return View(orders);
        }

    }
}
