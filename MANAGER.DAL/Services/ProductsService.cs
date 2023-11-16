using MANAGER.DAL;
using MANAGER.DTOS.Products;
using MANAGER.MODELS;
using MANAGER.REPOSITORY.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGER.REPOSITORY.Services
{
    public class ProductsService : IProductService
    {
        private readonly AppDbContext _appDbContext;
         
        public ProductsService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        } 
        
        public IEnumerable<Products> GetAllProducts()
        {
            return _appDbContext.Products.ToList();
        }

        public Products CreateProducts(CreateProductDTO createProductDTO)
        {
            var product = new Products();
            product.Name = createProductDTO.Name;
            product.Description = createProductDTO.Description;

            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
            return product;
        }

        public bool DeleteProductById(int Id)
        {
            var product = _appDbContext.Products.Where(x => x.Id == Id).FirstOrDefault();   
            if (product != null)
            {
                _appDbContext.Products.Remove(product);
                _appDbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
