using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.Data;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("{tenantId}/api/[controller]")]
[Produces("application/json")]
public class QuestionsController(QuestionService questionService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<Question>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromRoute] string tenantId)
    {
        questionService.UseTenant(tenantId);
        return Ok(await questionService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<Question>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] string tenantId, int id)
    {
        questionService.UseTenant(tenantId);
        var question = await questionService.GetByIdAsync(id);
        return question is null ? NotFound() : Ok(question);
    }
}
