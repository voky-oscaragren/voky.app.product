using System.ComponentModel.DataAnnotations;

namespace Voky.app.product.api.Models;

public class MainSupplier
{
    [Key, MaxLength(50)]
    public string SupplierNr { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Email { get; set; }

    [MaxLength(200)]
    public string? BankAccountGiro { get; set; }

    public ICollection<Product> Products { get; set; } = [];
}
