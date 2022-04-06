using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SklepAPI.Entities;
using SklepAPI.Exceptions;
using SklepAPI.Models;
using SklepAPI.Services;
using SklepAPI.Tracking;
using SklepAPI.Tracking.TrackingModel;
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

        [HttpGet("GetUsersOrders")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<GetUserOrdersDto>> GetUsersOrders()
        {
            var Orders = _orderService.GetUsersOrders();
            return Ok(Orders);
        }

        [HttpGet("GetLoggedUserOrders")]
        [Authorize(Roles = "User")]
        public ActionResult<IEnumerable<GetUserOrdersDto>> GetLoggedUserOrders()
        {
            int userId;

            if (int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int id))
            {
                userId = id;
            }
            else
            {
                throw new NotFoundException("User not found");
            }

            var Orders = _orderService.GetLoggedUserOrders(userId);
            return Ok(Orders);
        }

        [HttpPut("ChangeOrderStatus/{orderId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult ChangeOrderStatus([FromRoute] int orderId, [FromQuery] OrderStatus status)
        {
            _orderService.ChangeOrderStatus(orderId, status);
            return Ok();
        }

        [HttpGet("GetTrackingInfo/{TrackingNumber}")]
        public ActionResult<IEnumerable<TrackingDto>> GetTrackingInfo([FromRoute] string TrackingNumber)
        {
            var Tacking = _orderService.GetTrackingInfo(TrackingNumber);
            return Ok(Tacking);
        }

    }
}
