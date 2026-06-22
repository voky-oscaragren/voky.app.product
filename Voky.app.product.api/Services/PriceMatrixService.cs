using Voky.app.product.api.Data.Services;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Services;

public class PriceMatrixService(DbPriceMatrixService dbPriceMatrixService)
{
    public async Task<IEnumerable<PriceMatrix>> GetAllAsync() =>
        await dbPriceMatrixService.GetAllAsync();

    public async Task<PriceMatrix?> GetByIdAsync(Guid id) =>
        await dbPriceMatrixService.GetByIdAsync(id);
}
