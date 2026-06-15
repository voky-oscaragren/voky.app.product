using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.Models;

public class Question
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid QuestionGroupId { get; set; }
    public QuestionGroup QuestionGroup { get; set; } = null!;

    [MaxLength(50)]
    public string? QuestionTypeId { get; set; }

    public bool Mandatory { get; set; }

    [Required, MaxLength(60)]
    public string NameSwedish { get; set; } = string.Empty;

    [Required, MaxLength(60)]
    public string NameEnglish { get; set; } = string.Empty;

    public ICollection<QuestionChoice> Choices { get; set; } = [];
}
