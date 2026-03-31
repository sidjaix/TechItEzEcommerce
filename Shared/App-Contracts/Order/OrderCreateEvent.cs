namespace AppContracts.Order;

public class OrderCreateEvent
{
    public string UserId { get; set; }
    public int AddressId { get; set; }
    public int PaymentMethodId { get; set; }
    public int OrderTotal { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderDetailEvent> OrderItems { get; set; } = [];
}
