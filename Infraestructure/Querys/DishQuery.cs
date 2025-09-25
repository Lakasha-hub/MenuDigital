using Aplication.DTOs.Dish;
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

        public async Task<List<Dish>> GetAllDishes(DishFilterRequest filter)
        {
            var query = _context.Dish.Include(d => d.CategoryDb).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(d => d.Name.ToLower().Contains(filter.Name.Trim().ToLower()));
            }

            if (filter.Category.HasValue)
            {
                query = query.Where(d => d.Category == filter.Category);
            }

            if (filter.OnlyActive == true)
            {
                query = query.Where(d => d.Available);
            }

            query = filter.SortByPrice?.ToLower() switch
            {
                "asc" => query.OrderBy(d => d.Price),
                "desc" => query.OrderByDescending(d => d.Price),
                _ => query
            };

            return await query.ToListAsync();
        }

        public Dish GetDishByName(string name)
        {
            return _context.Dish.FirstOrDefault(d => d.Name == name);
        }

        public Dish GetDishById(Guid id)
        {
            return _context.Dish.Include(d => d.CategoryDb).FirstOrDefault(d => d.DishId == id);
        }
    }
}
