using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.Models;

public class SupplierCurrency
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(10)]
    public string Name { get; set; } = string.Empty;

    public ICollection<PriceMatrix> PriceMatrices { get; set; } = [];
}
