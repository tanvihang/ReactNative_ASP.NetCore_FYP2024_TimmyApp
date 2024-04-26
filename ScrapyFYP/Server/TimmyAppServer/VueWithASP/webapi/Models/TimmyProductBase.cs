using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[Table("TimmyProductBase")]
public partial class TimmyProductBase
{
    [Key]
    [Column("timmy_product_base_id")]
    [StringLength(50)]
    public string TimmyProductBaseId { get; set; } = null!;

    [Column("timmy_product_category")]
    [StringLength(50)]
    public string? TimmyProductCategory { get; set; }

    [Column("timmy_product_brand")]
    [StringLength(50)]
    public string? TimmyProductBrand { get; set; }

    [InverseProperty("TimmyProductBase")]
    public virtual ICollection<TimmyProductModel> TimmyProductModels { get; set; } = new List<TimmyProductModel>();
}
