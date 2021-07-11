using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Service.Abstraction;
using ProductCatalog.Domain;
using ProductCatalog.Domain.Request.Product;
using ProductCatalog.ViewModels.ProductViewModels;
using System;
using System.Threading.Tasks;

namespace ProductCatalog.Controllers
{
    [Produces("application/json")]
    [Route("v1/product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        #region Constructor
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region Create Product
        [HttpPost]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                var obj = await _productService.AddAsync(product);
                return Created(nameof(GetProductBydId), obj);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Get Products
        [HttpGet]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProductAsync());
        }
        #endregion

        #region Get Product By Id
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> GetProductBydId([FromRoute] Guid id)
        {
            var obj = await _productService.GetProductByIdAsync(id);
            if (obj == null)
                return NotFound();

            return Ok(obj);
        }
        #endregion

        #region Update Product
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid id, [FromBody] EditorProductViewModel product)
        {
            var obj = await _productService.GetProductByIdAsync(id);
            if (obj == null)
                return NotFound();

            return Accepted(await _productService.UpdateProductAsync(id, product));
        }
        #endregion

        #region MyRegion
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Product), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid id)
        {
            var obj = await _productService.GetProductByIdAsync(id);
            if (obj == null)
                return NotFound();

            await _productService.DeleteProductAsync(id);

            return NoContent();
        }
        #endregion

    }
}