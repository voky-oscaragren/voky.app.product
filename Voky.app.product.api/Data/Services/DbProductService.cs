using Microsoft.EntityFrameworkCore;
using Voky.Shared.Visma.Database;

namespace Voky.app.product.api.Data.Services;

public class DbProductService(VokyDbContextFactory<VismaDbContext> contextFactory) : DbBaseService(contextFactory)
{
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.Products.AsNoTracking().Where(p => p.ProductNr.StartsWith("w") ).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(string productNr)
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductNr == productNr);
    }

    public async Task<IEnumerable<Product>> GetBySupplierAsync(int supplierNr)
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.Products.AsNoTracking()
            .Where(p => p.ProductNr.StartsWith("w") && p.MainSupplierNr == supplierNr && p.LifeCycle != 7)
            .ToListAsync();
    }
}


