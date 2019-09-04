using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels.ProductViewModels;


namespace ProductCatalog.Controllers
{

    public class ProductController : Controller {

        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository){

            _repository = repository;
        }


        //////// GET /////////

        [Route("v1/products")]
        [HttpGet]

        public IEnumerable<ListProductViewModel> Get(){

            return _repository.Get();
        }

        [Route("v1/products{id}")]
        [HttpGet]

        public Product Get(int id){

            return _repository.Get(id);
        }


        [Route("v1/products")]
        [HttpPost]

        public ResultViewModel Post([FromBody]EditorProductViewModel model){

            model.Validate();

            if (model.Invalid) 
                return new ResultViewModel
            {
                Success = false,
                Message = "Não foi possível cadastrar o produto",
                Data = model.Notifications
            };

            var product = new Product();
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.CreateDate = DateTime.Now;  //Nunca recebe essa informação da tela
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now; //Nunca recebe essa informação da tela
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            _repository.Save(product);

            //Se der tudo certo retorna esse ResultViewModel
            return new ResultViewModel {

                Success = true,
                Message = "Produto cadastrado com sucesso",
                Data = product
            };
            
        }

        [Route("v1/products")]
        [HttpPut]

        public ResultViewModel Put([FromBody]EditorProductViewModel model){

            model.Validate();
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possível alterar o produto",
                    Data = model.Notifications
                };

            var product = _repository.Get(model.Id);
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.CreateDate = DateTime.Now;  //Nunca recebe essa informação da tela
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now; //Nunca recebe essa informação da tela
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
    

    }



}