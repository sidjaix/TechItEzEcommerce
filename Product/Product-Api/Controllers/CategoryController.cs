using Microsoft.AspNetCore.Mvc;
using Product_Api.Common.Filters;
using Product_Core.Entities;
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
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<Category>> CreateNewCategoryAsync([FromBody] Category categoryData)
        {
            var category = await categoryRepository.CreateNewCategoryAsync(categoryData);
            return category;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategoryAsync()
        {
            var categories = await categoryRepository.GetAllCategoryAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Get category detail
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>Category</returns>
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<Category>> GetCategoryAsync(int categoryId)
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
        /// <returns>Category</returns>
        [HttpPut("Update")]
        public async Task<ActionResult<Category>> UpdateExistingCategoryAsync(Category categoryData)
        {
            var category = await categoryRepository.UpdateExistingCategoryAsync(categoryData);
            if (category is null)
            {
                return BadRequest("Category has not updated due to in appropriate data");
            }
            return Ok(category);
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>bool</returns>
        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<bool>> DeleteCategoryAsync(int categoryId)
        {
            var isdeleted = await categoryRepository.DeleteCategoryAsync(categoryId);
            if (isdeleted)
            {
                return Ok("Category has deleted");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
