using Aplication.DTOs.Order;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Command
{
    public interface IOrderCommand
    {
        Task InsertOrder(Order order);
        Task UpdateOrder(Order order);
        Task UpdateOrderItem(UpdateOrderItemParams newParams);
        Task RecalculateOrderStatus(int orderId);
    }
}
