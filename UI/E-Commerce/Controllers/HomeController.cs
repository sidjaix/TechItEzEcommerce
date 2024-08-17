using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Models;
using ApiServices.Models;
using ApiServices.Services.IService;
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService productService;
    private readonly ICategoryService categoryService;

    public HomeController(ILogger<HomeController> logger,
    IProductService productService,
    ICategoryService categoryService)
    {
        _logger = logger;
        this.productService = productService;
        this.categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
