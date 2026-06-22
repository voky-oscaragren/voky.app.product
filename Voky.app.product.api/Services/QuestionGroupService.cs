using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Services;

public class QuestionGroupService(DbQuestionGroupService dbQuestionGroupService)
{
    public async Task<IEnumerable<QuestionGroup>> GetAllAsync() =>
        await dbQuestionGroupService.GetAllAsync();

    public async Task<QuestionGroup?> GetByIdAsync(int id) =>
        await dbQuestionGroupService.GetByIdAsync(id);

    public async Task<QuestionGroup> CreateAsync(CreateQuestionGroupDto dto)
    {
        var group = new QuestionGroup { Name = dto.Name };
        return await dbQuestionGroupService.CreateAsync(group);
    }
}
