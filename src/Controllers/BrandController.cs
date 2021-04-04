using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Service.Abstraction;
using ProductCatalog.Domain;
using ProductCatalog.ViewModels.BrandsViewModels;
using System;
using System.Threading.Tasks;

namespace ProductCatalog.Controllers
{
    [Produces("application/json")]
    [Route("v1/brands")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        #region Constructor
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        #endregion

        #region Create Brand
        [HttpPost]
        [ProducesResponseType(typeof(Brand), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateBrand([FromBody] Brand brand)
        {
            try
            {
                var obj = await _brandService.AddAsync(brand);
                return Created(nameof(GetBranBydId), obj);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get All Brands
        [HttpGet]
        [ProducesResponseType(typeof(Brand), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllBrands()
        {
            return Ok(await _brandService.GetAllBrandAsync());
        }
        #endregion

        #region Get Brand By Id
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Brand), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetBranBydId([FromRoute] Guid id)
        {
            var obj = await _brandService.GetBrandByIdAsync(id);
            if (obj == null)
                return NotFound();

            return Ok(obj);
        }
        #endregion

        #region Update Brand Async
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(Brand), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateBrandAsync([FromRoute] Guid id, [FromBody] EditorBrandViewModel brand)
        {
            var obj = await _brandService.GetBrandByIdAsync(id);
            if (obj == null)
                return NotFound();

            return Accepted(await _brandService.UpdateBrandAsync(id, brand));
        }
        #endregion

        #region Delete Brand Async
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Brand), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteBrandAsync([FromRoute] Guid id)
        {
            var obj = await _brandService.GetBrandByIdAsync(id);
            if (obj == null)
                return NotFound();

            await _brandService.DeleteBrandAsync(id);

            return NoContent();
        }
        #endregion
    }
}