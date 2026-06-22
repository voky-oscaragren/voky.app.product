using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voky.app.product.api.Data;

[Keyless]
public class MainSupplier
{
    [Column("SupNo")]
    public int SupplierNr { get; set; }

    [Column("Nm")]
    [MaxLength(80)]
    public required string Name { get; set; }

    [Column("Cur")]
    public int CurrencyNr { get; set; }
    
    [Column("DME_CostPercent")]
    [Precision(28, 6)]
    public decimal CostPriceAddon { get; set; }

    [Column("DME_ZG_Numerisk_9")]
    public long MarketMargin { get; set; }


    public ICollection<Product> Products { get; set; } = [];
}
