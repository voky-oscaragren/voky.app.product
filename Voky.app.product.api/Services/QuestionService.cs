using Voky.app.product.api.Data.Services;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Services;

public class QuestionService(DbQuestionService dbQuestionService)
{
    public async Task<IEnumerable<Question>> GetAllAsync() =>
        await dbQuestionService.GetAllAsync();

    public async Task<Question?> GetByIdAsync(int id) =>
        await dbQuestionService.GetByIdAsync(id);
}
