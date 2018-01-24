using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.Data.Models;
using fullPlate.DataContracts.Requests;
using fullPlate.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace fullPlate.Controllers
{
    [Route("/api/lunch")]
    public class LunchController : Controller
    {
        private readonly ILunchService _lunchService;
        private readonly UserManager<User> _userManager;

        public LunchController(ILunchService lunchService, UserManager<User> userManager)
        {
            _lunchService = lunchService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetAllLunches()
        {
            var lunches = _lunchService.GetAllLunches();
            return Ok(lunches);
        }

        [HttpGet("available-lunches")]
        public IActionResult GetAvailableLunches()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            var availableLunches = _lunchService.GetAvailableLunches(userId);

            return Ok(availableLunches);
        }

        [HttpGet("available-dishes")]
        public IActionResult GetAllAvailableDishes()
        {
            var dishes = _lunchService.GetAllDishes();
            return Ok(dishes);
        }

        [HttpGet("{lunchId}")]
        public IActionResult GetOneLunch(int lunchId)
        {
            var lunch = _lunchService.GetOneLunch(lunchId);
            return Ok(lunch);
        }

        [HttpPost]
        public IActionResult RegisterLunch([FromBody] LunchDataRequest request)
        {
            var lunches = _lunchService.CreateNewLuch(request);
            return Ok(lunches);
        }

        [HttpDelete("{lunchId}")]
        public IActionResult DeleteLunch(int lunchId)
        {
            var removeResult = _lunchService.RemoveLunch(lunchId);
            return Ok(removeResult);
        }

        [HttpPut("{lunchId}/toggle-enable")]
        public IActionResult ToggleEnableLunch(int lunchId)
        {
            var enableResult = _lunchService.EnableLunch(lunchId);
            return Ok(enableResult);
        }
    }
}
