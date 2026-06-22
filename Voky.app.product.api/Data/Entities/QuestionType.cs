using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.Data;

public class QuestionType
{
    [Key, MaxLength(50)]
    public string QuestionTypeId { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}
