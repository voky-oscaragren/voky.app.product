using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.Data;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class QuestionTypesController(QuestionTypeService questionTypeService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<QuestionType>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var types = await questionTypeService.GetAllAsync();
        return Ok(types);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<QuestionType>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var type = await questionTypeService.GetByIdAsync(id);
        return type is null ? NotFound() : Ok(type);
    }
}
