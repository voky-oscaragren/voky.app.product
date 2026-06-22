using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.Services;

public class QuestionChoiceService(DbQuestionChoiceService dbQuestionChoiceService)
{
    public async Task<IEnumerable<QuestionChoice>> GetAllAsync() =>
        await dbQuestionChoiceService.GetAllAsync();

    public async Task<QuestionChoice?> GetByIdAsync(int id) =>
        await dbQuestionChoiceService.GetByIdAsync(id);

    public async Task<QuestionChoice> CreateAsync(CreateQuestionChoiceDto dto)
    {
        var choice = new QuestionChoice
        {
            QuestionId = dto.QuestionId,
            NameSwedish = dto.NameSwedish,
            NameEnglish = dto.NameEnglish,
            ProductNumber = dto.ProductNumber,
        };

        return await dbQuestionChoiceService.CreateAsync(choice);
    }
}
