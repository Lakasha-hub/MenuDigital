using Aplication.DTOs;
using Aplication.Interfaces.Querys;
using Aplication.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class DeliveryTypeService : IDeliveryTypeService
    {
        private readonly IDeliveryTypeQuery _query;

        public DeliveryTypeService(IDeliveryTypeQuery query)
        {
            _query = query;
        }

        public async Task<List<GenericResponse>> GetAll()
        {
            var deliveryTypes = _query.GetAllDeliveryTypes();
            var result = deliveryTypes.Select(dt => new GenericResponse
            {
                Id = dt.Id,
                Name = dt.Name
            }).ToList();
            return result;
        }
    }
}
