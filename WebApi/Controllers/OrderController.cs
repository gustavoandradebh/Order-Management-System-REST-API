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
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetOrderById([FromQuery] int orderId)
        {
            try
            {
                var order = _orderService.FindById(orderId);
                if (order is null)
                    return NotFound();

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on GetOrderById: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = UserRoles.PowerUser)]
        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderPostDto order)
        {
            try
            {
                var createdOrder = _orderService.CreateOrder(order);
                return Created(createdOrder.Id.ToString(), createdOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on CreateOrder: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
