using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.Data;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("{tenantId}/api/[controller]")]
[Produces("application/json")]
public class MainSuppliersController(MainSupplierService mainSupplierService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<MainSupplier>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromRoute] string tenantId)
    {
        mainSupplierService.UseTenant(tenantId);
        return Ok(await mainSupplierService.GetAllAsync());
    }

    [HttpGet("{supplierNr:int}")]
    [ProducesResponseType<MainSupplier>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] string tenantId, int supplierNr)
    {
        mainSupplierService.UseTenant(tenantId);
        var supplier = await mainSupplierService.GetByIdAsync(supplierNr);
        return supplier is null ? NotFound() : Ok(supplier);
    }
}
