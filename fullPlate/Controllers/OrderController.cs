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
    [Route("/api/orders")] 
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;

        public OrderController(IOrderService orderService, UserManager<User> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        public IActionResult GetAllOrders()
        {
//            var lunches = _orderService.GetAllLunches();
            return Ok();
        }

        [HttpGet("lunch/{restaurantId}")]
        public IActionResult GetUserOrder(int restaurantId)
        {
            //            var lunches = _orderService.GetAllLunches();
            return Ok();
        }

        [HttpPost("lunch/{lunchId}")]
        public IActionResult SaveOrder(int lunchId, [FromBody] OrderDataRequest request)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            var saveSuccessful = _orderService.SaveOrder(lunchId, request, userId);

            return Ok(saveSuccessful);
        }

        [HttpPut("{restaurantId}/orders/{orderId}")]
        public IActionResult RemoveOrder(int orderId)
        {
            var removeSuccessful = _orderService.RemoveOrder(orderId);
            return Ok(removeSuccessful);
        }
    }
}
