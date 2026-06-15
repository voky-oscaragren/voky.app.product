using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MainSuppliersController(MainSupplierService mainSupplierService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<MainSupplier>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var suppliers = await mainSupplierService.GetAllAsync();
        return Ok(suppliers);
    }

    [HttpGet("{supplierNr}")]
    [ProducesResponseType<MainSupplier>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string supplierNr)
    {
        var supplier = await mainSupplierService.GetByIdAsync(supplierNr);
        return supplier is null ? NotFound() : Ok(supplier);
    }

    [HttpPost]
    [ProducesResponseType<MainSupplier>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateMainSupplierDto dto)
    {
        var supplier = await mainSupplierService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { supplierNr = supplier.SupplierNr }, supplier);
    }
}
