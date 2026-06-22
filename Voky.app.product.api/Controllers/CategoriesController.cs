using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.Data;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("{tenantId}/api/[controller]")]
[Produces("application/json")]
public class CategoriesController(CategoryService categoryService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<Category>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromRoute] string tenantId)
    {
        categoryService.UseTenant(tenantId);
        return Ok(await categoryService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<Category>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] string tenantId, int id)
    {
        categoryService.UseTenant(tenantId);
        var category = await categoryService.GetByIdAsync(id);
        return category is null ? NotFound() : Ok(category);
    }
}
