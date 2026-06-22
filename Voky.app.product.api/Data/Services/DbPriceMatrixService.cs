using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Data.Services;

public class DbPriceMatrixService(VismaDbContext db)
{
    public async Task<IEnumerable<PriceMatrix>> GetAllAsync() =>
        await db.PriceMatrices.AsNoTracking().ToListAsync();

    public async Task<PriceMatrix?> GetByIdAsync(int id) =>
        await db.PriceMatrices.AsNoTracking().FirstOrDefaultAsync(p => p.LineNo == id);
}
