using Microsoft.AspNetCore.Mvc;
using Product_Api.Common.Filters;
using Product_Core.Entities;
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
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Product>> CreateNewProduct([FromBody] Product productData)
        {
            var product = await productRepository.CreateNewProductAsync(productData);
            return CreatedAtAction(nameof(GetProductById), new { productId = product.ProductId }, product);
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await productRepository.GetProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Get all products of a category
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetProductByCategory/{categoryId}")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await productRepository.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Product</returns>
        [HttpGet("{productId}")]
        //[Route("GetProductById")]
        public async Task<ActionResult<Product>> GetProductById(int productId)
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
        /// <returns>Product</returns>
        [HttpPut("Update")]
        public async Task<ActionResult<Product>> UpdateProduct(Product productData)
        {
            var product = await productRepository.UpdateExistingProductAsync(productData);
            if (product is null)
            {
                return BadRequest("Product has not been updated due to in appropriate data");
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
            try
            {
                await productRepository.DeleteProductAsync(productId);
                return Ok("Product Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
