namespace ApiServices.Models.Order;

public partial class OrderStatusViewModel
{
    public int StatusId { get; set; }
    public int OrderId { get; set; }
    public string StatusName { get; set; } = null!;
}
