using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.DataContracts.Enums;

namespace fullPlate.Data.Models
{
    public class Dish
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DishType DishType { get; set; }

        [Required]
        public bool IsVegetarian { get; set; }
    }
}
