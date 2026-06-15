using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LifecyclesController(LifecycleService lifecycleService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<Lifecycle>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var lifecycles = await lifecycleService.GetAllAsync();
        return Ok(lifecycles);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<Lifecycle>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var lifecycle = await lifecycleService.GetByIdAsync(id);
        return lifecycle is null ? NotFound() : Ok(lifecycle);
    }

    [HttpPost]
    [ProducesResponseType<Lifecycle>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateLifecycleDto dto)
    {
        var lifecycle = await lifecycleService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = lifecycle.Id }, lifecycle);
    }
}
