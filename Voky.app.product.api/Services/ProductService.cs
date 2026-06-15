using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Services;

public class ProductService(DbProductService dbProductService)
{
    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await dbProductService.GetAllAsync();

    public async Task<Product?> GetByIdAsync(string productNr) =>
        await dbProductService.GetByIdAsync(productNr);

    public async Task<Product> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            ProductNr = dto.ProductNr,
            Name = dto.Name,
            LevArtNrName = dto.LevArtNrName,
            ArtNrVarianthead = dto.ArtNrVarianthead,
            Description = dto.Description,
            SendToOpti = dto.SendToOpti,
            ArtNrStartCost = dto.ArtNrStartCost,
            StartCostAmount = dto.StartCostAmount,
            MainSupplierId = dto.MainSupplierId,
            LifecycleId = dto.LifecycleId,
            QuestionGroupId = dto.QuestionGroupId,
        };

        return await dbProductService.CreateAsync(product);
    }

    public async Task<Product?> UpdateAsync(string productNr, UpdateProductDto dto)
    {
        var product = new Product
        {
            ProductNr = productNr,
            Name = dto.Name,
            LevArtNrName = dto.LevArtNrName,
            ArtNrVarianthead = dto.ArtNrVarianthead,
            Description = dto.Description,
            SendToOpti = dto.SendToOpti,
            ArtNrStartCost = dto.ArtNrStartCost,
            StartCostAmount = dto.StartCostAmount,
            MainSupplierId = dto.MainSupplierId,
            LifecycleId = dto.LifecycleId,
            QuestionGroupId = dto.QuestionGroupId,
        };

        return await dbProductService.UpdateAsync(product);
    }

    public async Task<bool> DeleteAsync(string productNr) =>
        await dbProductService.DeleteAsync(productNr);
}
