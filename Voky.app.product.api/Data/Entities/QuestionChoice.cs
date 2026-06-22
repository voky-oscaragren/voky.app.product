using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.Data;

public class QuestionChoice
{
    [Key]
    public int QuestionChoiceId { get; set; }

    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;

    [Required, MaxLength(60)]
    public string NameSwedish { get; set; } = string.Empty;

    [Required, MaxLength(60)]
    public string NameEnglish { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? ProductNumber { get; set; }
}
