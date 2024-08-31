namespace App_Contracts.Inventory;

public class InventoryUpdateEvent
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
