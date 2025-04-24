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

        public async override Task<Empty_Response> Add_Order(Order request, ServerCallContext context)
        {
            OrderEntities.Order order = new()
            {
                ProductAmount = request.ProductAmount,
                ProductId = request.ProductId,
            };

            await AddOrder(order);

            return await Task.FromResult(new Empty_Response());
        }

        public async Task AddOrder(OrderEntities.Order order)
        {
            if(await ProductClientService.IsProductExist(order.ProductId))
            {
                await ProductClientService.UpdateAmount(order.ProductId, order.ProductAmount);
                context.Order.Add(order);
                context.SaveChanges();
            }
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
