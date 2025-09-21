using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Querys
{
    public interface IStatusQuery
    {
        IEnumerable<Status> GetAllStatus();
    }
}
