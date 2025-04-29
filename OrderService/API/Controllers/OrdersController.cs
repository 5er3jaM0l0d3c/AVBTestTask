using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Application.Interfaces;
using OrderEntities;

namespace OrderAPI.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrder Order { get; set; }
        public OrdersController(IOrder order)
        {
            Order = order;
        }

        [HttpGet("{id}")]
        public async Task<Order?> GetOrder(int id)
        {
            return await Order.GetOrder(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            await Order.AddOrder(order);
            return Created("https://localhost:5066/orders/" + order.Id, order);
        }
    }
}
