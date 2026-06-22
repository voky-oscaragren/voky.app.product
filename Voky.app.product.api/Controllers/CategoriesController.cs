using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.Data;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoriesController(CategoryService categoryService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<Category>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var categories = await categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<Category>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await categoryService.GetByIdAsync(id);
        return category is null ? NotFound() : Ok(category);
    }
}
