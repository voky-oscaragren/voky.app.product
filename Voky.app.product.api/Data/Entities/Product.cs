using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voky.app.product.api.Models;

public class Product
{
    [Key, MaxLength(50)]
    public string ProductNr { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? LevArtNrName { get; set; }

    [MaxLength(50)]
    public string? ArtNrVarianthead { get; set; }

    [MaxLength(2000)]
    public string? Description { get; set; }

    public bool SendToOpti { get; set; }

    [MaxLength(50)]
    public string? ArtNrStartCost { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? StartCostAmount { get; set; }

    public int? MainSupplierNr { get; set; }
    public MainSupplier? MainSupplier { get; set; }

    public int? LifecycleId { get; set; }
    public Lifecycle? Lifecycle { get; set; }

    public int? QuestionGroupId { get; set; }
    public QuestionGroup? QuestionGroup { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<PriceMatrix> PriceMatrices { get; set; } = [];
}
