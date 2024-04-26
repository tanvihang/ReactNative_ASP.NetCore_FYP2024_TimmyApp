using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[Table("UserT")]
[Index("UserName", Name = "UQ__UserT__7C9273C4EF4D95E4", IsUnique = true)]
[Index("UserEmail", Name = "UQ__UserT__B0FBA212936CF7A1", IsUnique = true)]
public partial class UserT
{
    [Key]
    [Column("user_id")]
    [StringLength(50)]
    public string UserId { get; set; } = null!;

    [Column("user_name")]
    [StringLength(50)]
    public string? UserName { get; set; }

    [Column("user_email")]
    [StringLength(50)]
    public string? UserEmail { get; set; }

    [Column("user_password")]
    [StringLength(50)]
    public string? UserPassword { get; set; }

    [Column("user_level")]
    public int? UserLevel { get; set; }

    [Column("user_register_date", TypeName = "datetime")]
    public DateTime? UserRegisterDate { get; set; }

    [Column("user_phone_no")]
    [StringLength(50)]
    public string? UserPhoneNo { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("User")]
    public virtual ICollection<UserFavourite> UserFavourites { get; set; } = new List<UserFavourite>();

    [InverseProperty("User")]
    public virtual ICollection<UserSearchHistory> UserSearchHistories { get; set; } = new List<UserSearchHistory>();

    [InverseProperty("User")]
    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();

    [InverseProperty("User")]
    public virtual ICollection<UserVerificationCode> UserVerificationCodes { get; set; } = new List<UserVerificationCode>();
}
