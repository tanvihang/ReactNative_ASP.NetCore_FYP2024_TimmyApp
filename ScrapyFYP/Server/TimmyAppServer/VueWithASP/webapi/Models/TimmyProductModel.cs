using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

[Table("TimmyProductModel")]
public partial class TimmyProductModel
{
    [Key]
    [Column("timmy_product_model_id")]
    [StringLength(50)]
    public string TimmyProductModelId { get; set; } = null!;

    [Column("timmy_product_base_id")]
    [StringLength(50)]
    public string? TimmyProductBaseId { get; set; }

    [Column("timmy_product_model_model")]
    [StringLength(50)]
    public string? TimmyProductModelModel { get; set; }

    [Column("timmy_product_model_sub_model")]
    [StringLength(50)]
    public string? TimmyProductModelSubModel { get; set; }

    [Column("timmy_product_model_adopted")]
    [StringLength(50)]
    public string? TimmyProductModelAdopted { get; set; }

    [ForeignKey("TimmyProductBaseId")]
    [InverseProperty("TimmyProductModels")]
    [JsonIgnore]
    public virtual TimmyProductBase? TimmyProductBase { get; set; }
}
