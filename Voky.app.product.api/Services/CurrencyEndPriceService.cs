using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Services;

public class CurrencyEndPriceService(DbCurrencyEndPriceService dbCurrencyEndPriceService)
{
    public async Task<IEnumerable<CurrencyEndPrice>> GetAllAsync() =>
        await dbCurrencyEndPriceService.GetAllAsync();

    public async Task<CurrencyEndPrice?> GetByIdAsync(int id) =>
        await dbCurrencyEndPriceService.GetByIdAsync(id);

    public async Task<CurrencyEndPrice> CreateAsync(CreateCurrencyEndPriceDto dto)
    {
        var currency = new CurrencyEndPrice { Name = dto.Name };
        return await dbCurrencyEndPriceService.CreateAsync(currency);
    }
}
