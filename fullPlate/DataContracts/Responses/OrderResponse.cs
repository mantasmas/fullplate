using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.DataContracts.Responses
{
    public class OrderResponse
    {
        public int id { get; set; }
        public string soupName { get; set; }
        public string mainDishName { get; set; }
        public string userName { get; set; }
    }
}
