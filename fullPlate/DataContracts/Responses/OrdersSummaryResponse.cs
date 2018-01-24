using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.DataContracts.Responses
{
    public class OrdersSummaryResponse
    {
        public List<OneOrderData> soupOrders { get; set; }
        public List<OneOrderData> mainMealOrders { get; set; }
        public double totalLunchPrice { get; set; }
    }

    public class OneOrderData
    {
        public string dishName { get; set; }
        public int ordersCount { get; set; }
    }
}
