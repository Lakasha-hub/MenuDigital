using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Order
{
    public class OrderFilterRequest
    {
        public DateTime? From { get; set; }
        
        public DateTime? To { get; set; }

        [Range(1, 5, ErrorMessage = "status no válido")]
        public int? Status { get; set; }
    }
}
