using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.Data.Models
{
    public class FridayLunch
    {
        public int Id { get; set; }

        [Required]
        public DateTime LunchDate { get; set; }

        [Required]
        public DateTime LastRegisterDate { get; set; }

        public bool Enabled { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }
    }
}
