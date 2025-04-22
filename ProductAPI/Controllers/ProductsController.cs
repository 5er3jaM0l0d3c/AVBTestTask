using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductEntities;
using ProductServices.Interface;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProduct Product { get; set; }

        public ProductsController(IProduct product)
        {
            this.Product = product;
        }

        [Authorize]
        [HttpGet("{id}")]
        public Product? GetProducts(int id)
        {
            return Product.GetProduct(id);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product) 
        {
            Product.AddProduct(product);
            return Ok();
        }

        [Authorize]
        [HttpPut("{productId}/stock")]
        public IActionResult UpdateProduct(int productId, int amount)
        {
            Product.UpdateProduct(productId, amount);
            return Ok();
        }
    }
}
