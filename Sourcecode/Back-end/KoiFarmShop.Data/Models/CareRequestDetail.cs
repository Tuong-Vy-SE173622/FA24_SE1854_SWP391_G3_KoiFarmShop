﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KoiFarmShop.Data.Models;

public partial class CareRequestDetail
{
    public int RequestDetailId { get; set; }

    public int RequestId { get; set; }

    public int? Image { get; set; }

    public int? CareMethod { get; set; }

    public string Status { get; set; }

    public string Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; }

    public virtual CareRequest Request { get; set; }
}