using MANAGER.DTOS.Products;
using MANAGER.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGER.REPOSITORY.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Products> GetAllProducts();

        Products CreateProducts(CreateProductDTO createProductDTO);

        bool DeleteProductById(int Id);
    }
}
