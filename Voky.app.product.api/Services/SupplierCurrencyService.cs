using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Services;

public class SupplierCurrencyService(DbSupplierCurrencyService dbSupplierCurrencyService)
{
    public async Task<IEnumerable<SupplierCurrency>> GetAllAsync() =>
        await dbSupplierCurrencyService.GetAllAsync();

    public async Task<SupplierCurrency?> GetByIdAsync(int id) =>
        await dbSupplierCurrencyService.GetByIdAsync(id);

    public async Task<SupplierCurrency> CreateAsync(CreateSupplierCurrencyDto dto)
    {
        var currency = new SupplierCurrency { Name = dto.Name };
        return await dbSupplierCurrencyService.CreateAsync(currency);
    }
}
