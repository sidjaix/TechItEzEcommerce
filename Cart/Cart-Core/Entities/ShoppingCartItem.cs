using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cart_Core.Entities;

[Table("ShoppingCartItem")]
public class ShoppingCartItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ShoppingCartId { get; set; }
    public int ProductItemId { get; set; }
    public int Quantity { get; set; }

    [ForeignKey(nameof(ShoppingCartId))]
    public ShoppingCart Cart { get; set; }
}
