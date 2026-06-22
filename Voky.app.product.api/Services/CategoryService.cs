using Voky.app.product.api.Data.Services;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Services;

public class CategoryService(DbCategoryService dbCategoryService)
{
    public void UseTenant(string tenantId) => dbCategoryService.UseTenant(tenantId);

    public async Task<IEnumerable<Category>> GetAllAsync() =>
        await dbCategoryService.GetAllAsync();

    public async Task<Category?> GetByIdAsync(int id) =>
        await dbCategoryService.GetByIdAsync(id);
}
