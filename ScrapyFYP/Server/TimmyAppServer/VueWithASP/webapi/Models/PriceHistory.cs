using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[Table("PriceHistory")]
public partial class PriceHistory
{
    [Key]
    [Column("price_history_id")]
    [StringLength(50)]
    public string PriceHistoryId { get; set; } = null!;

    [Column("timmy_product_full_name")]
    [StringLength(100)]
    public string? TimmyProductFullName { get; set; }

    [Column("price_history_price", TypeName = "decimal(10, 2)")]
    public decimal? PriceHistoryPrice { get; set; }

    [Column("price_history_effective_date", TypeName = "datetime")]
    public DateTime? PriceHistoryEffectiveDate { get; set; }

    [Column("price_history_spider")]
    [StringLength(50)]
    public string? PriceHistorySpider { get; set; }

    [ForeignKey("TimmyProductFullName")]
    [InverseProperty("PriceHistories")]
    [JsonIgnore]
    public virtual TimmyProduct? TimmyProductFullNameNavigation { get; set; }
}
