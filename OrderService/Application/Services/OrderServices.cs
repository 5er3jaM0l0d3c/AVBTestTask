using Microsoft.EntityFrameworkCore;
using OrderEntities;
using OrderService.Application.Interfaces;
using OrderService.Grpc.Service;
using OrderServiceDbContext = OrderService.Infrastructure.DbContexts.OrderServiceDbContext;

namespace OrderService.Application.Services
{
    public class OrderServices : IOrder
    {
        private OrderServiceDbContext context { get; set; }

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

        public async Task<Order> GetOrder(int id)
        {
            return await context.Order.FirstOrDefaultAsync(o => o.Id == id) ?? throw new InvalidOperationException("Order with Id = " + id + " not found");
        }
    }
}
