using Aplication.Interfaces.Querys;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class CategoryService
    {
        private readonly ICategoryQuery _query;

        public CategoryService(ICategoryQuery query)
        {
            _query = query;
        }

        public Category GetById(int id)
        {
            var category = _query.GetCategoryById(id);
            if(category == null)
            {
                throw new Exception("Category not found");
            }
            return category;
        }

    }
}
