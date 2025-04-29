using Microsoft.AspNetCore.Mvc;
using OrderEntities;
using OrderService.Application.Interfaces;

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
        public async Task<Order?> GetOrder(int id)
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
