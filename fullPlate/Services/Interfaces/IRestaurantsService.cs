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
        List<RestaurantResponse>GetAll();
        RestaurantResponse GetOneRestaurant(int restaurantId);
        RestaurantResponse AddNewRestaurant(RestaurantDataRequest restaurantData);
        RestaurantResponse UpdateRestaurant(int id, RestaurantDataRequest restaurantData);
        DishResponse AddNewDish(int restaurantId, NewDishRequest restaurant);
        bool RemoveRestaurant(int restaurantId);
    }
}
