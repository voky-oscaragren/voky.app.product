using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Services;

public class MainSupplierService(DbMainSupplierService dbMainSupplierService)
{
    public async Task<IEnumerable<MainSupplier>> GetAllAsync() =>
        await dbMainSupplierService.GetAllAsync();

    public async Task<MainSupplier?> GetByIdAsync(string supplierNr) =>
        await dbMainSupplierService.GetByIdAsync(supplierNr);

    public async Task<MainSupplier> CreateAsync(CreateMainSupplierDto dto)
    {
        var supplier = new MainSupplier
        {
            SupplierNr = dto.SupplierNr,
            Name = dto.Name,
            Email = dto.Email,
            BankAccountGiro = dto.BankAccountGiro,
        };

        return await dbMainSupplierService.CreateAsync(supplier);
    }
}
