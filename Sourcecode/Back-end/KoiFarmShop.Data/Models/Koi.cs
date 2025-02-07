﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KoiFarmShop.Data.Models;

public partial class Koi
{
    public int KoiId { get; set; }

    public int? KoiTypeId { get; set; }

    public string Origin { get; set; }

    public int? Gender { get; set; }

    public int? Age { get; set; }

    public double? Size { get; set; }

    public string Characteristics { get; set; }

    public double? FeedingAmountPerDay { get; set; }

    public double? ScreeningRate { get; set; }

    public bool? IsOwnedByFarm { get; set; }

    public bool? IsImported { get; set; }

    public string Generation { get; set; }

    public bool? IsLocal { get; set; }

    public bool? IsActive { get; set; }

    public string Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; }

    public virtual ICollection<CareRequest> CareRequests { get; set; } = new List<CareRequest>();

    public virtual ICollection<ConsignmentDetail> ConsignmentDetails { get; set; } = new List<ConsignmentDetail>();

    public virtual KoiType KoiType { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}