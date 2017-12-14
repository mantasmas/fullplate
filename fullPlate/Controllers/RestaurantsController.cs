using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using fullPlate.DataContracts.Requests;
using fullPlate.DataContracts.Responses;
using fullPlate.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace fullPlate.Controllers
{
    [Route("/api/restaurants")]
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantsService _restaurantsService;

        public RestaurantsController(IRestaurantsService restaurantsManager)
        {
            _restaurantsService = restaurantsManager;
        }

        [HttpGet]
        public IActionResult GetAllRestaurants()
        {
            List<RestaurantResponse> allRestaurants = _restaurantsService.GetAll();

            return Ok(allRestaurants);
        }

        [HttpGet("{id}")]
        public IActionResult GetOneRestaurant(int id)
        {
            RestaurantResponse foundRestaurant;

            try
            {
                foundRestaurant = _restaurantsService.GetOneRestaurant(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { errorMessage = "Cannot find restaurant", errorCause = ex.Data.ToString()});
            }

            return Ok(foundRestaurant);
        }

        [HttpPost]
        public IActionResult SaveNewRestaurant([FromBody]RestaurantDataRequest request)
        {
            RestaurantResponse response;
            try
            {
                response = _restaurantsService.AddNewRestaurant(request);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse { errorMessage = "Restaurant with same name already exists!", errorCause = ex.Data.ToString() });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { errorMessage = "Problems encountered when saving a restaurant!", errorCause = ex.Data.ToString() });
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRestaurant(int id, [FromBody]RestaurantDataRequest request)
        {
            RestaurantResponse response;
            try
            {
                response = _restaurantsService.UpdateRestaurant(id, request);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { errorMessage = "Problems encountered when saving a restaurant!", errorCause = ex.Data.ToString() });
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurant(int id)
        {
            try
            {
                _restaurantsService.RemoveRestaurant(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { errorMessage = "Cannot remove restaurant", errorCause = ex.Data.ToString() });
            }

            return Ok(new {Ok = true});
        }

        [HttpGet("{restaurantId}/dishes")]
        public IActionResult GetRestaurantDishes(int restaurantId)
        {
            RestaurantDishesResponse response = _restaurantsService.GetRestaurantDishes(restaurantId);

            return Ok(response);
        }

        [HttpPost("{restaurantId}/dishes")]
        public IActionResult SaveNewDish(int restaurantId, [FromBody]DishDataRequest request)
        {
            DishResponse response = _restaurantsService.AddNewDish(restaurantId, request);

            return Ok(response);
        }

        [HttpPut("{restaurantId}/dishes/{dishId}")]
        public IActionResult UpdateDish(int dishId, [FromBody]DishDataRequest request)
        {
            DishResponse response = _restaurantsService.UpdateDish(dishId, request);

            return Ok(response);
        }

        [HttpDelete("{restaurantId}/dishes/{dishId}")]
        public IActionResult DeleteDish(int dishId)
        {
            _restaurantsService.DeleteDish(dishId);

            return Ok(new {Ok=true});
        }
    }
}
