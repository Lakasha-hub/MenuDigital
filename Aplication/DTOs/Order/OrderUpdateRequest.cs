using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Order
{
    public class OrderUpdateRequest
    {
        public List<Item>? Items { get; set; }
    }
}
