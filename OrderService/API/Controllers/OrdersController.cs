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
        public Order? GetOrder(int id)
        {
            return Order.GetOrder(id);
        }

        [HttpPost]
        public async Task AddOrder([FromBody] Order order)
        {
            await Order.AddOrder(order);
        }
    }
}
