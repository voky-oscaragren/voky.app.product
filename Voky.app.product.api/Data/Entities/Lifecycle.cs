using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.Models;

public class Lifecycle
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = [];
}
