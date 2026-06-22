using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.Data;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("{tenantId}/api/[controller]")]
[Produces("application/json")]
public class QuestionTypesController(QuestionTypeService questionTypeService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<QuestionType>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromRoute] string tenantId)
    {
        questionTypeService.UseTenant(tenantId);
        return Ok(await questionTypeService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<QuestionType>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] string tenantId, int id)
    {
        questionTypeService.UseTenant(tenantId);
        var type = await questionTypeService.GetByIdAsync(id);
        return type is null ? NotFound() : Ok(type);
    }
}
