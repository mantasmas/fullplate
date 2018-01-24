using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.DataContracts.Requests
{
    public class OrderDataRequest
    {
        public int soupId { get; set; }
        public int mainDishId { get; set; }
    }
}
