using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Models;

[Table("Care_Request_Detail")]
public partial class CareRequestDetail
{
    [Key]
    [Column("request_detail_id")]
    public int RequestDetailId { get; set; }

    [Column("request_id")]
    public int RequestId { get; set; }

    [Column("image")]
    public int? Image { get; set; }

    [Column("care_method")]
    public int? CareMethod { get; set; }

    [Column("status")]
    [StringLength(255)]
    public string? Status { get; set; }

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

    [ForeignKey("RequestId")]
    [InverseProperty("CareRequestDetails")]
    public virtual CareRequest Request { get; set; } = null!;
}
