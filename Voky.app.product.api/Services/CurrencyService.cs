using Voky.app.product.api.Data;
using Voky.app.product.api.Data.Services;

namespace Voky.app.product.api.Services;

public class CurrencyService(DbCurrencyService dbCurrencyService)
{
    public void UseTenant(string tenantId) => dbCurrencyService.UseTenant(tenantId);

    public async Task<IEnumerable<Currency>> GetAllAsync() =>
        await dbCurrencyService.GetAllAsync();

    public async Task<Currency?> GetByIdAsync(int currencyNr) =>
        await dbCurrencyService.GetByIdAsync(currencyNr);
}
