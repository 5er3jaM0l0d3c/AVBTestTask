﻿using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace ProductService.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProduct _product { get; set; }

        public ProductsController(IProduct product)
        {
            _product = product;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts([Range(1, int.MaxValue, ErrorMessage = "Id must be > 0")] int id)
        {
            var result = await _product.GetProduct(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            await _product.AddProduct(product);
            return Created("https://localhost:5057/Products/" + product.Id, product);
        }

        [HttpPut("{productId}/stock")]
        public IActionResult UpdateProduct(int productId, [FromQuery, Required, Range(1, int.MaxValue, ErrorMessage = "amount must be > 0")] int amount)
        {
            _product.UpdateProduct(productId, amount);
            return Ok();
        }
    }
}
