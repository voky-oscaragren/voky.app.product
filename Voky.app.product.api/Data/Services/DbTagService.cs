using Microsoft.EntityFrameworkCore;
using Voky.Integration.Order.Visma.Database;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Data.Services;

public class DbTagService(VismaDbContext db)
{
    public async Task<IEnumerable<Tag>> GetAllAsync() =>
        await db.Tags.AsNoTracking().ToListAsync();

    public async Task<Tag?> GetByIdAsync(int id) =>
        await db.Tags.AsNoTracking().FirstOrDefaultAsync(t => t.TagId == id);
}
