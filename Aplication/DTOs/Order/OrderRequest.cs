using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Order
{
    public class OrderRequest
    {
        [Required(ErrorMessage = "los items son requeridos")]
        [MinLength(1, ErrorMessage = "El pedido debe contener al menos un ítem.")]
        public List<Item> Items { get; set; }

        [Required(ErrorMessage = "el tipo de delivery es requerido")]
        public Delivery Delivery { get; set; }

        public string? Notes { get; set; }
    }
}
