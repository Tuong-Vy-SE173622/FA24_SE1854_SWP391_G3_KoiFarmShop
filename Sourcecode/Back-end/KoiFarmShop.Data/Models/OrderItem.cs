﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KoiFarmShop.Data.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int? OrderId { get; set; }

    public int? KoiId { get; set; }

    public int? Amount { get; set; }

    public double? Price { get; set; }

    public bool? IsActive { get; set; }

    public string Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; }

    public virtual ICollection<Certification> Certifications { get; set; } = new List<Certification>();

    public virtual Koi Koi { get; set; }

    public virtual Order Order { get; set; }
}