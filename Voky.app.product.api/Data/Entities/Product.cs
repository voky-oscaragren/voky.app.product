using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voky.Shared.Visma.Database.Entities;

namespace Voky.app.product.api.Data;

[Table("Prod")]
public class Product : VismaEntity
{
    [Key]
    [Column("ProdNo")]
    [MaxLength(40)]
    public string ProductNr { get; set; } = string.Empty;

    [Column("Descr")]
    [MaxLength(60)]
    public required string Name { get; set; } = string.Empty;

    [Column("DME_DescEng")]
    [MaxLength(100)]
    public required string LevArtNrName { get; set; }

    [Column("DME_NS_MainProdNo")]
    [MaxLength(40)]
    public string? ArtNrVarianthead { get; set; }

    [Column("DME_ZG_Fritext_1000t_1")]
    [MaxLength(1000)]
    public string? Description { get; set; }

    //public bool SendToOpti { get; set; } //Ej med just nu, mer admingrej

    [Column("DME_ProdNoSetUp")]
    [MaxLength(60)]
    public string? ArtNrStartCost { get; set; }

    [Column("DME_NoSetUp")]
    public int? StartCostAmount { get; set; }

    [Column("DME_MainSup")]
    public required int MainSupplierNr { get; set; }

    [Column("DME_QGId")]
    public int? QuestionGroupId { get; set; }

    [Column("DME_NS_SalesPackageQty")]
    public int Moq_customer { get; set; }

    [Column("DME_NS_LifeCycle")]
    public long? LifeCycle { get; set; }
}
