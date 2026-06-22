using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.Data;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController(ProductService productService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<Product>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var products = await productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{productNr}")]
    [ProducesResponseType<Product>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string productNr)
    {
        var product = await productService.GetByIdAsync(productNr);
        return product is null ? NotFound() : Ok(product);
    }
}
