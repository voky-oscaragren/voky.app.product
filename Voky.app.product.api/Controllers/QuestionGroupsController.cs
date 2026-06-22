using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Data;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("{tenantId}/api/[controller]")]
[Produces("application/json")]
public class QuestionGroupsController(QuestionGroupService questionGroupService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<QuestionGroup>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromRoute] string tenantId)
    {
        questionGroupService.UseTenant(tenantId);
        return Ok(await questionGroupService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<QuestionGroup>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] string tenantId, int id)
    {
        questionGroupService.UseTenant(tenantId);
        var group = await questionGroupService.GetByIdAsync(id);
        return group is null ? NotFound() : Ok(group);
    }

    [HttpPost]
    [ProducesResponseType<QuestionGroup>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromRoute] string tenantId, [FromBody] CreateQuestionGroupDto dto)
    {
        questionGroupService.UseTenant(tenantId);
        var group = await questionGroupService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { tenantId, id = group.QuestionGroupId }, group);
    }
}
