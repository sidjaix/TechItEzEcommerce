using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cart_Core.Entities;

[Table("Wishlist")]
public class Wishlist
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int WishlistId { get; set; }
    [Required]
    public string UserId { get; set; } = null!;
}
