using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voky.app.product.api.Data;

public class Tag
{
    //Är typ en enum i visma?
    [Key]
    [Column("TxtNo")]
    public int TagId { get; set; }
    [Key]
    [Column("TxtTp")]
    public long TextType { get; set; } = 20007;

    [Column("Txt")]
    [MaxLength(240)]
    public string Name { get; set; } = string.Empty;
}
