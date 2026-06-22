using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voky.Shared.Visma.Database.Entities;

namespace Voky.app.product.api.Data;

public class QuestionType : VismaEntity
{
    [Key]
    [Column("DME_QTId")]
    public int QuestionTypeId { get; set; }

    [Column("DME_QTNm")]
    [MaxLength(250)]
    public string Name { get; set; } = string.Empty;
}
