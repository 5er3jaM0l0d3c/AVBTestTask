using Grpc.Core;
using OrderEntities;
using OrderServices.Interface;

namespace OrderAPI.Services
{
    public class OrderService : OrderProto.OrderProtoBase, IOrder
    {
        private OrderServiceDbContext context {  get; set; }

        public OrderService(OrderServiceDbContext context)
        {
            this.context = context;
        }
        public Task AddOrder(OrderEntities.Order order)
        {
            throw new NotImplementedException();
        }

        public OrderEntities.Order? GetOrder(int id)
        {
            return context.Order.FirstOrDefault(x => x.Id == id);
        }

        public override Task<Order> Get_Order(Order_Id request, ServerCallContext cotext)
        {
            var orderId = request.ProductId;

            var order = GetOrder(orderId);
            if (order == null) 
            {
                order = new();
            }
            return Task.FromResult(new Order { Id = order.Id, ProductAmount = order.ProductAmount, ProductId = orderId });
        }

    }
}
