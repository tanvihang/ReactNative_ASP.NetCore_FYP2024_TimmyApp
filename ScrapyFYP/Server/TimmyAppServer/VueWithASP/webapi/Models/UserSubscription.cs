using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[Table("UserSubscription")]
public partial class UserSubscription
{
    [Key]
    [Column("user_subscription_id")]
    [StringLength(50)]
    public string UserSubscriptionId { get; set; } = null!;

    [Column("user_subscription_product_full_name")]
    [StringLength(100)]
    public string? UserSubscriptionProductFullName { get; set; }

    [Column("user_id")]
    [StringLength(50)]
    public string? UserId { get; set; }

    [Column("user_subscription_product_category")]
    [StringLength(50)]
    public string? UserSubscriptionProductCategory { get; set; }

    [Column("user_subscription_product_brand")]
    [StringLength(50)]
    public string? UserSubscriptionProductBrand { get; set; }

    [Column("user_subscription_product_model")]
    [StringLength(50)]
    public string? UserSubscriptionProductModel { get; set; }

    [Column("user_subscription_product_sub_model")]
    [StringLength(50)]
    public string? UserSubscriptionProductSubModel { get; set; }

    [Column("user_subscription_product_description")]
    [StringLength(100)]
    public string? UserSubscriptionProductDescription { get; set; }

    [Column("user_subscription_product_highest_price", TypeName = "decimal(10, 2)")]
    public decimal? UserSubscriptionProductHighestPrice { get; set; }

    [Column("user_subscription_product_lowest_price", TypeName = "decimal(10, 2)")]
    public decimal? UserSubscriptionProductLowestPrice { get; set; }

    [Column("user_subscription_product_country")]
    [StringLength(50)]
    public string? UserSubscriptionProductCountry { get; set; }

    [Column("user_subscription_product_state")]
    [StringLength(50)]
    public string? UserSubscriptionProductState { get; set; }

    [Column("user_subscription_product_condition")]
    [StringLength(50)]
    public string? UserSubscriptionProductCondition { get; set; }

    [Column("user_subscription_notification_method")]
    [StringLength(50)]
    public string? UserSubscriptionNotificationMethod { get; set; }

    [Column("user_subscription_notification_time")]
    public int? UserSubscriptionNotificationTime { get; set; }

    [Column("user_subscription_date", TypeName = "datetime")]
    public DateTime? UserSubscriptionDate { get; set; }

    [Column("user_subscription_price", TypeName = "decimal(10, 2)")]
    public decimal? UserSubscriptionPrice { get; set; }

    [Column("user_subscription_status")]
    public int? UserSubscriptionStatus { get; set; }

    [Column("user_subscription_spiders")]
    [StringLength(100)]
    public string? UserSubscriptionSpiders { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserSubscriptions")]
    [JsonIgnore]
    public virtual UserT? User { get; set; }

    [InverseProperty("UserSubscription")]
    [JsonIgnore]
    public virtual ICollection<UserSubscriptionProduct> UserSubscriptionProducts { get; set; } = new List<UserSubscriptionProduct>();
}
