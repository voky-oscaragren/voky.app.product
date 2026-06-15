using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Services;

public class PriceMatrixService(DbPriceMatrixService dbPriceMatrixService)
{
    public async Task<IEnumerable<PriceMatrix>> GetAllAsync() =>
        await dbPriceMatrixService.GetAllAsync();

    public async Task<PriceMatrix?> GetByIdAsync(Guid id) =>
        await dbPriceMatrixService.GetByIdAsync(id);

    public async Task<PriceMatrix> CreateAsync(CreatePriceMatrixDto dto)
    {
        var priceMatrix = new PriceMatrix
        {
            ProductId = dto.ProductId,
            SupplierCurrencyId = dto.SupplierCurrencyId,
            CurrencyEndPriceId = dto.CurrencyEndPriceId,
            EndCustPrice = dto.EndCustPrice,
            MOQ = dto.MOQ,
            SupplierNetPrice = dto.SupplierNetPrice,
        };

        return await dbPriceMatrixService.CreateAsync(priceMatrix);
    }
}
