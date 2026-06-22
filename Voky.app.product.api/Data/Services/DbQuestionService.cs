using Microsoft.EntityFrameworkCore;
using Voky.Integration.Order.Visma.Database;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Data.Services;

public class DbQuestionService(VismaDbContext db)
{
    public async Task<IEnumerable<Question>> GetAllAsync() =>
        await db.Questions.AsNoTracking().ToListAsync();

    public async Task<Question?> GetByIdAsync(int id) =>
        await db.Questions.AsNoTracking().FirstOrDefaultAsync(q => q.QuestionId == id);
}
