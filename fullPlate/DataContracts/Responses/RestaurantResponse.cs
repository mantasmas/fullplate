using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.DataContracts.Responses
{
    public class RestaurantResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string telephoneNumber { get; set; }
        public string address { get; set; }
        public List<DishResponse> dishes { get; set; }
    }
}
