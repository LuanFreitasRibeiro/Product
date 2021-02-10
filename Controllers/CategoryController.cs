using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Service.Abstraction;
using ProductCatalog.Domain;
using System;
using System.Threading.Tasks;

namespace ProductCatalog.Controllers
{
    [Produces("application/json")]
    [Route("v1/category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        //Create
        [HttpPost]
        [ProducesResponseType(typeof(Category), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
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

        //Read
        [HttpGet]
        [ProducesResponseType(typeof(Category), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _categoryService.GetAllCategoryAsync());
        }

        ////Read
        //Buscando as marcas por ID
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Category), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetCategoryBydId([FromRoute] Guid id)
        {
            var obj = await _categoryService.GetCategoryByIdAsync(id);
            if (obj == null)
                return NotFound();

            return Ok(obj);
        }

        //Delete
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Category), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] Guid id, [FromBody] Category brand)
        {
            var obj = await _categoryService.GetCategoryByIdAsync(id);
            if (obj == null)
                return NotFound();

            return Accepted(await _categoryService.UpdateCategoryAsync(id, brand));
        }

        //Delete
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Category), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteBrandAsync([FromRoute] Guid id)
        {
            var obj = await _categoryService.GetCategoryByIdAsync(id);
            if (obj == null)
                return NotFound();

            await _categoryService.DeleteCategoryAsync(id);

            return NoContent();
        }
    }
}