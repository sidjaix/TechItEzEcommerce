using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Core.Entities;

public partial class ContactUs : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ContactUsId { get; set; }

    public string Name { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    public string Message { get; set; } = null!;
}
