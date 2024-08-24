using System.ComponentModel.DataAnnotations;

namespace ApiServices.Models.Order;

public partial class OrderViewModel
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

    public List<OrderDetailViewModel> OrderDetails { get; set; } = [];
}
