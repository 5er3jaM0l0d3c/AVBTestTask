using OrderEntities;

namespace OrderService.Application.Interfaces
{
    public interface IOrder
    {
        Task AddOrder(Order order);
        Task<Order> GetOrder(int id);
    }
}
