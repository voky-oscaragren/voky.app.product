using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Data;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class QuestionGroupsController(QuestionGroupService questionGroupService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<QuestionGroup>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var groups = await questionGroupService.GetAllAsync();
        return Ok(groups);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<QuestionGroup>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var group = await questionGroupService.GetByIdAsync(id);
        return group is null ? NotFound() : Ok(group);
    }

    [HttpPost]
    [ProducesResponseType<QuestionGroup>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateQuestionGroupDto dto)
    {
        var group = await questionGroupService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = group.QuestionGroupId }, group);
    }
}
