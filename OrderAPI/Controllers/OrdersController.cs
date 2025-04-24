using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderEntities;
using OrderServices.Interface;

namespace OrderAPI.Controllers
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
        public OrderEntities.Order? GetOrder(int id)
        {
            return Order.GetOrder(id);
        }

        [HttpPost]
        public async Task AddOrder([FromBody] OrderEntities.Order order)
        {
            await Order.AddOrder(order);
        }
    }
}
