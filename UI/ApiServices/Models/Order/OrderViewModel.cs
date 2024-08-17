namespace ApiServices.Models.Order;

public partial class OrderViewModel
{
    public int OrderId { get; set; }
    public string UserId { get; set; }
    public DateTime OrderDate { get; set; }
}
