namespace Order_Cores.Models;

public partial class OrderModel
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime CreatedOn { get; set; }
}
