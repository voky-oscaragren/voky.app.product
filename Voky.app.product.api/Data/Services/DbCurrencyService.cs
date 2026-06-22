using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Data.Services;

public class DbCurrencyService(VismaDbContext db)
{
    public async Task<IEnumerable<Currency>> GetAllAsync() =>
        await db.Currencies.AsNoTracking().ToListAsync();

    public async Task<Currency?> GetByIdAsync(int currencyNr) =>
        await db.Currencies.AsNoTracking().FirstOrDefaultAsync(c => c.CurrencyNr == currencyNr);
}
