using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Data.Services;

public class DbCurrencyEndPriceService(AppDbContext db)
{
    public async Task<IEnumerable<CurrencyEndPrice>> GetAllAsync() =>
        await db.CurrencyEndPrices.AsNoTracking().ToListAsync();

    public async Task<CurrencyEndPrice?> GetByIdAsync(int id) =>
        await db.CurrencyEndPrices.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

    public async Task<CurrencyEndPrice> CreateAsync(CurrencyEndPrice currency)
    {
        db.CurrencyEndPrices.Add(currency);
        await db.SaveChangesAsync();
        return currency;
    }
}
