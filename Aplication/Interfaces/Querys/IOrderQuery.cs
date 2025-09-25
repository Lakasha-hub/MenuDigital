using Aplication.DTOs.Order;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Querys
{
    public interface IOrderQuery
    {
        Task<List<Order>> GetAllOrders(OrderFilterRequest filter);
        Order GetOrderById(int id);
        Task<bool> IsDishInOrderActive(Guid orderId);
    }
}
