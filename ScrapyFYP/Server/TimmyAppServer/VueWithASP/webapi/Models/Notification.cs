using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[Table("Notification")]
public partial class Notification
{
    [Key]
    [Column("notification_id")]
    [StringLength(50)]
    public string NotificationId { get; set; } = null!;

    [Column("user_id")]
    [StringLength(50)]
    public string? UserId { get; set; }

    [Column("notification_title")]
    [StringLength(50)]
    public string? NotificationTitle { get; set; }

    [Column("notification_content")]
    public string? NotificationContent { get; set; }

    [Column("notification_type")]
    [StringLength(50)]
    public string? NotificationType { get; set; }

    [Column("notification_date", TypeName = "datetime")]
    public DateTime? NotificationDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Notifications")]
    public virtual UserT? User { get; set; }
}
