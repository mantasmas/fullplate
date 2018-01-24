using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.Data.Models
{
    public class LunchDay
    {
        public int Id { get; set; }

        [Required]
        public DateTime LunchDate { get; set; }

        [Required]
        public DateTime LastRegisterDate { get; set; }

        public bool PaidByCompany { get; set; }
        public bool Enabled { get; set; }
        public bool Deleted { get; set; }

        public List<LunchDish> LunchDayDishes { get; set; }
        public List<Order> Orders { get; set; }
    }
}
