using OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAPI.Application.Interfaces
{
    public interface IOrder
    {
        Task AddOrder(Order order);
        Task<Order?> GetOrder(int id);
    }
}
