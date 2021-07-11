using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Service.Abstraction;
using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.Category;
using System;
using System.Threading.Tasks;

namespace ProductCatalog.Controllers
{
    [Produces("application/json")]
    [Route("v1/category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        #region Constructor
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        #endregion

        #region Create Category
        [HttpPost]
        [ProducesResponseType(typeof(Category), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest category)
        {
            try
            {
                var obj = await _categoryService.AddAsync(category);
                return Created(nameof(GetCategoryBydId), obj);
            }
            catch (ArgumentNullException ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get Caregories
        [HttpGet]
        [ProducesResponseType(typeof(Category), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _categoryService.GetAllCategoryAsync());
        }
        #endregion

        #region Get Cateogiries By Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Category), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> GetCategoryBydId([FromRoute] Guid id)
        {
            var obj = await _categoryService.GetCategoryByIdAsync(id);
            if (obj == null)
                return NotFound();

            return Ok(obj);
        }
        #endregion

        #region Update Category
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Category), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] Guid id, [FromBody] UpdateCategoryRequest brand)
        {
            var obj = await _categoryService.GetCategoryByIdAsync(id);
            if (obj == null)
                return NotFound();

            return Accepted(await _categoryService.UpdateCategoryAsync(id, brand));
        }
        #endregion

        #region Delete Category
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Category), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid id)
        {
            var obj = await _categoryService.GetCategoryByIdAsync(id);
            if (obj == null)
                return NotFound();

            await _categoryService.DeleteCategoryAsync(id);

            return NoContent();
        }
        #endregion
    }
}