using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[Table("Administrator")]
[Index("AdministratorName", Name = "UQ__Administ__63B977293D541A60", IsUnique = true)]
[Index("AdministratorEmail", Name = "UQ__Administ__6D1FEC2F8740B7B1", IsUnique = true)]
public partial class Administrator
{
    [Key]
    [Column("administrator_id")]
    [StringLength(50)]
    public string AdministratorId { get; set; } = null!;

    [Column("administrator_name")]
    [StringLength(50)]
    public string? AdministratorName { get; set; }

    [Column("administrator_email")]
    [StringLength(50)]
    public string? AdministratorEmail { get; set; }

    [Column("administrator_password")]
    [StringLength(50)]
    public string? AdministratorPassword { get; set; }

    [Column("administrator_level")]
    public int? AdministratorLevel { get; set; }

    [Column("administrator_register_date", TypeName = "datetime")]
    public DateTime? AdministratorRegisterDate { get; set; }

    [Column("administrator_phone_no")]
    [StringLength(50)]
    public string? AdministratorPhoneNo { get; set; }
}
