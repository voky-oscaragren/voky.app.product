using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voky.Shared.Visma.Database.Entities;

namespace Voky.app.product.api.Data;

public class QuestionChoice : VismaEntity
{
    [Key]
    [Column("DME_QCId")]
    public int QuestionChoiceId { get; set; }

    [Column("DME_QId")]
    public int QuestionId { get; set; }

    [Column("DME_QCNm")]
    [MaxLength(250)]
    public string NameSwedish { get; set; } = string.Empty;

    [Column("DME_QCNmEng")]
    [MaxLength(250)]
    public string NameEnglish { get; set; } = string.Empty;

    [Column("DME_ProdNo")]
    [MaxLength(40)]
    public string? ProductNumber { get; set; }
}
