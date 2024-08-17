using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order_Core.Entities;

[Table("Order")]
public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string UserId { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public int PaymentMethodId { get; set; }
    [Required]
    public int AddressId { get; set; }
    public decimal OrderTotal { get; set; }
    [Required]
    public int OrderStatusId { get; set; }

    public virtual OrderStatus OrderStatus { get; set; }
}
