using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.DataContracts.Responses
{
    public class LunchResponse
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public DateTime lastRegistrationTime { get; set; }
        public bool paidByCompany { get; set; }
        public bool enabled { get; set; }
        public List<DishResponse> dishes { get; set; }
        public int userOrderId { get; set; }
    }
}
