﻿
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Models;

[Table("Customer")]
public partial class Customer
{
    [Key]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("address")]
    [StringLength(255)]
    [Unicode(false)]
    public string Address { get; set; }

    [Column("loyalty_points")]
    public int? LoyaltyPoints { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("created_by")]
    [StringLength(255)]
    [Unicode(false)]
    public string CreatedBy { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]
    [StringLength(255)]
    [Unicode(false)]
    public string UpdatedBy { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<CareRequest> CareRequests { get; set; } = new List<CareRequest>();

    [InverseProperty("Customer")]
    public virtual ICollection<ConsignmentRequest> ConsignmentRequests { get; set; } = new List<ConsignmentRequest>();

    [InverseProperty("Customer")]
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("Customer")]
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    [ForeignKey("UserId")]
    [InverseProperty("Customers")]
    public virtual User User { get; set; }
}