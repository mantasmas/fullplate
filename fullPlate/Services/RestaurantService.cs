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
                .Include(x => x.Dishes)
                .Where(x => x.Deleted == false)
                .Select(x => new RestaurantResponse
                    {
                        id = x.Id,
                        name = x.Name,
                        address = x.Address,
                        telephoneNumber = x.TelephoneNumber,
                        dishes = x.Dishes.Select(y => new DishResponse
                        {
                            id = y.Id,
                            name = y.Name,
                            dishType = y.DishType,
                            price = y.Price,
                            isVegetarian = y.IsVegetarian
                        })
                        .ToList()
                    }
                )
                .ToList();
        }

        public RestaurantResponse GetOneRestaurant(int restaurantId)
        {
            return _dbContext
                .Restaurants
                .Where(x => x.Id.Equals(restaurantId) && x.Deleted == false)
                .OrderBy(x => x.Name)
                .Select(x => new RestaurantResponse
                    {
                        id = x.Id,
                        name = x.Name,
                        address = x.Address,
                        telephoneNumber = x.TelephoneNumber,
                        dishes = x.Dishes.Select(y => new DishResponse
                        {
                            id = y.Id,
                            name = y.Name,
                            dishType = y.DishType,
                            price = y.Price,
                            isVegetarian = y.IsVegetarian
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
            Restaurant restaurant = new Restaurant
            {
                Id = restaurantId,
                Deleted = true
            };

            _dbContext.Restaurants.Update(restaurant);
            _dbContext.SaveChanges();

            return true;
        }

        public RestaurantDishesResponse GetRestaurantDishes(int restaurantId)
        {
            List<DishResponse>  dishes = _dbContext.Dishes
                .Where(x => x.Restaurant.Id == restaurantId && x.Deleted == false)
                .OrderBy(x => x.DishType)
                .Select(x => new DishResponse
                {
                    id = x.Id,
                    name = x.Name,
                    price = x.Price,
                    dishType = x.DishType,
                    isVegetarian = x.IsVegetarian
                })
                .ToList();

            Restaurant restaurant = _dbContext.Restaurants
                .SingleOrDefault(x => x.Id.Equals(restaurantId));

            return new RestaurantDishesResponse { restaurantName = restaurant.Name, dishes = dishes};
        }

        public DishResponse AddNewDish(int restaurantId, DishDataRequest dishData)
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

            restaurant.Dishes.Add(newDish);

            _dbContext.SaveChanges();

            return new DishResponse
            {
                id = newDish.Id,
                name = newDish.Name,
                price = newDish.Price,
                dishType = newDish.DishType,
                isVegetarian = newDish.IsVegetarian
            };
        }

        public DishResponse UpdateDish(int dishId, DishDataRequest dishData)
        {
            Dish dish = new Dish
            {
                Id = dishId,
                Name = dishData.name,
                Price = dishData.price,
                DishType = dishData.dishType,
                IsVegetarian = dishData.isVegetarian
            };

            _dbContext.Dishes.Update(dish);
            _dbContext.SaveChanges();

            return new DishResponse
            {
                id = dish.Id,
                name = dish.Name,
                price = dish.Price,
                isVegetarian = dish.IsVegetarian
            };
        }

        public bool DeleteDish(int dishId)
        {
            Dish dish = new Dish
            {
                Id = dishId,
                Deleted = true
            };

            _dbContext.Dishes.Update(dish);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
