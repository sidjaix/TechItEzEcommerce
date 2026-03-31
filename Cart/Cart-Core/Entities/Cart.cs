using System;

namespace CartCore.Entities;

public class Cart
{
    public Guid Id { get; private set; }

    // Soft reference to the User Domain (can be a registered user or a guest session ID)
    public Guid CustomerId { get; private set; }

    public DateTime LastModifiedAt { get; private set; }

    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
    private readonly List<CartItem> _items = new();

    // EF Core constructor
    private Cart() { }

    public Cart(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        LastModifiedAt = DateTime.UtcNow;
    }

    // Domain Behavior: Manages adding items and updating quantities
    public void AddOrUpdateItem(Guid variantId, string productName, decimal unitPrice, int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");

        var existingItem = _items.FirstOrDefault(i => i.VariantId == variantId);

        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(quantity);
            existingItem.UpdatePrice(unitPrice); // Always sync to the latest price when modifying
        }
        else
        {
            _items.Add(new CartItem(this.Id, variantId, productName, unitPrice, quantity));
        }

        LastModifiedAt = DateTime.UtcNow;
    }

    public void RemoveItem(Guid variantId)
    {
        var item = _items.FirstOrDefault(i => i.VariantId == variantId);
        if (item != null)
        {
            _items.Remove(item);
            LastModifiedAt = DateTime.UtcNow;
        }
    }

    public void Clear()
    {
        _items.Clear();
        LastModifiedAt = DateTime.UtcNow;
    }
}
