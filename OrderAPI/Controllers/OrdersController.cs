using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderEntities;
using OrderServices.Interface;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
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
        public void AddOrder([FromBody]Order order)
        {
            Order.AddOrder(order);
        }
    }
}
