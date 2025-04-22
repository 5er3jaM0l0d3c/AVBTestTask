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

        [HttpGet("{id}")]
        public IActionResult GetProducts(int id)
        {
            var result = Product.GetProduct(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product) 
        {
            Product.AddProduct(product);
            return Ok();
        }

        [HttpPut("{productId}/stock")]
        public IActionResult UpdateProduct(int productId, int amount)
        {
            Product.UpdateProduct(productId, amount);
            return Ok();
        }
    }
}
