using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Order
{
    public class UpdateOrderItemParams
    {
        public int OrderId { get; set; }
        public int OrderItemId { get; set; }
        public int Status { get; set; }
    }
}
