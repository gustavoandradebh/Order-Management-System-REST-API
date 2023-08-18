using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetProductById([FromQuery]int productId)
    {
       var product = _productService.FindById(productId);
        if (product is null)
            return NotFound();

        return Ok(product);
    }
}
