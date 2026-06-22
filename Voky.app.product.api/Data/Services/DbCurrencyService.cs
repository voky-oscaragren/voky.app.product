using Microsoft.EntityFrameworkCore;
using Voky.Shared.Visma.Database;

namespace Voky.app.product.api.Data.Services;

public class DbCurrencyService(VokyDbContextFactory<VismaDbContext> contextFactory) : DbBaseService(contextFactory)
{
    public async Task<IEnumerable<Currency>> GetAllAsync()
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.Currencies.AsNoTracking().ToListAsync();
    }

    public async Task<Currency?> GetByIdAsync(int currencyNr)
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.Currencies.AsNoTracking().FirstOrDefaultAsync(c => c.CurrencyNr == currencyNr);
    }
}
