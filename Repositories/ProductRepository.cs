using ProductCatalog.Data;
using System.Collections.Generic;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.ProductViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ProductCatalog.Repositories{

    public class ProductRepository{

        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context){

            _context = context;
        }

        public IEnumerable<ListProductViewModel> Get()
        {
            return _context.Products
                .Include(x => x.Category) //incluir a categoria listando 
                .Select(x => new ListProductViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    Category = x.Category.Title,
                    CategoryId = x.CategoryId
                })
                .AsNoTracking()
                .ToList(); 
        }

        public Product Get(int id){

            return _context.Products.Find(id);
        }

        public void Save(Product product){

            _context.Products.Add(product);
            _context.SaveChanges();

        }

        public void Update(Product product) {


            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();


        }


        
    }

}