using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.DataContracts.Requests;
using fullPlate.DataContracts.Responses;

namespace fullPlate.Services.Interfaces
{
    public interface IRestaurantsService
    {
        List<RestaurantResponse> GetAll();
        RestaurantResponse GetOneRestaurant(int restaurantId);
        RestaurantResponse AddNewRestaurant(RestaurantDataRequest restaurantData);
        RestaurantResponse UpdateRestaurant(int id, RestaurantDataRequest restaurantData);
        bool RemoveRestaurant(int restaurantId);
        RestaurantDishesResponse GetRestaurantDishes(int restaurantId);
        DishResponse AddNewDish(int restaurantId, DishDataRequest restaurant);
        DishResponse UpdateDish(int dishId, DishDataRequest dishData);
        bool DeleteDish(int dishId);
    }
}
