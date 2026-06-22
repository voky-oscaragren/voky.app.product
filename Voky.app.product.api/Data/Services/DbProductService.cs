using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Data.Services;

public class DbProductService(VismaDbContext db)
{
    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await db.Products.AsNoTracking().ToListAsync();

    public async Task<Product?> GetByIdAsync(string productNr) =>
        await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductNr == productNr);
}
