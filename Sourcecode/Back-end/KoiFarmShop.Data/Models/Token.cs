using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Models;

[Table("Token")]
public partial class Token
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime ExpiredTime { get; set; }

    public int Status { get; set; }
}
