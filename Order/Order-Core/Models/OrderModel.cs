using System.ComponentModel.DataAnnotations;

namespace Order_Core.Models;

public partial class OrderModel
{
    public int OrderId { get; set; }
    [Required]
    public string UserId { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public int PaymentMethodId { get; set; }
    [Required]
    public int AddressId { get; set; }
    public int OrderTotal { get; set; }
    [Required]
    public int OrderStatusId { get; set; }
    public string Status { get; set; }

    public List<OrderDetailModel> OrderDetails { get; set; } = [];
}
