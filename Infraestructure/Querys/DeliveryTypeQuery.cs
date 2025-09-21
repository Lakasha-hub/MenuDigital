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
    public class DeliveryTypeQuery : IDeliveryTypeQuery
    {
        private MenuDigitalContext _context;

        public DeliveryTypeQuery(MenuDigitalContext context)
        {
            _context = context;
        }

        public IEnumerable<DeliveryType> GetAllDeliveryTypes()
        {
            return _context.DeliveryType.ToList();
        }
    }
}
