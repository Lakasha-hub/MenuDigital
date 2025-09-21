using Aplication.DTOs.Category;
using Aplication.DTOs.Dish;
using Aplication.Exceptions;
using Aplication.Interfaces.Querys;
using Aplication.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryQuery _query;

        public CategoryService(ICategoryQuery query)
        {
            _query = query;
        }

        public async Task<List<CategoryResponse>> GetAll()
        {
            var categories = _query.GetAllCategories();
            var result = categories.Select(c => new CategoryResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Order = c.Order
            }).ToList();
            return result;
        }
    }
}
