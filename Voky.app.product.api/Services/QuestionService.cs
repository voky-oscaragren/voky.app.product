using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;

namespace Voky.app.product.api.Services;

public class QuestionService(DbQuestionService dbQuestionService)
{
    public async Task<IEnumerable<Question>> GetAllAsync() =>
        await dbQuestionService.GetAllAsync();

    public async Task<Question?> GetByIdAsync(Guid id) =>
        await dbQuestionService.GetByIdAsync(id);

    public async Task<Question> CreateAsync(CreateQuestionDto dto)
    {
        var question = new Question
        {
            QuestionGroupId = dto.QuestionGroupId,
            QuestionTypeId = dto.QuestionTypeId,
            Mandatory = dto.Mandatory,
            NameSwedish = dto.NameSwedish,
            NameEnglish = dto.NameEnglish,
        };

        return await dbQuestionService.CreateAsync(question);
    }
}
