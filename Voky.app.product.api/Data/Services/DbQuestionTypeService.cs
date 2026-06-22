using Microsoft.EntityFrameworkCore;
using Voky.Shared.Visma.Database;

namespace Voky.app.product.api.Data.Services;

public class DbQuestionTypeService(VokyDbContextFactory<VismaDbContext> contextFactory) : DbBaseService(contextFactory)
{
    public async Task<IEnumerable<QuestionType>> GetAllAsync()
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.QuestionTypes.AsNoTracking().ToListAsync();
    }

    public async Task<QuestionType?> GetByIdAsync(int id)
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.QuestionTypes.AsNoTracking().FirstOrDefaultAsync(t => t.QuestionTypeId == id);
    }
}
