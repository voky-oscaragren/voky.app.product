using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Services;

public class ProductService(DbProductService dbProductService)
{
    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await dbProductService.GetAllAsync();

    public async Task<Product?> GetByIdAsync(Guid id) =>
        await dbProductService.GetByIdAsync(id);

    public async Task<Product> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
        };

        return await dbProductService.CreateAsync(product);
    }

    public async Task<Product?> UpdateAsync(Guid id, UpdateProductDto dto)
    {
        var product = new Product
        {
            Id = id,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
        };

        return await dbProductService.UpdateAsync(product);
    }

    public async Task<bool> DeleteAsync(Guid id) =>
        await dbProductService.DeleteAsync(id);
}
