using Voky.app.product.api.Data.Services;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Services;

public class TagService(DbTagService dbTagService)
{
    public void UseTenant(string tenantId) => dbTagService.UseTenant(tenantId);

    public async Task<IEnumerable<Tag>> GetAllAsync() =>
        await dbTagService.GetAllAsync();

    public async Task<Tag?> GetByIdAsync(int id) =>
        await dbTagService.GetByIdAsync(id);
}
