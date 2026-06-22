using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Services;

public class MainSupplierService(DbMainSupplierService dbMainSupplierService)
{
    public void UseTenant(string tenantId) => dbMainSupplierService.UseTenant(tenantId);

    public async Task<IEnumerable<MainSupplier>> GetAllAsync() =>
        await dbMainSupplierService.GetAllAsync();

    public async Task<MainSupplier?> GetByIdAsync(int supplierNr) =>
        await dbMainSupplierService.GetByIdAsync(supplierNr);

    public async Task<SupplierCostDto?> GetCostDetailsAsync(int supplierNr) =>
        await dbMainSupplierService.GetCostDetailsAsync(supplierNr);
}
