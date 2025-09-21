using Aplication.DTOs.Dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Order
{
    public class OrderItemResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
        public GenericResponse Status { get; set; }
        public DishShortResponse Dish { get; set; }
    }
}
