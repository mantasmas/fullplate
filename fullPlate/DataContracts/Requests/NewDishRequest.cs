using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.DataContracts.Enums;

namespace fullPlate.DataContracts.Requests
{
    public class NewDishRequest
    {
        public string name { get; set; }
        public decimal price { get; set; }
        public DishType dishType { get; set; }
        public bool isVegetarian { get; set; }
    }
}
