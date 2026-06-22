using Voky.app.product.api.Data.Services;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Services;

public class QuestionTypeService(DbQuestionTypeService dbQuestionTypeService)
{
    public async Task<IEnumerable<QuestionType>> GetAllAsync() =>
        await dbQuestionTypeService.GetAllAsync();

    public async Task<QuestionType?> GetByIdAsync(string id) =>
        await dbQuestionTypeService.GetByIdAsync(id);
}
