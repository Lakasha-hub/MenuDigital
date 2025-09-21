using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Order
{
    public class OrderCreateResponse
    {
        public int OrderNumber { get; set; }
        public double TotalAmount { get; set; }
        public string CreatedAt { get; set; }
    }
}
