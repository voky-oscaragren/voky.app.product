using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.Data;

public class Question
{
    [Key]
    public int QuestionId { get; set; }

    public int QuestionGroupId { get; set; }
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
