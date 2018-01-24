using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fullPlate.Data.Models;
using fullPlate.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace fullPlate.Controllers
{
    [Route("/api/users")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly UserManager<User> _userManager;

        public UsersController(IUsersService usersService, UserManager<User> userManager)
        {
            _usersService = usersService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _usersService.GetAllUsers();
            return Ok(users);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DisableUser(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            user.Disabled = true;

            await _userManager.UpdateAsync(user);

            return Ok(true);
        }
    }
}
