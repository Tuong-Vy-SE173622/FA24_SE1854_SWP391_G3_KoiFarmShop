using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Models;

[Table("Rating")]
public partial class Rating
{
    [Key]
    [Column("rating_id")]
    public int RatingId { get; set; }

    [Column("koi_id")]
    public int? KoiId { get; set; }

    [Column("customer_id")]
    public int? CustomerId { get; set; }

    [Column("rating_value")]
    public double? RatingValue { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Ratings")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("KoiId")]
    [InverseProperty("Ratings")]
    public virtual Koi? Koi { get; set; }
}
