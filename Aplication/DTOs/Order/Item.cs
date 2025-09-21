using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Order
{
    public class Item
    {
        [Required(ErrorMessage = "id del item es requerido")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "quantity del item es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "quantity debe ser un número positivo")]
        public int Quantity { get; set; }

        public string? Notes { get; set; }
    }
}
