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
        Task<IEnumerable<Dish>> GetAllDishes();
        Dish GetDishByName(string name);
        Dish GetDishById(Guid id);
    }
}
