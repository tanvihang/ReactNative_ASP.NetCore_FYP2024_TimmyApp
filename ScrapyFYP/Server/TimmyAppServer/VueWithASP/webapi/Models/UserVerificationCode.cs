using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[Table("UserVerificationCode")]
public partial class UserVerificationCode
{
    [Column("user_id")]
    [StringLength(50)]
    public string? UserId { get; set; }

    [Key]
    [Column("user_email")]
    [StringLength(50)]
    public string UserEmail { get; set; } = null!;

    [Column("verification_code")]
    [StringLength(4)]
    public string? VerificationCode { get; set; }

    [Column("verification_code_expiration_date", TypeName = "datetime")]
    public DateTime? VerificationCodeExpirationDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserVerificationCodes")]
    public virtual UserT? User { get; set; }
}
