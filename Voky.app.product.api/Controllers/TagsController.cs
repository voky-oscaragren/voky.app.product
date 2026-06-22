using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.Data;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("{tenantId}/api/[controller]")]
[Produces("application/json")]
public class TagsController(TagService tagService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<Tag>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromRoute] string tenantId)
    {
        tagService.UseTenant(tenantId);
        return Ok(await tagService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<Tag>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] string tenantId, int id)
    {
        tagService.UseTenant(tenantId);
        var tag = await tagService.GetByIdAsync(id);
        return tag is null ? NotFound() : Ok(tag);
    }
}
