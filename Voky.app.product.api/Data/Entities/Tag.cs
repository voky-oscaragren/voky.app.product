using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.Data;

public class Tag
{
    [Key]
    public int TagId { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;
}
