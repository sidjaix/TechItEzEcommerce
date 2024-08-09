using Microsoft.AspNetCore.Mvc;
using Product_Api.Common.Filters;
using Product_Core.Entities;
using Product_Core.Models;
using Product_Data.Repositories;

namespace Product_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <returns>ProductModel</returns>
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ProductModel>> CreateNewProduct([FromBody] ProductModel productData)
        {
            var product = await productRepository.CreateNewProductAsync(productData);
            return CreatedAtAction(nameof(GetProductById), new { productId = product.ProductId }, product);
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of ProductModel</returns>
        [HttpGet]
        public async Task<ActionResult<List<ProductModel>>> GetProducts()
        {
            var products = await productRepository.GetProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Get all products of a category
        /// </summary>
        /// <returns>List of ProductModel</returns>
        [HttpGet("GetProductByCategory/{categoryId}")]
        public async Task<ActionResult<List<ProductModel>>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await productRepository.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>ProductModel</returns>
        [HttpGet("{productId}")]
        //[Route("GetProductById")]
        public async Task<ActionResult<ProductModel>> GetProductById(int productId)
        {
            var product = await productRepository.GetProductDetailAsync(productId);
            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Update existing product
        /// </summary>
        /// <param name="productData"></param>
        /// <returns>ProductModel</returns>
        [HttpPut("Update")]
        public async Task<ActionResult<ProductModel>> UpdateProduct(ProductModel productData)
        {
            var product = await productRepository.UpdateExistingProductAsync(productData);
            if (product is null)
            {
                return BadRequest("ProductModel has not been updated due to in appropriate data");
            }
            return Ok(product);
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductAsync(int productId)
        {
            var isdeleted = await productRepository.DeleteProductAsync(productId);
            if (!isdeleted)
            {
                return BadRequest("Product has not deleted");
            }
            return Ok("Product has deleted");
        }
    }
}
