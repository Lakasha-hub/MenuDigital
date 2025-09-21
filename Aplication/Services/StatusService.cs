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
    public class StatusService : IStatusService
    {
        private readonly IStatusQuery _query;

        public StatusService(IStatusQuery query)
        {
            _query = query;
        }

        public async Task<List<GenericResponse>> GetAll()
        {
            var statuses = _query.GetAllStatus();
            var result = statuses.Select(s => new GenericResponse
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
            return result;
        }
    }
}
