using Domain.Auth;
using Domain.DTOs;
using Domain.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetOrderById([FromQuery] int orderId)
        {
            var order = _orderService.FindById(orderId);
            if (order is null)
                return NotFound();

            return Ok(order);
        }

        [Authorize(Roles = UserRoles.PowerUser)]
        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderPostDto order)
        {
            var createdOrder = _orderService.CreateOrder(order);
            return Ok();
        }
    }
}
