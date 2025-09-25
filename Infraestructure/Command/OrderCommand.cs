using Aplication.DTOs.Order;
using Aplication.Interfaces.Command;
using Domain.Entities;
using Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Command
{
    public class OrderCommand : IOrderCommand
    {
        private readonly MenuDigitalContext _context;

        public OrderCommand(MenuDigitalContext context)
        {
            _context = context;
        }

        public async Task InsertOrder(Order order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderItem(UpdateOrderItemParams newParams)
        {
            var orderItem = _context.OrderItem.FirstOrDefault(oi => oi.Order == newParams.OrderId
            && oi.OrderItemId == newParams.OrderItemId);
            orderItem.Status = newParams.Status;
            await _context.SaveChangesAsync();
        }
    }
}
