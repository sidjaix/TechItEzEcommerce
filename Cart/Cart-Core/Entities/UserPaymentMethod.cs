using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cart_Core.Entities;

[Table("UserPaymentMethod")]
public class UserPaymentMethod
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserPaymentMethodId { get; set; }
    [Required]
    public string UserId { get; set; }
    [Required]
    public int PaymentTypeId { get; set; }
    public string AccountNumber { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public bool IsDefault { get; set; }

    [ForeignKey(nameof(PaymentTypeId))]
    public PaymentType PaymentType { get; set; }
}
