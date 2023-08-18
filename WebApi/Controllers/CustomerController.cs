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
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll([FromQuery] int? pageSize, [FromQuery] int? pageNumber)
        {
            var customers = _customerService.GetAll(pageSize, pageNumber);

            return Ok(customers);
        }
    }
}
