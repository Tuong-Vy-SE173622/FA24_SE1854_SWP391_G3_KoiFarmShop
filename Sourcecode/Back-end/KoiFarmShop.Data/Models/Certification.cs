
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Models;

[Table("Certification")]
public partial class Certification
{
    [Key]
    [Column("certificate_id")]
    public int CertificateId { get; set; }

    [Column("order_item_id")]
    public int OrderItemId { get; set; }

    [Column("origin")]
    [StringLength(500)]
    public string Origin { get; set; }

    [Column("image")]
    [StringLength(500)]
    public string Image { get; set; }

    [Column("health_status_description")]
    [StringLength(500)]
    public string HealthStatusDescription { get; set; }

    [Column("award")]
    [StringLength(500)]
    public string Award { get; set; }

    [ForeignKey("OrderItemId")]
    [InverseProperty("Certifications")]
    public virtual OrderItem OrderItem { get; set; }
}