using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[Table("UserSubscriptionProduct")]
public partial class UserSubscriptionProduct
{
    [Key]
    [Column("user_subscription_product_id")]
    [StringLength(50)]
    public string UserSubscriptionProductId { get; set; } = null!;

    [Column("user_subscription_id")]
    [StringLength(50)]
    public string? UserSubscriptionId { get; set; }

    [Column("user_subscription_product_currency")]
    [StringLength(50)]
    public string? UserSubscriptionProductCurrency { get; set; }

    [Column("user_subscription_product_added_date", TypeName = "datetime")]
    public DateTime? UserSubscriptionProductAddedDate { get; set; }

    [Column("user_subscription_product_user_preference")]
    public int? UserSubscriptionProductUserPreference { get; set; }

    [Column("user_subscription_product_url")]
    [StringLength(300)]
    public string? UserSubscriptionProductUrl { get; set; }

    [Column("user_subscription_product_image")]
    [StringLength(300)]
    public string? UserSubscriptionProductImage { get; set; }

    [Column("user_subscription_product_unique_id")]
    [StringLength(100)]
    public string? UserSubscriptionProductUniqueId { get; set; }

    [Column("user_subscription_product_title")]
    [StringLength(100)]
    public string? UserSubscriptionProductTitle { get; set; }

    [Column("user_subscription_product_description")]
    public string? UserSubscriptionProductDescription { get; set; }

    [Column("user_subscription_product_condition")]
    [StringLength(50)]
    public string? UserSubscriptionProductCondition { get; set; }

    [Column("user_subscription_product_spider")]
    [StringLength(50)]
    public string? UserSubscriptionProductSpider { get; set; }

    [Column("user_subscription_product_price", TypeName = "decimal(10, 2)")]
    public decimal? UserSubscriptionProductPrice { get; set; }

    [Column("user_subscription_product_price_CNY", TypeName = "decimal(10, 2)")]
    public decimal? UserSubscriptionProductPriceCny { get; set; }

    [ForeignKey("UserSubscriptionId")]
    [InverseProperty("UserSubscriptionProducts")]
    [JsonIgnore]
    public virtual UserSubscription? UserSubscription { get; set; }
}
