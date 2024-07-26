using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace User_Core.Entities;

[Table("OrderStatus")]
public partial class OrderStatus
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StatusId { get; set; }

    public int OrderId { get; set; }

    [StringLength(50)]
    public string StatusName { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("OrderStatuses")]
    public virtual Order Order { get; set; } = null!;
}
