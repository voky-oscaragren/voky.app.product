using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SupplierCurrenciesController(SupplierCurrencyService supplierCurrencyService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<SupplierCurrency>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var currencies = await supplierCurrencyService.GetAllAsync();
        return Ok(currencies);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<SupplierCurrency>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var currency = await supplierCurrencyService.GetByIdAsync(id);
        return currency is null ? NotFound() : Ok(currency);
    }

    [HttpPost]
    [ProducesResponseType<SupplierCurrency>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateSupplierCurrencyDto dto)
    {
        var currency = await supplierCurrencyService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = currency.Id }, currency);
    }
}
