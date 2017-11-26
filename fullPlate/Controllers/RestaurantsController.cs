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
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse { ErrorMessage = "Cannot find restaurant" });
            }

            return Ok(foundRestaurant);
        }

        [HttpPost]
        public IActionResult SaveNewRestaurant([FromBody]NewRestaurantRequest request)
        {
            RestaurantResponse response;
            try
            {
                response = _restaurantsService.AddNewRestaurant(request.Title);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse {ErrorMessage = "Restaurant with same name already exists!"});
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { ErrorMessage = "Problems encountered when saving a restaurant!" });
            }

            return Ok(response);
        }

        [HttpPost("{restaurantId}/dishes")]
        public IActionResult SaveNewDish(int restaurantId, [FromBody]NewDishRequest request)
        {
            DishResponse response;
            try
            {
                response = _restaurantsService.AddNewDish(restaurantId, request);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorResponse { ErrorMessage = "Restaurant with same name already exists!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new ErrorResponse { ErrorMessage = "Problems encountered when saving a restaurant!" });
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
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse {ErrorMessage = "Cannot remove restaurant"});
            }

            return Ok(new {Ok = true});
        }
    }
}
