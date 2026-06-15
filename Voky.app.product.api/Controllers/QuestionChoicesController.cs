using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class QuestionChoicesController(QuestionChoiceService questionChoiceService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<QuestionChoice>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var choices = await questionChoiceService.GetAllAsync();
        return Ok(choices);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<QuestionChoice>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var choice = await questionChoiceService.GetByIdAsync(id);
        return choice is null ? NotFound() : Ok(choice);
    }

    [HttpPost]
    [ProducesResponseType<QuestionChoice>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateQuestionChoiceDto dto)
    {
        var choice = await questionChoiceService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = choice.QuestionChoiceId }, choice);
    }
}
