using Voky.app.product.api.Data.Services;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Services;

public class ProductService(DbProductService dbProductService)
{
    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await dbProductService.GetAllAsync();

    public async Task<Product?> GetByIdAsync(string productNr) =>
        await dbProductService.GetByIdAsync(productNr);
}
