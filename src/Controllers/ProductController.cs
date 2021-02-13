using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Service.Abstraction;
using ProductCatalog.Domain;
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

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //Create
        [HttpPost]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
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

        //Read
        [HttpGet]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProductAsync());
        }

        ////Read
        //Buscando as marcas por ID
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProductBydId([FromRoute] Guid id)
        {
            var obj = await _productService.GetProductByIdAsync(id);
            if (obj == null)
                return NotFound();

            return Ok(obj);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateBrandAsync([FromRoute] Guid id, [FromBody] EditorProductViewModel product)
        {
            var obj = await _productService.GetProductByIdAsync(id);
            if (obj == null)
                return NotFound();

            return Accepted(await _productService.UpdateProductAsync(id, product));
        }

        //Delete
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Product), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteBrandAsync([FromRoute] Guid id)
        {
            var obj = await _productService.GetProductByIdAsync(id);
            if (obj == null)
                return NotFound();

            await _productService.DeleteProductAsync(id);

            return NoContent();
        }
    }
}