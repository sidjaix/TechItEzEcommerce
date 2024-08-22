using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product_Api.Common.Filters;
using Product_Core.Models;
using Product_Data.Repository.IRepository;

namespace Product_Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly ResponseDto _response;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            _response = new ResponseDto();
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <returns>Custom response with product model as result</returns>
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ProductModel>> CreateNewProduct([FromBody] ProductModel productData)
        {
            try
            {
                var product = await productRepository.CreateNewProductAsync(productData);
                _response.Result = CreatedAtAction(nameof(GetProductById), new { productId = product.ProductId }, product);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>custom response with list of product model as result</returns>
        [HttpGet]
        public async Task<ActionResult<List<ProductModel>>> GetProducts()
        {
            try
            {
                var products = await productRepository.GetProductsAsync();
                _response.Result = products;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Get all products of a category
        /// </summary>
        /// <returns>Custom response with list of product model as result</returns>
        [HttpGet("GetProductByCategory/{categoryId}")]
        public async Task<ActionResult<ResponseDto>> GetProductsByCategoryAsync(int categoryId)
        {
            try
            {
                var products = await productRepository.GetProductsByCategoryAsync(categoryId);
                _response.Result = products;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Custom response with product model as result</returns>
        [HttpGet("{productId}")]
        public async Task<ActionResult<ResponseDto>> GetProductById(int productId)
        {
            try
            {
                var product = await productRepository.GetProductDetailAsync(productId);
                _response.Result = product;
                if (product is null)
                {
                    _response.Message = "Record has not found with specified Product ID.";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Update existing product
        /// </summary>
        /// <param name="productData"></param>
        /// <returns>Custom Response</returns>
        [HttpPut("Update")]
        [ValidateModel]
        public async Task<ActionResult<ResponseDto>> UpdateProduct(ProductModel productData)
        {
            try
            {
                var product = await productRepository.UpdateExistingProductAsync(productData);
                _response.Result = product;
                if (product is null)
                {
                    _response.Message = "ProductModel has not been updated due to in appropriate data";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Custom Response</returns>
        [HttpDelete("{productId}")]
        public async Task<ActionResult<ResponseDto>> DeleteProductAsync(int productId)
        {
            try
            {
                var isdeleted = await productRepository.DeleteProductAsync(productId);
                _response.Result = isdeleted;
                if (!isdeleted)
                {
                    _response.Message = "Product has not deleted";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}
