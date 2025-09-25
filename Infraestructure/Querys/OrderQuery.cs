using Aplication.Interfaces.Querys;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Querys
{
    public class OrderQuery : IOrderQuery
    {
        private readonly MenuDigitalContext _context;

        public OrderQuery(MenuDigitalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Order.Include(o => o.DeliveryTypeDb)
                .Include(o => o.StatusDb)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.DishDb)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.StatusDb)
                .ToListAsync();
        }

        public Order GetOrderById(int id)
        {
            return _context.Order.Include(o => o.StatusDb)
                .Include(o => o.DeliveryTypeDb)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.StatusDb)
                .Include(oi => oi.OrderItems)
                    .ThenInclude(oi => oi.DishDb)
                .FirstOrDefault(o => o.OrderId == id);
        }

        public async Task<bool> IsDishInOrderActive(Guid dishId)
        {
            return await _context.OrderItem
                .AnyAsync(oi => oi.Dish == dishId && (oi.Status == 1 || oi.Status == 2));
        }
    }
}
