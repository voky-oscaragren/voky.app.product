using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.Data;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;
}
