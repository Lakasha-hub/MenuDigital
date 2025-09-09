using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Dish
{
    public class DishUpdateRequest
    {
        [Required(ErrorMessage = "name es obligatorio")]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "price es obligatorio")]
        [Range(0.01, double.MaxValue)]
        public double Price { get; set; }

        [Required(ErrorMessage = "category es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "category debe ser un número positivo")]
        public int Category { get; set; }

        public string? Image { get; set; }

        [Required(ErrorMessage = "isActive es obligatorio")]
        public bool IsActive { get; set; }
    }
}
