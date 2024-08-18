using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cart_Core.Entities;

[Table("WishlistItem")]
public class WishlistItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int WishlistItemId { get; set; }
    public int WishlistId { get; set; }
    public int ProductId { get; set; }
    public Wishlist Wishlist { get; set; }
}