using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Core.Entities;

[Table("ContactUs")]
public partial class ContactUs
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ContactUsId { get; set; }

    public string Name { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string Message { get; set; } = null!;
}
