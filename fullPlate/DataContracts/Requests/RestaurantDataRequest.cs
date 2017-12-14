using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.DataContracts.Requests
{
    public class RestaurantDataRequest
    {
        public string name { get; set; }
        public string address { get; set; }
        public string telephoneNumber { get; set; }
    }
}
