using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voky.app.product.api.Models;

public class PriceMatrix
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int SupplierCurrencyId { get; set; }
    public SupplierCurrency SupplierCurrency { get; set; } = null!;

    public int CurrencyEndPriceId { get; set; }
    public CurrencyEndPrice CurrencyEndPrice { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal EndCustPrice { get; set; }

    public int? MOQ { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal SupplierNetPrice { get; set; }
}
