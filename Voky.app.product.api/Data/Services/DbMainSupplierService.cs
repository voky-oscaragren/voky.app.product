using Microsoft.EntityFrameworkCore;
using Voky.Shared.Visma.Database;

namespace Voky.app.product.api.Data.Services;

public class DbMainSupplierService(VokyDbContextFactory<VismaDbContext> contextFactory) : DbBaseService(contextFactory)
{
    public async Task<IEnumerable<MainSupplier>> GetAllAsync()
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.MainSuppliers.AsNoTracking().Where(s => s.SupplierNr != 0).ToListAsync();
    }

    public async Task<MainSupplier?> GetByIdAsync(int supplierNr)
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.MainSuppliers.AsNoTracking().FirstOrDefaultAsync(s => s.SupplierNr == supplierNr);
    }
}
