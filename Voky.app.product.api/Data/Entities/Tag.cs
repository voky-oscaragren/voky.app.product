using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voky.Shared.Visma.Database.Entities;

namespace Voky.app.product.api.Data;

[Table("Txt")]
[PrimaryKey(nameof(TagId), nameof(TextType))]
public class Tag : VismaEntity
{
    [Column("TxtNo")]
    public int TagId { get; set; }

    [Column("TxtTp")]
    public long TextType { get; set; } = 20007;

    [Column("Txt")]
    [MaxLength(240)]
    public string Name { get; set; } = string.Empty;
}
