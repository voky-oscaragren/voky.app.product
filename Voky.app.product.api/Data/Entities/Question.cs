using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voky.Shared.Visma.Database.Entities;

namespace Voky.app.product.api.Data;

public class Question : VismaEntity
{
    [Key]
    [Column("DME_QId")]
    public int QuestionId { get; set; }

    [Column("DME_QGId")]
    public int QuestionGroupId { get; set; }

    [Column("DME_QTId")]
    public int? QuestionTypeId { get; set; }

    [Column("DME_QMan")]
    public byte Mandatory { get; set; }

    [Column("DME_QNm")]
    [MaxLength(250)]
    public string NameSwedish { get; set; } = string.Empty;

    [Column("DME_QNmEng")]
    [MaxLength(250)]
    public string NameEnglish { get; set; } = string.Empty;
}
