using Aplication.DTOs.Order;
using Aplication.Interfaces.Command;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

        public async Task RecalculateOrderStatus(int orderId)
        {
            var AllStatusOfOrderItems = await _context.OrderItem
                .Where(oi => oi.Order == orderId)
                .Select(oi => oi.Status)
                .ToListAsync();

            if (!AllStatusOfOrderItems.Any())
            {
                await _context.Order
                    .Where(o => o.OrderId == orderId)
                    .ExecuteUpdateAsync(s => s.SetProperty(o => o.OverallStatus, 1));
                return;
            }

            var newStatus = AllStatusOfOrderItems.Min();
            await _context.Order
                .Where (o => o.OrderId == orderId)
                .ExecuteUpdateAsync(s => s.SetProperty(s => s.OverallStatus, newStatus));
        }
    }
}
