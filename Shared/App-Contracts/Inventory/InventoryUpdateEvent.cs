namespace AppContracts.Inventory;

public class InventoryUpdateEvent
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
