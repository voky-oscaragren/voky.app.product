using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.Data;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("{tenantId}/api/[controller]")]
[Produces("application/json")]
public class CurrenciesController(CurrencyService currencyService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<Currency>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromRoute] string tenantId)
    {
        currencyService.UseTenant(tenantId);
        return Ok(await currencyService.GetAllAsync());
    }

    [HttpGet("{currencyNr:int}")]
    [ProducesResponseType<Currency>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] string tenantId, int currencyNr)
    {
        currencyService.UseTenant(tenantId);
        var currency = await currencyService.GetByIdAsync(currencyNr);
        return currency is null ? NotFound() : Ok(currency);
    }
}
