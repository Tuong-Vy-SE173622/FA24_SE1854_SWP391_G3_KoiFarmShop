using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Models;

[Table("Koi")]
public partial class Koi
{
    [Key]
    [Column("koi_id")]
    public int KoiId { get; set; }

    [Column("koi_type_id")]
    public int? KoiTypeId { get; set; }

    [Column("origin")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Origin { get; set; }

    [Column("gender")]
    public int? Gender { get; set; }

    [Column("age")]
    public int? Age { get; set; }

    [Column("size")]
    public double? Size { get; set; }

    [Column("characteristics")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Characteristics { get; set; }

    [Column("feeding_amount_per_day")]
    public double? FeedingAmountPerDay { get; set; }

    [Column("screening_rate")]
    public double? ScreeningRate { get; set; }

    [Column("is_owned_by_farm")]
    public bool? IsOwnedByFarm { get; set; }

    [Column("is_imported")]
    public bool? IsImported { get; set; }

    [Column("generation")]
    [StringLength(255)]
    public string? Generation { get; set; }

    [Column("is_local")]
    public bool? IsLocal { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; }

    [Column("note")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Note { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("created_by")]
    [StringLength(255)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]
    [StringLength(255)]
    [Unicode(false)]
    public string? UpdatedBy { get; set; }

    [InverseProperty("Koi")]
    public virtual ICollection<CareRequest> CareRequests { get; set; } = new List<CareRequest>();

    [InverseProperty("Koi")]
    public virtual ICollection<ConsignmentDetail> ConsignmentDetails { get; set; } = new List<ConsignmentDetail>();

    [ForeignKey("KoiTypeId")]
    [InverseProperty("Kois")]
    public virtual KoiType? KoiType { get; set; }

    [InverseProperty("Koi")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("Koi")]
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
