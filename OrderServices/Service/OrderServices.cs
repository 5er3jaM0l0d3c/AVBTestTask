using OrderEntities;
using OrderServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderServices.Service
{
    public class OrderServices : IOrder
    {
        private OrderServiceDbContext context {  get; set; }

        public OrderServices(OrderServiceDbContext context)
        {
            this.context = context;
        }


        public void AddOrder(Order order)
        {
            context.Order.Add(order);
        }

        public Order? GetOrder(int id)
        {
            return context.Order.FirstOrDefault(o => o.Id == id);
        }
    }
}
