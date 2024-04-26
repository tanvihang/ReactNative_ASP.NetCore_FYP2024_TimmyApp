using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[Table("DailySearch")]
public partial class DailySearch
{
    [Key]
    [Column("product_name")]
    [StringLength(50)]
    public string ProductName { get; set; } = null!;
}
