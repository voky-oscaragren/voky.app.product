using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Data.Services;

public class DbSupplierCurrencyService(AppDbContext db)
{
    public async Task<IEnumerable<SupplierCurrency>> GetAllAsync() =>
        await db.SupplierCurrencies.AsNoTracking().ToListAsync();

    public async Task<SupplierCurrency?> GetByIdAsync(int id) =>
        await db.SupplierCurrencies.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

    public async Task<SupplierCurrency> CreateAsync(SupplierCurrency currency)
    {
        db.SupplierCurrencies.Add(currency);
        await db.SaveChangesAsync();
        return currency;
    }
}
