using Microsoft.EntityFrameworkCore;
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

        public async Task AddOrder(Order order)
        {
            if (await ProductClientService.IsProductExist(order.ProductId))
            {
                await ProductClientService.UpdateAmount(order.ProductId, order.ProductAmount);
                await context.Order.AddAsync(order);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Order?> GetOrder(int id)
        {
            return await context.Order.FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
