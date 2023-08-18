using Domain.Auth;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll([FromQuery] int? pageSize, [FromQuery] int? pageNumber)
        {
            try
            {
                var customers = _customerService.GetAll(pageSize, pageNumber);
                return Ok(customers);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error on GetAll Customers: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
