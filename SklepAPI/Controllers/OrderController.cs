using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SklepAPI.Models;
using SklepAPI.Services;
using System.Security.Claims;

namespace SklepAPI.Controllers
{
    [Route("Order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("NewOrder")]
        [Authorize(Roles = "User")]
        public ActionResult NewOrder([FromBody] NewOrderDto dto)
        {
            int userId = int.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            _orderService.NewOrder(dto, userId);
            return Ok();
        }

        [HttpGet("GetUserOrders")]
        [Authorize(Roles)]

    }
}
