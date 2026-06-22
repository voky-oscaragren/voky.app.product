using Voky.app.product.api.Data.Services;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Services;

public class MainSupplierService(DbMainSupplierService dbMainSupplierService)
{
    public async Task<IEnumerable<MainSupplier>> GetAllAsync() =>
        await dbMainSupplierService.GetAllAsync();

    public async Task<MainSupplier?> GetByIdAsync(int supplierNr) =>
        await dbMainSupplierService.GetByIdAsync(supplierNr);
}
