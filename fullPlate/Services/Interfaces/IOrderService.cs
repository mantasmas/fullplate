using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.DataContracts.Requests;
using fullPlate.DataContracts.Responses;

namespace fullPlate.Services.Interfaces
{
    public interface IOrderService
    {
        OrdersSummaryResponse GetOrdersSummary(int restaurantId);
        bool SaveOrder(int lunchId, OrderDataRequest requestData, string userId);
        bool RemoveOrder(int orderId);
    }
}
