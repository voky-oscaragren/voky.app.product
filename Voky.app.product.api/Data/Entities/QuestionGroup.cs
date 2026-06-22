using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voky.Shared.Visma.Database.Entities;

namespace Voky.app.product.api.Data;

[Table("DME_QuestionGroups")]

public class QuestionGroup : VismaEntity
{
    [Key]
    [Column("DME_QGId")]
    public int QuestionGroupId { get; set; }

    [Column("DME_QGNm")]
    [MaxLength(250)]
    public string Name { get; set; } = string.Empty;
}
