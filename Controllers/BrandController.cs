using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.BrandsViewModels;

namespace ProductCatalog.Controllers
{
    public class BrandController : Controller
    {
        private readonly BrandRepository _repository;

        public BrandController(BrandRepository repository)
        {
            _repository = repository;    
        }

        //Read
        [Route("v1/brands")]
        [HttpGet]
        public IEnumerable<ListBrandViewModel> GetAllBrands()
        {
            return _repository.GetAllBrands();
        }

        //Read
        //Buscando as marcas por ID
        [Route("v1/brands/{id}")]
        [HttpGet]
        public Brand GetBrandId(int id)
        {
            return _repository.GetBrandId(id);
        }

        //Read
        //Buscando produtos por marca
        [Route("v1/brands/{id}/products")]
        [HttpGet]
        public IEnumerable<Product> GetProductsBrand(int id)
        {
            return _repository.GetProductsBrand(id);
        }

        //Create
        [Route("v1/brands")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorBrandViewModel model)
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
        [Route("v1/brands")]
        [HttpPut]
        public ResultViewModel Put([FromBody]EditorBrandViewModel model)
        {
            model.Validate();
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível alterar a marca",
                    Data = model.Notifications
                };

            var brand = _repository.GetBrandId(model.Id);
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
        [Route("v1/brands")]
        [HttpDelete]
        public ResultViewModel Delete([FromBody]EditorBrandViewModel model)
        {
            var brand = _repository.GetBrandId(model.Id);
            brand.Id = model.Id;

            _repository.Delete(brand);

            return new ResultViewModel
            {
                Success = true,
                Message = "Marca deletada com sucesso!",
                Data = model
            };
        }
    }
}