using System;

namespace OrderCore.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }

    // Soft reference to Product Domain
    public Guid VariantId { get; private set; }

    // IMMUTABLE SNAPSHOTS: Never update these after creation
    public string ProductName { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    private OrderItem() { }

    internal OrderItem(Guid orderId, Guid variantId, string productName, decimal unitPrice, int quantity)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        VariantId = variantId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
}
