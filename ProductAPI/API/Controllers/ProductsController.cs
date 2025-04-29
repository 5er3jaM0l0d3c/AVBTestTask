using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Interface;
using ProductService.Domain.Entities;


namespace ProductService.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProduct Product { get; set; }

        public ProductsController(IProduct product)
        {
            Product = product;
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
        public IActionResult UpdateProduct(int productId, [FromQuery] int amount)
        {
            Product.UpdateProduct(productId, amount);
            return Ok();
        }
    }
}
