using Microsoft.EntityFrameworkCore;
using Voky.Shared.Visma.Database;

namespace Voky.app.product.api.Data.Services;

public class DbPriceMatrixService(VokyDbContextFactory<VismaDbContext> contextFactory) : DbBaseService(contextFactory)
{
    public async Task<IEnumerable<PriceMatrix>> GetAllAsync(string? productNr = null)
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        var query = dbContext.PriceMatrices.AsNoTracking();
        if (productNr is not null)
            query = query.Where(p => p.ProductNr == productNr);
        return await query.ToListAsync();
    }

    public async Task<PriceMatrix?> GetByIdAsync(int id)
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.PriceMatrices.AsNoTracking().FirstOrDefaultAsync(p => p.LineNo == id);
    }
}
