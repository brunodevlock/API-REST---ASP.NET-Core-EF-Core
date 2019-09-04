using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;


namespace ProductCatalog.Controllers{


    public class CategoryController : Controller {

        private readonly StoreDataContext _context;


        //Construtor da classe gerando dependencia
        public CategoryController(StoreDataContext context){

            _context = context;
        }



            //////////// MÉTODOS DE READ - LEITURA - GET //////////////////////
            //////////////////////////////////////////////////////////////////




        [Route("v1/categories")]
        [HttpGet]

        //Retorna uma Lista de Categorias com IEnumerable
        // Sempre que joga dados direto na tela usar o AsNoTracking pra não trazer muitos dados 
        // desnecessários.
        public IEnumerable<Category> Get(){

            return _context.Categories.AsNoTracking().ToList();

        }


        [Route("v1/categories/{id}")]
        [HttpGet]

        public Category Get(int id){

            return _context.Categories.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();

        }

        [Route("v1/categories/{id}/products")]
        [HttpGet]

        //Obtendo os produtos desta categoria 
        public IEnumerable<Product> GetProducts(int id){

            return _context.Products.AsNoTracking().Where(x => x.CategoryId == id).ToList();
        }


            //////////// MÉTODOS DE ESCRITA - POST //////////////////////
            /////////////////////////////////////////////////////////////




        [Route("v1/categories")]
        [HttpPost]

        public Category Post([FromBody]Category category){

            _context.Categories.Add(category);
            _context.SaveChanges();

            return category;

        }


            // EXECUTA A ATUALIZAÇÃO NO BANCO /////
            // PUT ////

        [Route("v1/categories")]
        [HttpPut]

        public Category Put([FromBody]Category category) {

            _context.Entry<Category>(category).State = EntityState.Modified;
            _context.SaveChanges();

            return category;

        }


        // REMOVER DO BANCO ///
        // DELETE ///

        [Route("v1/categories")]
        [HttpDelete]


        public Category Delete([FromBody]Category category){

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return category;

        }






    }








}