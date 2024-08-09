namespace Cart_Cores.Models;

public partial class OrderStatusModel
{
    public int StatusId { get; set; }

    public int OrderId { get; set; }
    public string StatusName { get; set; } = null!;
}
