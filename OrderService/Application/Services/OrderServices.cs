using OrderAPI.Application.Interfaces;
using OrderAPI.Grpc.Service;
using OrderEntities;
using OrderServiceDbContext = OrderAPI.Infrastructure.DbContexts.OrderServiceDbContext;

namespace OrderAPI.Application.Services
{
    public class OrderServices : IOrder
    {
        private OrderServiceDbContext context {  get; set; }

        public OrderServices(OrderServiceDbContext context)
        {
            this.context = context;
        }

        public async Task AddOrder(OrderEntities.Order order)
        {
            if (await ProductClientService.IsProductExist(order.ProductId))
            {
                await ProductClientService.UpdateAmount(order.ProductId, order.ProductAmount);
                context.Order.Add(order);
                context.SaveChanges();
            }
        }

        public Order? GetOrder(int id)
        {
            return context.Order.FirstOrDefault(o => o.Id == id);
        }
    }
}
