using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.Data;

public class MainSupplier
{
    [Key]
    public int SupplierNr { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Email { get; set; }

    [MaxLength(200)]
    public string? BankAccountGiro { get; set; }

    public ICollection<Product> Products { get; set; } = [];
}
