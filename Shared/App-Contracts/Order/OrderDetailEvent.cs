namespace AppContracts.Order;

public class OrderDetailEvent
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
}
