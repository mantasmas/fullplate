using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.Data;
using fullPlate.Data.Models;
using fullPlate.DataContracts.Requests;
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
                        id = x.Id,
                        name = x.Name,
                        address = x.Address,
                        telephoneNumber = x.TelephoneNumber
                    }
                )
                .ToList();
        }

        public RestaurantResponse GetOneRestaurant(int restaurantId)
        {
            return _dbContext
                .Restaurants
                .Where(x => x.Id.Equals(restaurantId))
                .Select(x => new RestaurantResponse
                    {
                        id = x.Id,
                        name = x.Name,
                        address = x.Address,
                        telephoneNumber = x.TelephoneNumber,
                        dishes = x.Dishes.Select(y => new DishResponse
                        {
                            Id = y.Id,
                            Name = y.Name,
                            Type = y.DishType,
                            Price = y.Price,
                            IsVegetarian = y.IsVegetarian
                        })
                        .ToList()
                    }
                )
                .Single();
        }

        public RestaurantResponse AddNewRestaurant(RestaurantDataRequest restaurantData)
        {
            Restaurant restaurant = new Restaurant
            {
                Name = restaurantData.name,
                Address = restaurantData.address,
                TelephoneNumber = restaurantData.telephoneNumber
            };
            
            if (_dbContext.Restaurants.Any(x => x.Name.Equals(restaurantData.name)))
            {
                throw new ArgumentException();
            }

            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return new RestaurantResponse
            {
                id = restaurant.Id,
                name = restaurant.Name,
                address = restaurant.Address,
                telephoneNumber = restaurant.TelephoneNumber
            };
        }

        public RestaurantResponse UpdateRestaurant(int id, RestaurantDataRequest restaurantData)
        {
            Restaurant restaurant = new Restaurant
            {
                Id = id,
                Name = restaurantData.name,
                Address = restaurantData.address,
                TelephoneNumber = restaurantData.telephoneNumber
            };

            _dbContext.Restaurants.Update(restaurant);

            _dbContext.SaveChanges();

            return new RestaurantResponse
            {
                id = restaurant.Id,
                name = restaurant.Name,
                address = restaurant.Address,
                telephoneNumber = restaurant.TelephoneNumber
            };
        }

        public bool RemoveRestaurant(int restaurantId)
        {
            Restaurant restaurant = new Restaurant {Id = restaurantId};

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();

            return true;
        }

        public DishResponse AddNewDish(int restaurantId, NewDishRequest dishData)
        {
            Dish newDish = new Dish
            {
                Name = dishData.name,
                Price = dishData.price,
                DishType = dishData.dishType,
                IsVegetarian = dishData.isVegetarian
            };

            Restaurant restaurant = _dbContext.Restaurants
                .Include(x => x.Dishes)
                .Single(x => x.Id.Equals(restaurantId));

            Console.WriteLine(restaurant.Id);
            Console.WriteLine(restaurant.Name);

            restaurant.Dishes.Add(newDish);

            _dbContext.SaveChanges();

            return new DishResponse
            {
                Id = newDish.Id,
                Name = newDish.Name,
                Price = newDish.Price,
                Type = newDish.DishType,
                IsVegetarian = newDish.IsVegetarian
            };
        }
    }
}
