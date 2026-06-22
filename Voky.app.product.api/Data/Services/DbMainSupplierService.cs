using Microsoft.EntityFrameworkCore;
using Voky.Integration.Order.Visma.Database;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Data.Services;

public class DbMainSupplierService(VismaDbContext db)
{
    public async Task<IEnumerable<MainSupplier>> GetAllAsync() =>
        await db.MainSuppliers.AsNoTracking().ToListAsync();

    public async Task<MainSupplier?> GetByIdAsync(int supplierNr) =>
        await db.MainSuppliers.AsNoTracking().FirstOrDefaultAsync(s => s.SupplierNr == supplierNr);

    public async Task<MainSupplier> CreateAsync(MainSupplier supplier)
    {
        db.MainSuppliers.Add(supplier);
        await db.SaveChangesAsync();
        return supplier;
    }
}
