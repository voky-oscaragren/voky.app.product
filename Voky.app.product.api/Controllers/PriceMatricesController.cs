using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PriceMatricesController(PriceMatrixService priceMatrixService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<PriceMatrix>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var prices = await priceMatrixService.GetAllAsync();
        return Ok(prices);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<PriceMatrix>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var price = await priceMatrixService.GetByIdAsync(id);
        return price is null ? NotFound() : Ok(price);
    }

    [HttpPost]
    [ProducesResponseType<PriceMatrix>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePriceMatrixDto dto)
    {
        var price = await priceMatrixService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = price.Id }, price);
    }
}
