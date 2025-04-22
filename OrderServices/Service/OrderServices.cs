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


        public async void AddOrder(Order order)
        {
            using(HttpClient client = new())
            {
                var response = await client.GetAsync("http://localhost:5066/products/" + order.ProductId.ToString());
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Product with Id=" + order.ProductId + " is undefined");
                
                response = await client.PutAsync("http://localhost:5066/products/" + order.ProductId.ToString() + "/-" + order.ProductAmount.ToString(), null);

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Something wrong");

            }

            context.Order.Add(order);
        }

        public Order? GetOrder(int id)
        {
            return context.Order.FirstOrDefault(o => o.Id == id);
        }
    }
}
