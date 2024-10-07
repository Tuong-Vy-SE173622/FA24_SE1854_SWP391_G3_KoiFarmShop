using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Models;

[Table("Consignment_Request")]
public partial class ConsignmentRequest
{
    [Key]
    [Column("consignment_id")]
    public int ConsignmentId { get; set; }

    [Column("customer_id")]
    public int? CustomerId { get; set; }

    [Column("sub_amount")]
    [StringLength(10)]
    public string? SubAmount { get; set; }

    [Column("VAT")]
    [StringLength(10)]
    public string? Vat { get; set; }

    [Column("VAT_amount")]
    [StringLength(10)]
    public string? VatAmount { get; set; }

    [Column("promotion_amount")]
    public int? PromotionAmount { get; set; }

    [Column("total_amount")]
    public double? TotalAmount { get; set; }

    [Column("payment_method")]
    public double? PaymentMethod { get; set; }

    [Column("payment_status")]
    [StringLength(50)]
    public string? PaymentStatus { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; }

    [Column("note")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Note { get; set; }

    [Column("status")]
    [StringLength(255)]
    public string? Status { get; set; }

    [Column("is_online")]
    public bool? IsOnline { get; set; }

    [InverseProperty("Consignment")]
    public virtual ICollection<ConsignmentDetail> ConsignmentDetails { get; set; } = new List<ConsignmentDetail>();

    [ForeignKey("CustomerId")]
    [InverseProperty("ConsignmentRequests")]
    public virtual Customer? Customer { get; set; }
}
