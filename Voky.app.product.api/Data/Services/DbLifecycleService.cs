using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Data.Services;

public class DbLifecycleService(AppDbContext db)
{
    public async Task<IEnumerable<Lifecycle>> GetAllAsync() =>
        await db.Lifecycles.AsNoTracking().ToListAsync();

    public async Task<Lifecycle?> GetByIdAsync(int id) =>
        await db.Lifecycles.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);

    public async Task<Lifecycle> CreateAsync(Lifecycle lifecycle)
    {
        db.Lifecycles.Add(lifecycle);
        await db.SaveChangesAsync();
        return lifecycle;
    }
}
