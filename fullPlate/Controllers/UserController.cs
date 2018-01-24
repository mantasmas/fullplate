using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using fullPlate.Data;
using fullPlate.Data.Models;
using fullPlate.DataContracts.Requests;
using fullPlate.DataContracts.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace fullPlate.Controllers
{
    [Route("/api/user")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly FullPlateContext _dbContext;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, FullPlateContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {

            User user = await _userManager.FindByNameAsync(request.username);


            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(request.username, request.password, isPersistent: true, lockoutOnFailure: false);

            if (!result.Succeeded || user.Disabled)
            {
                return Unauthorized();
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.username)
            };

            var userIdentity = new ClaimsIdentity(claims, "login");

            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);

            return Ok(new
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserId = user.Id,
                Role = roles[0],
                LoggedIn = true
            });
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            await _signInManager.SignOutAsync();
            return Ok(new { message = "success" });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            User user = new User
            {
                Email = request.email,
                UserName = request.username,
                FirstName = request.firstName,
                LastName = request.lastName
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.password);

            if (result.Errors.Any())
            {
                return BadRequest(new ErrorResponse { errorMessage = "Wrong input fields" });
            }


            User usr = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(usr, "EMPLOYEE");

            return Ok(new { message = "success" });
        }
    }
}
