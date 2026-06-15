using Microsoft.EntityFrameworkCore;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Data.Services;

public class DbQuestionService(AppDbContext db)
{
    public async Task<IEnumerable<Question>> GetAllAsync() =>
        await db.Questions.AsNoTracking().ToListAsync();

    public async Task<Question?> GetByIdAsync(int id) =>
        await db.Questions.AsNoTracking().FirstOrDefaultAsync(q => q.QuestionId == id);

    public async Task<Question> CreateAsync(Question question)
    {
        db.Questions.Add(question);
        await db.SaveChangesAsync();
        return question;
    }
}
