using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Data.Services;

public class DbQuestionGroupService(AppDbContext db)
{
    public async Task<IEnumerable<QuestionGroup>> GetAllAsync() =>
        await db.QuestionGroups.AsNoTracking().ToListAsync();

    public async Task<QuestionGroup?> GetByIdAsync(Guid id) =>
        await db.QuestionGroups.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);

    public async Task<QuestionGroup> CreateAsync(QuestionGroup group)
    {
        db.QuestionGroups.Add(group);
        await db.SaveChangesAsync();
        return group;
    }
}
