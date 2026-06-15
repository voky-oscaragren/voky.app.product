using Voky.app.product.api.Data.Services;
using Voky.app.product.api.DTOs;
using Voky.app.product.api.Models;
using Voky.app.product.api.Services;
using Voky.app.product.api.tests.Helpers;
using Voky.app.product.api.Data;

namespace Voky.app.product.api.tests.Services;

public class QuestionServiceTests
{
    private static async Task<(QuestionService service, AppDbContext db, int groupId)> SetupAsync()
    {
        var db = TestDbContextFactory.Create();
        var group = new QuestionGroup { Name = "General" };
        db.QuestionGroups.Add(group);
        await db.SaveChangesAsync();

        var dbService = new DbQuestionService(db);
        var service = new QuestionService(dbService);
        return (service, db, group.QuestionGroupId);
    }

    private static CreateQuestionDto SampleDto(int groupId) => new(
        QuestionGroupId: groupId,
        QuestionTypeId: null,
        Mandatory: true,
        NameSwedish: "Vilken färg?",
        NameEnglish: "Which color?"
    );

    [Fact]
    public async Task CreateAsync_MapsAllDtoFieldsToQuestion()
    {
        var (service, db, groupId) = await SetupAsync();

        var created = await service.CreateAsync(SampleDto(groupId));

        Assert.Equal("Which color?", created.NameEnglish);
        Assert.Equal("Vilken färg?", created.NameSwedish);
        Assert.True(created.Mandatory);
        db.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllQuestions()
    {
        var (service, db, groupId) = await SetupAsync();
        await service.CreateAsync(SampleDto(groupId));
        await service.CreateAsync(new CreateQuestionDto(groupId, null, false, "Storlek?", "Size?"));

        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count());
        db.Dispose();
    }

    [Fact]
    public async Task GetByIdAsync_ExistingQuestion_ReturnsQuestion()
    {
        var (service, db, groupId) = await SetupAsync();
        var created = await service.CreateAsync(SampleDto(groupId));

        var result = await service.GetByIdAsync(created.QuestionId);

        Assert.NotNull(result);
        Assert.Equal("Which color?", result.NameEnglish);
        db.Dispose();
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingQuestion_ReturnsNull()
    {
        var (service, db, _) = await SetupAsync();

        var result = await service.GetByIdAsync(9999);

        Assert.Null(result);
        db.Dispose();
    }
}
