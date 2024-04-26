using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[PrimaryKey("UserId", "ProductUniqueId")]
[Table("UserFavourite")]
public partial class UserFavourite
{
    [Key]
    [Column("product_unique_id")]
    [StringLength(50)]
    public string ProductUniqueId { get; set; } = null!;

    [Key]
    [Column("user_id")]
    [StringLength(50)]
    public string UserId { get; set; } = null!;

    [Column("user_favourite_date", TypeName = "datetime")]
    public DateTime? UserFavouriteDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserFavourites")]
    public virtual UserT User { get; set; } = null!;
}
