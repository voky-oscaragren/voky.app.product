using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.Models;

public class QuestionChoice
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = null!;

    [Required, MaxLength(60)]
    public string NameSwedish { get; set; } = string.Empty;

    [Required, MaxLength(60)]
    public string NameEnglish { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? ProductNumber { get; set; }
}
