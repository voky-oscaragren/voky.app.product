using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.Data;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TagsController(TagService tagService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<Tag>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var tags = await tagService.GetAllAsync();
        return Ok(tags);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<Tag>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var tag = await tagService.GetByIdAsync(id);
        return tag is null ? NotFound() : Ok(tag);
    }
}
