using Voky.app.product.api.Data.Services;
using Voky.app.product.api.Models;
using Voky.app.product.api.tests.Helpers;

namespace Voky.app.product.api.tests.DbServices;

public class DbQuestionServiceTests
{
    private static (QuestionGroup group, Question question) SampleData()
    {
        var group = new QuestionGroup { Name = "General" };
        var question = new Question
        {
            QuestionGroup = group,
            NameSwedish = "Vad är din färg?",
            NameEnglish = "What is your color?",
            Mandatory = true,
        };
        return (group, question);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllQuestions()
    {
        using var db = TestDbContextFactory.Create();
        var (group, q1) = SampleData();
        var q2 = new Question { QuestionGroup = group, NameSwedish = "Storlek?", NameEnglish = "Size?", Mandatory = false };
        db.QuestionGroups.Add(group);
        db.Questions.AddRange(q1, q2);
        await db.SaveChangesAsync();

        var service = new DbQuestionService(db);
        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ExistingQuestion_ReturnsQuestion()
    {
        using var db = TestDbContextFactory.Create();
        var (group, question) = SampleData();
        db.QuestionGroups.Add(group);
        db.Questions.Add(question);
        await db.SaveChangesAsync();
        var questionId = db.Questions.First().QuestionId;

        var service = new DbQuestionService(db);
        var result = await service.GetByIdAsync(questionId);

        Assert.NotNull(result);
        Assert.Equal("What is your color?", result.NameEnglish);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingQuestion_ReturnsNull()
    {
        using var db = TestDbContextFactory.Create();
        var service = new DbQuestionService(db);

        var result = await service.GetByIdAsync(9999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_AddsQuestionToDatabase()
    {
        using var db = TestDbContextFactory.Create();
        var (group, _) = SampleData();
        db.QuestionGroups.Add(group);
        await db.SaveChangesAsync();

        var service = new DbQuestionService(db);
        var question = new Question
        {
            QuestionGroupId = group.QuestionGroupId,
            NameSwedish = "Typ?",
            NameEnglish = "Type?",
            Mandatory = false,
        };

        var created = await service.CreateAsync(question);

        Assert.Equal("Type?", created.NameEnglish);
        Assert.Equal(1, db.Questions.Count());
    }
}
