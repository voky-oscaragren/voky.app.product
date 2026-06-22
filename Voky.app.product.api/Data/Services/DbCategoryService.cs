using Microsoft.EntityFrameworkCore;
using Voky.Integration.Order.Visma.Database;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Data.Services;

public class DbCategoryService(VismaDbContext db)
{
    public async Task<IEnumerable<Category>> GetAllAsync() =>
        await db.Categories.AsNoTracking().ToListAsync();

    public async Task<Category?> GetByIdAsync(int id) =>
        await db.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
}
