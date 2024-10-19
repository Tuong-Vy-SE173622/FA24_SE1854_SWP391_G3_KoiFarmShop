
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Models;

[Table("Feedback")]
public partial class Feedback
{
    [Key]
    [Column("feedback_id")]
    public int FeedbackId { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("customer_id")]
    public int? CustomerId { get; set; }

    [Column("content")]
    [StringLength(255)]
    [Unicode(false)]
    public string Content { get; set; }

    [Column("is_active")]
    public bool? IsActive { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Feedbacks")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Feedbacks")]
    public virtual Order Order { get; set; }
}