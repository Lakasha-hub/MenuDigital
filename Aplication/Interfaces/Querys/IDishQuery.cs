using Aplication.DTOs.Dish;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Querys
{
    public interface IDishQuery
    {
        Task<List<Dish>> GetAllDishes(DishFilterRequest filter);
        Dish GetDishByName(string name);
        Dish GetDishById(Guid id);
    }
}
