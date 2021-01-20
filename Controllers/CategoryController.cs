using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.CategoryViewModels;

namespace ProductCatalog.Controllers
{
    [Produces("application/json")]
    [Route("v1/category")]
    public class CategoryController: Controller
    {
        private readonly CategoryRepository _repository;

        public CategoryController(CategoryRepository repository)
        {
            _repository = repository;
        }

        //Read
        [HttpGet]
        [ProducesResponseType(typeof(Brand), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IEnumerable<ListCategoryViewModel> GetAllCategories()
        {
            return _repository.GetAllCategories();   
        }

        //Read
        //Buscando o categoria por id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Brand), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public Category GetCategoryId(int id)
        {
            // Find() não funciona com AsNoTracking
            // return _context.Categories.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            return _repository.GetCategoryId(id);
        }

        //Read
        //Buscando produtos por categoria
        [HttpGet("{id}/products")]
        [ProducesResponseType(typeof(Brand), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IEnumerable<Product> GetProductsCategory(int id)
        {
            return _repository.GetProductsCategory(id);
        }

        //Create
        [HttpPost]
        [ProducesResponseType(typeof(Brand), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        //[FromBody] está dizendo o parâmetro (category) será recebido do corpo da requisição
        public ResultViewModel CreateCategory([FromBody]EditorCategoryViewModel model)
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
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Brand), 202)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public ResultViewModel UpdateCategory([FromRoute] EditorCategoryViewModel categoryId, [FromBody]EditorCategoryViewModel model)
        {
            model.Validate();
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível alterar a categoria",
                    Data = model.Notifications
                };
            
            var category = _repository.GetCategoryId(categoryId.Id);
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
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Brand), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ResultViewModel Delete([FromRoute] EditorCategoryViewModel categoryId, [FromBody]EditorCategoryViewModel model)
        {
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível deletar a categoria",
                    Data = model.Notifications
                };

            var category = _repository.GetCategoryId(categoryId.Id);
            category.Id = categoryId.Id;

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