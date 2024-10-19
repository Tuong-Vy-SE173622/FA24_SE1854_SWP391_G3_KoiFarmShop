
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Models;

[Table("Consignment_Detail")]
public partial class ConsignmentDetail
{
    [Key]
    [Column("consignment_detail_id")]
    public int ConsignmentDetailId { get; set; }

    [Column("consignment_id")]
    public int? ConsignmentId { get; set; }

    [Column("koi_id")]
    public int? KoiId { get; set; }

    [Column("consignment_type")]
    [StringLength(30)]
    [Unicode(false)]
    public string? ConsignmentType { get; set; }

    [Column("monthly_consignment_fee")]
    public double? MonthlyConsignmentFee { get; set; }

    [Column("sold_price")]
    public double? SoldPrice { get; set; }

    [Column("health_description")]
    [StringLength(255)]
    public string? HealthDescription { get; set; }

    [Column("weight")]
    public double? Weight { get; set; }

    [Column("status")]
    [StringLength(255)]
    public string? Status { get; set; }

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

    [ForeignKey("ConsignmentId")]
    [InverseProperty("ConsignmentDetails")]
    public virtual ConsignmentRequest? Consignment { get; set; }

    [ForeignKey("KoiId")]
    [InverseProperty("ConsignmentDetails")]
    public virtual Koi? Koi { get; set; }
}