using Aplication.DTOs.Dish;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Services
{
    public interface IDishService
    {
        Task<DishResponse> Create(DishRequest dish);
        Task<DishResponse> Update(Guid id, DishUpdateRequest req);
        Task<List<DishResponse>> GetAll(DishFilterRequest filter);
        Task<DishResponse> GetById(Guid id);
        Task<DishResponse> Delete(Guid id);
    }
}
