using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.Data;
using fullPlate.Data.Models;
using fullPlate.DataContracts.Requests;
using fullPlate.DataContracts.Responses;
using fullPlate.Services.Interfaces;

namespace fullPlate.Services
{
    public class OrderService : IOrderService
    {
        private readonly FullPlateContext _dbContext;

        public OrderService(FullPlateContext dbContext)
        {
            _dbContext = dbContext;
        }

        public OrdersSummaryResponse GetOrdersSummary(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public bool SaveOrder(int lunchId, OrderDataRequest requestData, string userId)
        {
            Order order = new Order
            {
                Date = new DateTime(),
                SoupId = requestData.soupId,
                MainDishId = requestData.mainDishId,
                LunchId = lunchId,
                UserId = userId
            };

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return true;
        }

        public bool RemoveOrder(int orderId)
        {
            Order order = new Order
            {
                Id = orderId,
                Deleted = true
            };

            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
