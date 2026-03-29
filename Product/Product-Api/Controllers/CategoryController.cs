using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Common.Filters;
using ProductApplication.DTOs;
using ProductApplication.Interfaces;

namespace ProductApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ResponseDto _response;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
            _response = new ResponseDto();
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <returns>CategoryModel</returns>
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<CategoryDto>> CreateNewCategoryAsync([FromBody] CategoryDto categoryData)
        {
            try
            {
                var category = await categoryRepository.CreateNewCategoryAsync(categoryData);
                _response.Result = category;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>ResponseDto</returns>
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllCategoryAsync()
        {
            try
            {
                var categories = await categoryRepository.GetAllCategoryAsync();
                _response.Result = categories;
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
        /// Get category detail
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>CategoryModel</returns>
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryAsync(int categoryId)
        {

            try
            {
                var category = await categoryRepository.GetCategoryAsync(categoryId);
                _response.Result = category;
                if (category is null)
                {
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Update existing category
        /// </summary>
        /// <param name="categoryData"></param>
        /// <returns>CategoryModel</returns>
        [HttpPut("Update")]
        [ValidateModel]
        public async Task<ActionResult<CategoryDto>> UpdateExistingCategoryAsync(CategoryDto categoryData)
        {
            try
            {
                var category = await categoryRepository.UpdateExistingCategoryAsync(categoryData);
                _response.Result = category;
                if (category is null)
                {
                    _response.Message = "CategoryModel has not updated due to in appropriate data";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Delete CategoryModel
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>bool</returns>
        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<bool>> DeleteCategoryAsync(int categoryId)
        {
            try
            {
                var isdeleted = await categoryRepository.DeleteCategoryAsync(categoryId);
                if (!isdeleted)
                {
                    _response.Message = "Record does not delete due to incorrect data.";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }

            return NoContent();
        }
    }
}
