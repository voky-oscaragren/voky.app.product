using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Data.Services;

public class DbQuestionChoiceService(AppDbContext db)
{
    public async Task<IEnumerable<QuestionChoice>> GetAllAsync() =>
        await db.QuestionChoices.AsNoTracking().ToListAsync();

    public async Task<QuestionChoice?> GetByIdAsync(Guid id) =>
        await db.QuestionChoices.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

    public async Task<QuestionChoice> CreateAsync(QuestionChoice choice)
    {
        db.QuestionChoices.Add(choice);
        await db.SaveChangesAsync();
        return choice;
    }
}
