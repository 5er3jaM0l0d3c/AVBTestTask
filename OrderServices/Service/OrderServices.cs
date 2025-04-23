using OrderEntities;
using OrderServices.Interface;
using ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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


        public async Task AddOrder(Order order)
        {
            HttpClient client = new();
            var response = await client.GetAsync("http://localhost:5066/products/" + order.ProductId.ToString());
            if (!response.IsSuccessStatusCode)
                throw new Exception("Something wrong");

            response = await client.PutAsync("http://localhost:5066/products/" + order.ProductId.ToString() + "/stock?amount=" + order.ProductAmount.ToString(), null);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Something wrong");



            await context.Order.AddAsync(order);
            await context.SaveChangesAsync();
        }

        public Order? GetOrder(int id)
        {
            return context.Order.FirstOrDefault(o => o.Id == id);
        }
    }
}
