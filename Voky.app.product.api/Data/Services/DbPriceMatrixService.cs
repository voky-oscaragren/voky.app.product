using Microsoft.EntityFrameworkCore;
using Voky.Integration.Order.Visma.Database;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Data.Services;

public class DbPriceMatrixService(VismaDbContext db)
{
    public async Task<IEnumerable<PriceMatrix>> GetAllAsync() =>
        await db.PriceMatrices.AsNoTracking().ToListAsync();

    public async Task<PriceMatrix?> GetByIdAsync(Guid id) =>
        await db.PriceMatrices.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
}
