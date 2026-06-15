using Microsoft.AspNetCore.Mvc;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;
using Voky.app.product.api.Services;

namespace Voky.app.product.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController(ProductService productService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<Product>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var products = await productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{productNr}")]
    [ProducesResponseType<Product>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string productNr)
    {
        var product = await productService.GetByIdAsync(productNr);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    [ProducesResponseType<Product>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var product = await productService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { productNr = product.ProductNr }, product);
    }

    [HttpPut("{productNr}")]
    [ProducesResponseType<Product>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(string productNr, [FromBody] UpdateProductDto dto)
    {
        var product = await productService.UpdateAsync(productNr, dto);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpDelete("{productNr}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string productNr)
    {
        var deleted = await productService.DeleteAsync(productNr);
        return deleted ? NoContent() : NotFound();
    }
}
