using Microsoft.AspNetCore.Mvc;
using Product_Api.Common.Filters;
using Product_Core.Models;
using Product_Data.Repositories;

namespace Product_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <returns>CategoryModel</returns>
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<CategoryModel>> CreateNewCategoryAsync([FromBody] CategoryModel categoryData)
        {
            var category = await categoryRepository.CreateNewCategoryAsync(categoryData);
            if (category is null)
            {
                return BadRequest("New Category has not created");
            }
            return Ok(category);
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List of CategoryModel</returns>
        [HttpGet]
        public async Task<ActionResult<List<CategoryModel>>> GetAllCategoryAsync()
        {
            var categories = await categoryRepository.GetAllCategoryAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Get category detail
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>CategoryModel</returns>
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<CategoryModel>> GetCategoryAsync(int categoryId)
        {
            var category = await categoryRepository.GetCategoryAsync(categoryId);
            if (category is null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        /// <summary>
        /// Update existing category
        /// </summary>
        /// <param name="categoryData"></param>
        /// <returns>CategoryModel</returns>
        [HttpPut("Update")]
        public async Task<ActionResult<CategoryModel>> UpdateExistingCategoryAsync(CategoryModel categoryData)
        {
            var category = await categoryRepository.UpdateExistingCategoryAsync(categoryData);
            if (category is null)
            {
                return BadRequest("CategoryModel has not updated due to in appropriate data");
            }
            return Ok(category);
        }

        /// <summary>
        /// Delete CategoryModel
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>bool</returns>
        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<bool>> DeleteCategoryAsync(int categoryId)
        {
            var isdeleted = await categoryRepository.DeleteCategoryAsync(categoryId);
            if (!isdeleted)
            {
                return BadRequest();
            }
            return Ok("Category has deleted");
        }
    }
}
