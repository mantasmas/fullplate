using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.DataContracts.Responses
{
    public class RestaurantDishesResponse
    {
        public string restaurantName { get; set; }
        public List<DishResponse> dishes { get; set; }
    }
}
