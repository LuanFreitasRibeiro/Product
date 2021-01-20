using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Controllers
{
    [Route("v1/product")]
    [Produces("application/json")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        //Read 
        [HttpGet]
        [ProducesResponseType(typeof(Brand), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IEnumerable<ListProductViewModel> GetAllProducts()
        {
            return _repository.GetAllProducts();
        }

        //Read
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Brand), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public Product GetProductsId(int id)
        {
            return _repository.GetProductsId(id);
        }

        //Create
        [HttpPost]
        [ProducesResponseType(typeof(Brand), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ResultViewModel Post([FromBody]EditorProductViewModel model)
        {
            model.Validate();
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível cadastrar o produto",
                    Data = model.Notifications
                };

            var product = new Product();
            product.Name = model.Name;
            product.CategoryId = model.CategoryId;
            product.CreateDate = DateTime.Now;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;
            product.BrandId = model.BrandId;

            _repository.Save(product);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto cadastrado com sucesso!",
                Data = product
            };
        }

        //Update
        [HttpPut("{Id}")]
        [ProducesResponseType(typeof(Brand), 202)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public ResultViewModel Put([FromRoute] EditorProductViewModel categoryId, [FromBody]EditorProductViewModel model)
        {
            model.Validate();
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível alterar o produto",
                    Data = model.Notifications
                };
            
            var product = _repository.GetProductsId(categoryId.Id);
            product.Name = model.Name;
            product.CategoryId = model.CategoryId;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;
            product.BrandId = model.BrandId;

            _repository.Update(product);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto alterado com sucesso!",
                Data = product
            };
        }

        //Delete
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Brand), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ResultViewModel Delete([FromRoute]EditorProductViewModel categoryId)
        {
            var product = _repository.GetProductsId(categoryId.Id);
            product.Id = categoryId.Id;

            _repository.Delete(product);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto deletado com sucesso!",
                Data = ""
            };
        }
    }
}