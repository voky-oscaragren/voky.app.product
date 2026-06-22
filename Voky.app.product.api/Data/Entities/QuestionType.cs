using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voky.app.product.api.Data;

public class QuestionType
{
    [Key]
    [Column("DME_QTId")]
    public int QuestionTypeId { get; set; }

    [Column("DME_QTNm")]
    [MaxLength(250)]
    public string Name { get; set; } = string.Empty;
}
