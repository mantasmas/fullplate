using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.DataContracts.Responses
{
    public class OneLunchResponse
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public DateTime lastRegistrationTime { get; set; }
        public bool paidByCompany { get; set; }
        public bool enabled { get; set; }
        public List<DishResponse> soups { get; set; }
        public List<DishResponse> mainDishes { get; set; }
    }
}
