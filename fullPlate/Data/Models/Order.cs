using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int? SoupId { get; set; }
        public Dish Soup { get; set; }

        public int? MainDishId { get; set; }
        public Dish MainDish { get; set; }

        public int LunchId { get; set; }
        public LunchDay LunchDay { get; set; }

        public bool Deleted { get; set; }
    }
}
