using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.DataContracts.Enums;

namespace fullPlate.DataContracts.Responses
{
    public class DishResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DishType Type { get; set; }
        public bool IsVegetarian { get; set; }
    }
}
