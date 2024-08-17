using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cart_Core.Entities;

[Table("UserCartItem")]
public class UserCartItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserCartItemId { get; set; }
    public int UserCartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    [ForeignKey(nameof(UserCartId))]
    public UserCart Cart { get; set; }
}
