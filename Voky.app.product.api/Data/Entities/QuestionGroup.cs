using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.Data;

public class QuestionGroup
{
    [Key]
    public int QuestionGroupId { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Question> Questions { get; set; } = [];
    public ICollection<Product> Products { get; set; } = [];
}
