using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[Table("Scraper")]
public partial class Scraper
{
    [Key]
    [Column("scrape_id")]
    [StringLength(50)]
    public string ScrapeId { get; set; } = null!;

    [Column("scrape_time", TypeName = "datetime")]
    public DateTime? ScrapeTime { get; set; }

    [Column("scrape_product_count")]
    public int? ScrapeProductCount { get; set; }

    [Column("scrape_product_category")]
    [StringLength(50)]
    public string? ScrapeProductCategory { get; set; }

    [Column("scrape_product_brand")]
    [StringLength(50)]
    public string? ScrapeProductBrand { get; set; }

    [Column("scrape_product_model")]
    [StringLength(50)]
    public string? ScrapeProductModel { get; set; }

    [Column("scrape_product_is_test")]
    public int? ScrapeProductIsTest { get; set; }

    [Column("scrape_product_iteration")]
    public int? ScrapeProductIteration { get; set; }
}
