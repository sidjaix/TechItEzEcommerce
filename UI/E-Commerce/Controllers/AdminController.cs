using ApiServices.Models.Product;
using ApiServices.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_Commerce.Controllers;

public class AdminController(IProductService productService, ICategoryService categoryService) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Products()
    {
        var products = await productService.GetProductsAsync();
        return View(products);
    }

    [HttpGet]
    public async Task<ActionResult> CreateProduct()
    {
        CreateProductViewModel product = new()
        {
            Categories = await categoryService.GetAllCategoryAsync()
        };
        return View(product);
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct(CreateProductViewModel productDetail)
    {
        var imageUrl = string.Empty;
        try
        {
            if (ModelState.IsValid)
            {
                imageUrl = await UploadImageAsync(productDetail);
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    productDetail.ImageUrl = imageUrl;
                    await productService.CreateNewProductAsync(productDetail);
                }
            }
        }
        catch (Exception ex)
        {
            // If an exception occurs, log the error and handle it
            // You can log the exception here

            // Rollback: Delete the saved image if an error occurs
            if (!string.IsNullOrEmpty(imageUrl) && System.IO.File.Exists(imageUrl))
            {
                System.IO.File.Delete(imageUrl);
            }

            // Optionally, add a model error
            ModelState.AddModelError(string.Empty, $"An error occurred while processing your request. following is the stack trace:=> {ex.Message}");
        }

        // Rollback: If validation fails, delete the saved image
        if (!ModelState.IsValid && !string.IsNullOrEmpty(imageUrl) && System.IO.File.Exists(imageUrl))
        {
            System.IO.File.Delete(imageUrl);
        }
        productDetail.Categories = await categoryService.GetAllCategoryAsync();
        return View(productDetail);
    }

    public async Task<ActionResult> DeleteProduct(int productId)
    {
        if (productId == 0)
        {
            return RedirectToAction(nameof(Products));
        }
        var response = await productService.DeleteProductAsync(productId);
        return RedirectToAction(nameof(Products));
    }

    [HttpGet]
    public async Task<ActionResult> EditProduct(int productId)
    {
        CreateProductViewModel editProduct = new()
        {
            Categories = await categoryService.GetAllCategoryAsync()
        };
        var response = await productService.GetProductDetailAsync(productId);
        if (response != null && response.IsSuccess)
        {
            var product = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(response.Result));
            editProduct.ProductId = product.ProductId;
            editProduct.CategoryId = product.CategoryId;
            editProduct.ProductName = product.ProductName;
            editProduct.Description = product.Description;
            editProduct.OriginalPrice = product.OriginalPrice;
            editProduct.SellingPrice = product.SellingPrice;
            editProduct.QuantityInStock = product.QuantityInStock;
            editProduct.ImageUrl = product.ImageUrl;
        }
        return View(editProduct);
    }

    [HttpPost]
    public async Task<ActionResult> EditProduct(CreateProductViewModel productDetail)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await productService.UpdateExistingProductAsync(productDetail);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"An error occurred while processing your request. following is the stack trace:=> {ex.Message}");
            productDetail.Categories = await categoryService.GetAllCategoryAsync();
            return View(productDetail);
        }
        return RedirectToAction(nameof(Products));
    }

    private static async Task<string> UploadImageAsync(CreateProductViewModel model)
    {
        var imagePath = string.Empty;
        if (model.ProductImage != null && model.ProductImage.Length > 0)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/product");
            var fileName = Path.GetFileName(model.ProductImage.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            // Ensure the directory exists
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Save the image to the server
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.ProductImage.CopyToAsync(fileStream);
            }

            // Optional: Save the relative path to the database
            imagePath = $"~/img/product/{fileName}";
        }
        return imagePath;
    }
}
