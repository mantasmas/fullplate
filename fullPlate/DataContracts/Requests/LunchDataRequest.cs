using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.DataContracts.Requests
{
    public class LunchDataRequest
    {
        public DateTime lunchDate { get; set; }
        public DateTime lastRegisterDate { get; set; }
        public bool paidByCompany { get; set; }
        public bool enabled { get; set; }
        public bool deleted { get; set; }
        public List<int> dishIds { get; set; }
    }
}
