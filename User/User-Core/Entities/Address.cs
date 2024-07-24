using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace User_Core.Entities;

[Table("Address")]
public partial class Address
{
    [Key]
    [Column("AddressID")]
    public int AddressId { get; set; }

    [Column("CustomerID")]
    public int CustomerId { get; set; }

    [StringLength(255)]
    public string Street { get; set; } = null!;

    [StringLength(50)]
    public string City { get; set; } = null!;

    [StringLength(50)]
    public string State { get; set; } = null!;

    [StringLength(20)]
    public string ZipCode { get; set; } = null!;

    public bool IsShippingAddress { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Addresses")]
    public virtual User Customer { get; set; } = null!;
}
