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
    public class StatusQuery : IStatusQuery
    {
        private readonly MenuDigitalContext _context;

        public StatusQuery(MenuDigitalContext context)
        {
            _context = context;
        }

        public IEnumerable<Status> GetAllStatus()
        {
            return _context.Status.ToList();
        }
    }
}
