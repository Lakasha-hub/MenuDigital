using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Order
{
    public class Delivery
    {
        [Required(ErrorMessage = "id de delivery es requerido")]
        public int Id { get; set; }
        public string? To { get; set; }
    }
}
