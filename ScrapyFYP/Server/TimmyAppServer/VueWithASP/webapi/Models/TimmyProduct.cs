using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[Table("TimmyProduct")]
public partial class TimmyProduct
{
    [Key]
    [Column("timmy_product_full_name")]
    [StringLength(100)]
    public string TimmyProductFullName { get; set; } = null!;

    [Column("timmy_product_category")]
    [StringLength(50)]
    public string? TimmyProductCategory { get; set; }

    [Column("timmy_product_brand")]
    [StringLength(50)]
    public string? TimmyProductBrand { get; set; }

    [Column("timmy_product_model")]
    [StringLength(50)]
    public string? TimmyProductModel { get; set; }

    [Column("timmy_product_sub_model")]
    [StringLength(50)]
    public string? TimmyProductSubModel { get; set; }

    [Column("timmy_product_adopted")]
    public int? TimmyProductAdopted { get; set; }

    [InverseProperty("TimmyProductFullNameNavigation")]
    public virtual ICollection<PriceHistory> PriceHistories { get; set; } = new List<PriceHistory>();
}
