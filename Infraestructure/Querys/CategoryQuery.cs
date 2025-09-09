using Aplication.Interfaces.Querys;
using Domain.Entities;
using Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Querys
{
    public class CategoryQuery : ICategoryQuery
    {
        private readonly MenuDigitalContext _context;

        public CategoryQuery(MenuDigitalContext context)
        {
            _context = context;
        }

        public Category GetCategoryById(int id)
        {
            var category = _context.Category.FirstOrDefault(c => c.Id == id);
            return category;
        }
    }
}
