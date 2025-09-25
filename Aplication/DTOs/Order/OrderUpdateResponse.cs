using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Order
{
    public class OrderUpdateResponse
    {
        public int OrderNumber { get; set; }
        public double TotalAmount { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
