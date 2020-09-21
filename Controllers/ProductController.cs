using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        //Read 
        [Route("v1/products")]
        [HttpGet]
        public IEnumerable<ListProductViewModel> GetAllProducts()
        {
            return _repository.GetAllProducts();
        }

        //Read
        [Route("v1/products/{id}")]
        [HttpGet]
        public Product GetProductsId(int id)
        {
            return _repository.GetProductsId(id);
        }

        //Create
        [Route("v1/products")]
        [HttpPost]
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

            _repository.Save(product);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto cadastrado com sucesso!",
                Data = product
            };
        }

        //Update
        [Route("v1/products")]
        [HttpPut]
        public ResultViewModel Put([FromBody]EditorProductViewModel model)
        {
            model.Validate();
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível alterar o produto",
                    Data = model.Notifications
                };
            
            var product = _repository.GetProductsId(model.Id);
            product.Name = model.Name;
            product.CategoryId = model.CategoryId;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            _repository.Update(product);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto alterado com sucesso!",
                Data = product
            };
        }

        //Delete
        [Route("v1/products")]
        [HttpDelete]
        public ResultViewModel Delete([FromBody]EditorProductViewModel model)
        {
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível deletar o produto",
                    Data = model.Notifications
                };

            var product = _repository.GetProductsId(model.Id);
            product.Id = model.Id;

            _repository.Delete(product);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto deletado com sucesso!",
                Data = model
            };
        }
    }
}