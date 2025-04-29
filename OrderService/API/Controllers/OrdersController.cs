using Microsoft.AspNetCore.Mvc;
using OrderEntities;
using OrderService.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OrderService.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrder _order { get; set; }
        public OrdersController(IOrder order)
        {
            _order = order;
        }

        [HttpGet("{id}")]
        public async Task<Order?> GetOrder([Range(1, int.MaxValue, ErrorMessage = "Id must be > 0")] int id)
        {
            return await _order.GetOrder(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            await _order.AddOrder(order);
            return Created("https://localhost:5066/orders/" + order.Id, order);
        }
    }
}
