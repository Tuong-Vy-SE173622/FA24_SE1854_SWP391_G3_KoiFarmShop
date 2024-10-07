using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Models;

[Table("Koi_Type")]
public partial class KoiType
{
    [Key]
    [Column("koi_type_id")]
    public int KoiTypeId { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("short_description")]
    [StringLength(500)]
    public string? ShortDescription { get; set; }

    [Column("origin_history")]
    [StringLength(500)]
    public string? OriginHistory { get; set; }

    [Column("category_description")]
    [StringLength(500)]
    public string? CategoryDescription { get; set; }

    [Column("feng_shui")]
    [StringLength(500)]
    public string? FengShui { get; set; }

    [Column("raising_condition")]
    [StringLength(500)]
    public string? RaisingCondition { get; set; }

    [Column("note")]
    [StringLength(500)]
    public string? Note { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("created_by")]
    [StringLength(255)]
    public string? CreatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]
    [StringLength(255)]
    public string? UpdatedBy { get; set; }

    [InverseProperty("KoiType")]
    public virtual ICollection<Koi> Kois { get; set; } = new List<Koi>();
}
