using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class QuestionsController(QuestionService questionService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<Question>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var questions = await questionService.GetAllAsync();
        return Ok(questions);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<Question>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var question = await questionService.GetByIdAsync(id);
        return question is null ? NotFound() : Ok(question);
    }

    [HttpPost]
    [ProducesResponseType<Question>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateQuestionDto dto)
    {
        var question = await questionService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = question.QuestionId }, question);
    }
}
