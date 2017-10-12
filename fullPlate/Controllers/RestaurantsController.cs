using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.DataContracts.Responses;
using fullPlate.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
