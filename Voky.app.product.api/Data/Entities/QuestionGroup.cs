using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voky.app.product.api.Data;

public class QuestionGroup
{
    [Key]
    [Column("DME_QGId")]
    public int QuestionGroupId { get; set; }

    [Column("DME_QGNm")]
    [MaxLength(250)]
    public string Name { get; set; } = string.Empty;
}
