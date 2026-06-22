using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voky.Shared.Visma.Database.Entities;

namespace Voky.app.product.api.Data;

public class PriceMatrix : VismaEntity
{
    [Key]
    [Column("LnNo")]
    public int LineNo { get; set; }

    [Column("ProdNo")]
    [MaxLength(40)]
    public string ProductNr { get; set; } = string.Empty;

    [Column("Cur")]
    public int CurrencyNo { get; set; }

    [Column("SalePr")]
    [Precision(28, 6)]
    public decimal EndCustPrice { get; set; }

    [Column("MinNo")]
    [Precision(28, 6)]
    public decimal MOQ { get; set; }

    [Column("CstPr")]
    [Precision(28, 6)]
    public decimal SupplierNetPrice { get; set; }

    [Column("CstCur")]
    public int CurrenyNr { get; set; }
}
