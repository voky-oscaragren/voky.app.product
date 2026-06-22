using Microsoft.EntityFrameworkCore;
using Voky.Shared.Visma.Database;

namespace Voky.app.product.api.Data.Services;

public class DbQuestionGroupService(VokyDbContextFactory<VismaDbContext> contextFactory) : DbBaseService(contextFactory)
{
    public async Task<IEnumerable<QuestionGroup>> GetAllAsync()
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.QuestionGroups.AsNoTracking().ToListAsync();
    }

    public async Task<QuestionGroup?> GetByIdAsync(int id)
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.QuestionGroups.AsNoTracking().FirstOrDefaultAsync(g => g.QuestionGroupId == id);
    }

    public async Task<QuestionGroup> CreateAsync(QuestionGroup group)
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        dbContext.QuestionGroups.Add(group);
        await dbContext.SaveChangesAsync();
        return group;
    }
}
