using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voky.Shared.Visma.Database.Entities;


namespace Voky.app.product.api.Data;

public class Category : VismaEntity 
{
    [Key]
    [Column("DME_CatNo")]
    public int CategoryId { get; set; }

    [Column("DME_CatNm")]
    [MaxLength(250)]
    public string Name { get; set; } = string.Empty;

    [Column("DME_ZG_Fritext_250t_2")]
    [MaxLength(250)]
    public string NameEnglish { get; set; } = string.Empty;
}
