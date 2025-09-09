using Aplication.Interfaces.Querys;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Querys
{
    public class DishQuery : IDishQuery
    {
        private readonly MenuDigitalContext _context;

        public DishQuery(MenuDigitalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dish>> GetAllDishes()
        {
            return _context.Dish.Include(d => d.CategoryDb);
        }

        public Dish GetDishByName(string name)
        {
            return _context.Dish.FirstOrDefault(d => d.Name == name);
        }

        public Dish GetDishById(Guid id)
        {
            return _context.Dish.FirstOrDefault(d => d.DishId == id);
        }
    }
}
