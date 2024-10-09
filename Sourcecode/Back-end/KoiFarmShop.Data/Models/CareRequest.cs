﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Models;

[Table("Care_Request")]
public partial class CareRequest
{
    [Key]
    [Column("request_id")]
    public int RequestId { get; set; }

    [Column("customer_id")]
    public int? CustomerId { get; set; }

    [Column("koi_id")]
    public int? KoiId { get; set; }

    [Column("requested_date")]
    public DateTime? RequestedDate { get; set; }

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

    [InverseProperty("Request")]
    public virtual ICollection<CareRequestDetail> CareRequestDetails { get; set; } = new List<CareRequestDetail>();

    [ForeignKey("CustomerId")]
    [InverseProperty("CareRequests")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("KoiId")]
    [InverseProperty("CareRequests")]
    public virtual Koi? Koi { get; set; }
}
