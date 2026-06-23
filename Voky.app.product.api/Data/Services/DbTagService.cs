using Microsoft.EntityFrameworkCore;
using Voky.Shared.Visma.Database;

namespace Voky.app.product.api.Data.Services;

public class DbTagService(VokyDbContextFactory<VismaDbContext> contextFactory) : DbBaseService(contextFactory)
{
    public async Task<IEnumerable<Tag>> GetAllAsync()
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.Tags.AsNoTracking().Where(t => t.TextType == 20007).ToListAsync();
    }

    public async Task<Tag?> GetByIdAsync(int id)
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.Tags.AsNoTracking().FirstOrDefaultAsync(t => t.TagId == id);
    }
}
