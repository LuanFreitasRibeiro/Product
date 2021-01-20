using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.BrandsViewModels;

namespace ProductCatalog.Controllers
{
    [Produces("application/json")]
    [Route("v1/brands")]
    public class BrandController : Controller
    {
        private readonly BrandRepository _repository;

        public BrandController(BrandRepository repository)
        {
            _repository = repository;    
        }

        //Read
        [HttpGet]
        [ProducesResponseType(typeof(Brand), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IEnumerable<ListBrandViewModel> GetAllBrands()
        {
            return _repository.GetAllBrands();
        }

        //Read
        //Buscando as marcas por ID
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Brand), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public Brand GetBranBydId(int id)
        {
            return _repository.GetBrandbyId(id);
        }

        //Read
        //Buscando produtos por marca
        [HttpGet("{id}/products")]
        [ProducesResponseType(typeof(Brand), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IEnumerable<Product> GetProductsBrand(int id)
        {
            return _repository.GetProductsBrand(id);
        }

        //Create
        [HttpPost]
        [ProducesResponseType(typeof(Brand), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ResultViewModel CreateBrand([FromBody]EditorBrandViewModel model)
        {
            model.Validate();
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível cadastrar a marca",
                    Data = model.Notifications
                };

            var brand = new Brand();
            brand.Name = model.Name;

            _repository.Save(brand);

            return new ResultViewModel
            {
                Success = true,
                Message = "Marca cadastrada com sucesso!",
                Data = model
            };
        }

        //Update
        [HttpPut("{Id}")]
        [ProducesResponseType(typeof(Brand), 202)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public ResultViewModel UpdateBrand([FromRoute] EditorBrandViewModel brandId, [FromBody]EditorBrandViewModel model)
        {
            model.Validate();
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível alterar a marca",
                    Data = model.Notifications
                };

            var brand = _repository.GetBrandbyId(brandId.Id);
            brand.Name = model.Name;

            _repository.Update(brand);

            return new ResultViewModel
            {
                Success = true,
                Message = "Marca alterada com sucesso!",
                Data = model
            };
        }

        //Delete
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Brand), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ResultViewModel Delete([FromRoute] EditorBrandViewModel brandId)
        {
            var brand = _repository.GetBrandbyId(brandId.Id);
            brand.Id = brandId.Id;

            _repository.Delete(brand);

            return new ResultViewModel
            {
                Success = true,
                Message = "Marca deletada com sucesso!",
                Data = ""
            };
        }
    }
}