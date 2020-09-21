using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.CategoryViewModels;

namespace ProductCatalog.Controllers
{
    public class CategoryController: Controller
    {
        private readonly CategoryRepository _repository;

        public CategoryController(CategoryRepository repository)
        {
            _repository = repository;
        }

        //Read
        [Route("v1/categories")]
        [HttpGet]
        public IEnumerable<ListCategoryViewModel> GetAllCategories()
        {
            return _repository.GetAllCategories();   
        }

        //Read
        //Buscando o protudo por id
        [Route("v1/categories/{id}")]
        [HttpGet]
        public Category GetCategoryId(int id)
        {
            // Find() não funciona com AsNoTracking
            // return _context.Categories.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            return _repository.GetCategoryId(id);
        }

        //Read
        //Buscando produtos por categoria
        [Route("v1/categories/{id}/products")]
        [HttpGet]
        public IEnumerable<Product> GetProductsCategory(int id)
        {
            return _repository.GetProductsCategory(id);
        }

        //Create
        [Route("v1/categories")]
        [HttpPost]
        //[FromBody] está dizendo o parâmetro (category) será recebido do corpo da requisição
        public ResultViewModel Post([FromBody]EditorCategoryViewModel model)
        {
            model.Validate();
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não possível cadastrar o produto",
                    Data = model.Notifications
                };

            var category = new Category();
            category.Name = model.Name;

            _repository.Save(category);

            return new ResultViewModel
            {
                Success = true,
                Message = "Categoria cadastrada com sucesso!",
                Data = category
            };
        }

        //Update
        [Route("v1/categories")]
        [HttpPut]
        public ResultViewModel Put([FromBody]EditorCategoryViewModel model)
        {
            model.Validate();
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível alterar a categoria",
                    Data = model.Notifications
                };
            
            var category = _repository.GetCategoryId(model.Id);
            category.Name = model.Name;

            _repository.Update(category);

            return new ResultViewModel
            {
                Success = true,
                Message = "Categoria alterada com sucesso!",
                Data = model
            };
        }

        //Delete
        [Route("v1/categories")]
        [HttpDelete]
        public ResultViewModel Delete([FromBody]EditorCategoryViewModel model)
        {
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível deletar a categoria",
                    Data = model.Notifications
                };

            var category = _repository.GetCategoryId(model.Id);
            category.Id = model.Id;

            _repository.Delete(category);

            return new ResultViewModel
            {
                Success = true,
                Message = "Categoria deletada com sucesso!",
                Data = model
            };
        }
    }
}