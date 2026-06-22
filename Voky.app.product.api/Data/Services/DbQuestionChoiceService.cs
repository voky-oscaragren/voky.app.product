using Microsoft.EntityFrameworkCore;
using Voky.Shared.Visma.Database;

namespace Voky.app.product.api.Data.Services;

public class DbQuestionChoiceService(VokyDbContextFactory<VismaDbContext> contextFactory) : DbBaseService(contextFactory)
{
    public async Task<IEnumerable<QuestionChoice>> GetAllAsync()
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.QuestionChoices.AsNoTracking().ToListAsync();
    }

    public async Task<QuestionChoice?> GetByIdAsync(int id)
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        return await dbContext.QuestionChoices.AsNoTracking().FirstOrDefaultAsync(c => c.QuestionChoiceId == id);
    }

    public async Task<QuestionChoice> CreateAsync(QuestionChoice choice)
    {
        CheckTenant();
        await using var dbContext = _contextFactory.Create(_tenantId!);
        dbContext.QuestionChoices.Add(choice);
        await dbContext.SaveChangesAsync();
        return choice;
    }
}
