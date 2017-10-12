using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.DataContracts.Responses
{
    public class RestaurantResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public List<DishResponse> Dishes { get; set; }
    }
}
