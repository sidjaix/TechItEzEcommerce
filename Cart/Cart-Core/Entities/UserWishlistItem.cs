using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cart_Core.Entities;

[Table("UserWishlistItem")]
public class UserWishlistItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserWishlistItemId { get; set; }
    public int UserWishlistId { get; set; }
    public int ProductItemId { get; set; }
    public UserWishlist UserWishlist { get; set; }
}