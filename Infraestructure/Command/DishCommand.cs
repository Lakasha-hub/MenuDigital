using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces.Command;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Command
{
    public class DishCommand : IDishCommand
    {
        private readonly MenuDigitalContext _context;

        public DishCommand(MenuDigitalContext context)
        {
            _context = context;
        }

        public async Task InsertDish(Dish dish)
        {
            _context.Add(dish);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDish(Dish dish)
        {
            _context.Update(dish);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDish(Guid dishId)
        {
            await _context.Dish.Where(d => d.DishId == dishId)
                .ExecuteUpdateAsync(s => s.SetProperty(d => d.Available, false));
        }
    }
}
