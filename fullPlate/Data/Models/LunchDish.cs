using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.Data.Models
{
    public class LunchDish
    {
        public int LunchDayId { get; set; }
        public LunchDay LunchDay { get; set; }
        public int DishId { get; set; }
        public Dish Dish { get; set; }
    }
}
