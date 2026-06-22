using Microsoft.EntityFrameworkCore;
using Voky.Integration.Order.Visma.Database;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Data.Services;

public class DbQuestionTypeService(VismaDbContext db)
{
    public async Task<IEnumerable<QuestionType>> GetAllAsync() =>
        await db.QuestionTypes.AsNoTracking().ToListAsync();

    public async Task<QuestionType?> GetByIdAsync(string id) =>
        await db.QuestionTypes.AsNoTracking().FirstOrDefaultAsync(t => t.QuestionTypeId == id);
}
