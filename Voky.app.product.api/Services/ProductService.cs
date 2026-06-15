using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Services;

public class ProductService(DbProductService dbProductService)
{
    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await dbProductService.GetAllAsync();
        return products.Select(ToDto);
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await dbProductService.GetByIdAsync(id);
        return product is null ? null : ToDto(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
        };

        var created = await dbProductService.CreateAsync(product);
        return ToDto(created);
    }

    public async Task<ProductDto?> UpdateAsync(Guid id, UpdateProductDto dto)
    {
        var product = new Product
        {
            Id = id,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
        };

        var updated = await dbProductService.UpdateAsync(product);
        return updated is null ? null : ToDto(updated);
    }

    public async Task<bool> DeleteAsync(Guid id) =>
        await dbProductService.DeleteAsync(id);

    private static ProductDto ToDto(Product p) =>
        new(p.Id, p.Name, p.Description, p.Price, p.Stock, p.CreatedAt, p.UpdatedAt);
}
