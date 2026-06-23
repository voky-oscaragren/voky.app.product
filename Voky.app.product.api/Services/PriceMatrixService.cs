using Voky.app.product.api.Data.Services;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Services;

public class PriceMatrixService(DbPriceMatrixService dbPriceMatrixService)
{
    public void UseTenant(string tenantId) => dbPriceMatrixService.UseTenant(tenantId);

    public async Task<IEnumerable<PriceMatrix>> GetAllAsync(string? productNr = null) =>
        await dbPriceMatrixService.GetAllAsync(productNr);

    public async Task<PriceMatrix?> GetByIdAsync(int id) =>
        await dbPriceMatrixService.GetByIdAsync(id);
}
