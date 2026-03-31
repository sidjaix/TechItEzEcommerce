using System;

namespace CartCore.Entities;

public class CartItem
{
    public Guid Id { get; private set; }
    public Guid CartId { get; private set; }

    // Soft reference to the Product Domain
    public Guid VariantId { get; private set; }

    // Snapshots: We store these so the cart can render quickly without querying the Product DB every time
    public string ProductName { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    private CartItem() { }

    internal CartItem(Guid cartId, Guid variantId, string productName, decimal unitPrice, int quantity)
    {
        Id = Guid.NewGuid();
        CartId = cartId;
        VariantId = variantId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    internal void IncreaseQuantity(int amount) => Quantity += amount;

    internal void UpdatePrice(decimal newPrice) => UnitPrice = newPrice;
}
