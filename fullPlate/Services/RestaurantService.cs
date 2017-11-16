using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.Data;
using fullPlate.Data.Models;
using fullPlate.DataContracts.Responses;
using fullPlate.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.EntityFrameworkCore;

namespace fullPlate.Services
{
    public class RestaurantService : IRestaurantsService
    {
        private readonly FullPlateContext _dbContext;

        public RestaurantService(FullPlateContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public List<RestaurantResponse> GetAll()
        {
            return _dbContext
                .Restaurants
                .Select(x => new RestaurantResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Address = x.Address,
                        Number = x.TelephoneNumber
                    }
                )
                .ToList();
        }

        public RestaurantResponse AddNewRestaurant(string restaurantName)
        {
            Restaurant restaurant = new Restaurant
            {
                Name = restaurantName
            };
            
            if (_dbContext.Restaurants.Any(x => x.Name.Equals(restaurantName)))
            {
                throw new ArgumentException();
            }

            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return new RestaurantResponse {Id = restaurant.Id, Name = restaurant.Name};
        }

        public bool RemoveRestaurant(int restaurantId)
        {
            Restaurant restaurant = new Restaurant {Id = restaurantId};

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
