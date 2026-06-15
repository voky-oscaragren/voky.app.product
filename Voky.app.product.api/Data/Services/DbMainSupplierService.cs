using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Data.Services;

public class DbMainSupplierService(AppDbContext db)
{
    public async Task<IEnumerable<MainSupplier>> GetAllAsync() =>
        await db.MainSuppliers.AsNoTracking().ToListAsync();

    public async Task<MainSupplier?> GetByIdAsync(string supplierNr) =>
        await db.MainSuppliers.AsNoTracking().FirstOrDefaultAsync(s => s.SupplierNr == supplierNr);

    public async Task<MainSupplier> CreateAsync(MainSupplier supplier)
    {
        db.MainSuppliers.Add(supplier);
        await db.SaveChangesAsync();
        return supplier;
    }
}
