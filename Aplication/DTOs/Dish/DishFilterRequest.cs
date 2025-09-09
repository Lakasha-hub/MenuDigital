using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.DTOs.Dish
{
    public class DishFilterRequest
    {
        public string? Name { get; set; }

        public int? Category { get; set; }

        [RegularExpression("^(asc|desc)$", ErrorMessage = "Parámetros de ordenamiento inválidos")]
        public string? SortByPrice { get; set; }

        public bool? OnlyActive { get; set; }
    }
}
