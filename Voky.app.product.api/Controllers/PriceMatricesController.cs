using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.Data;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("{tenantId}/api/[controller]")]
[Produces("application/json")]
public class PriceMatricesController(PriceMatrixService priceMatrixService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<PriceMatrix>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromRoute] string tenantId, [FromQuery] string? productNr = null)
    {
        priceMatrixService.UseTenant(tenantId);
        return Ok(await priceMatrixService.GetAllAsync(productNr));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<PriceMatrix>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] string tenantId, int id)
    {
        priceMatrixService.UseTenant(tenantId);
        var matrix = await priceMatrixService.GetByIdAsync(id);
        return matrix is null ? NotFound() : Ok(matrix);
    }
}
