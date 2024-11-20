﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Models;

[Table("Order_Item")]
public partial class OrderItem
{
    [Key]
    [Column("order_item_id")]
    public int OrderItemId { get; set; }

    [Column("order_id")]
    public int OrderId { get; set; }

    [Column("koi_id")]
    public int KoiId { get; set; }

    [Column("amount")]
    public int? Amount { get; set; }

    [Column("price")]
    public double? Price { get; set; }

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

    [InverseProperty("OrderItem")]
    public virtual ICollection<Certification> Certifications { get; set; } = new List<Certification>();

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order Order { get; set; }
}