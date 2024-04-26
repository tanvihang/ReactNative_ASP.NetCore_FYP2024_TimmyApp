using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[Table("SubscribedProduct")]
public partial class SubscribedProduct
{
    [Key]
    [Column("subscribed_product_full_name")]
    [StringLength(100)]
    public string SubscribedProductFullName { get; set; } = null!;

    [Column("subscribed_product_category")]
    [StringLength(50)]
    public string? SubscribedProductCategory { get; set; }

    [Column("subscribed_product_brand")]
    [StringLength(50)]
    public string? SubscribedProductBrand { get; set; }

    [Column("subscribed_product_model")]
    [StringLength(50)]
    public string? SubscribedProductModel { get; set; }

    [Column("subscribed_product_highest_level")]
    public int? SubscribedProductHighestLevel { get; set; }

    [Column("subscribed_product_count")]
    public int? SubscribedProductCount { get; set; }
}
