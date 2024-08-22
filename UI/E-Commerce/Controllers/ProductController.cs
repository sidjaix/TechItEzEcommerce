using ApiServices.Models.Product;
using ApiServices.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_Commerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var productPageModel = new ProductPageModel
            {
                Products = await _productService.GetProductsAsync(),
                Categories = await _categoryService.GetAllCategoryAsync()
            };
            return View(productPageModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductDetail(int productId)
        {
            ProductViewModel product = new();
            var response = await _productService.GetProductDetailAsync(productId);
            if (response != null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(response.Result));
            }
            product.ProductsByCategory = await _productService.GetProductsByCategoryAsync(product.CategoryId);
            product.ProductsByCategory = product.ProductsByCategory.Where(x => x.ProductId != productId).Take(4).ToList();
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var productPageModel = new ProductPageModel
            {
                Products = await _productService.GetProductsByCategoryAsync(categoryId),
                Categories = await _categoryService.GetAllCategoryAsync()
            };
            return View("Index", productPageModel);
        }
    }
}
