using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cart_Core.Entities;

[Table("Cart")]
public class Cart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CartId { get; set; }
    [Required]
    public string UserId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = [];
}
