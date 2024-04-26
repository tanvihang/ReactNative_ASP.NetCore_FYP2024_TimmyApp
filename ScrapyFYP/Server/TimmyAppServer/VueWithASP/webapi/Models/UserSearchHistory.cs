using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[PrimaryKey("UserId", "UserSearchHistoryProductFullName")]
[Table("UserSearchHistory")]
public partial class UserSearchHistory
{
    [Key]
    [Column("user_search_history_product_full_name")]
    [StringLength(100)]
    public string UserSearchHistoryProductFullName { get; set; } = null!;

    [Key]
    [Column("user_id")]
    [StringLength(50)]
    public string UserId { get; set; } = null!;

    [Column("user_searh_history_date", TypeName = "datetime")]
    public DateTime? UserSearhHistoryDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserSearchHistories")]
    public virtual UserT User { get; set; } = null!;
}
