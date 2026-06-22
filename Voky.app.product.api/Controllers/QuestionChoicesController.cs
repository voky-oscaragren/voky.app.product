using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Data;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("{tenantId}/api/[controller]")]
[Produces("application/json")]
public class QuestionChoicesController(QuestionChoiceService questionChoiceService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<QuestionChoice>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromRoute] string tenantId)
    {
        questionChoiceService.UseTenant(tenantId);
        return Ok(await questionChoiceService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<QuestionChoice>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] string tenantId, int id)
    {
        questionChoiceService.UseTenant(tenantId);
        var choice = await questionChoiceService.GetByIdAsync(id);
        return choice is null ? NotFound() : Ok(choice);
    }

    [HttpPost]
    [ProducesResponseType<QuestionChoice>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromRoute] string tenantId, [FromBody] CreateQuestionChoiceDto dto)
    {
        questionChoiceService.UseTenant(tenantId);
        var choice = await questionChoiceService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { tenantId, id = choice.QuestionChoiceId }, choice);
    }
}
