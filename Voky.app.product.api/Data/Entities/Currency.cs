using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voky.Shared.Visma.Database.Entities;

namespace Voky.app.product.api.Data;

public class Currency : VismaEntity
{
    [Key]
    [Column("CurNo")]
    public int CurrencyNr { get; set; }

    [Column("ISO")]
    [MaxLength(3)]
    public string IsoCode { get; set; } = string.Empty;

    [Column("SalesRt")]
    [Precision(28, 6)]
    public decimal SalesRate { get; set; }
}
