using MANAGER.DTOS.Products;
using MANAGER.REPOSITORY.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LABEL_MANAGER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var list = productService.GetAllProducts();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult CreateProducts(CreateProductDTO createProductDTO)
        {
            var result = productService.CreateProducts(createProductDTO);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteProductById(int Id) 
        {
            var result = productService.DeleteProductById(Id);
            return Ok(result);
        }
    }
}
