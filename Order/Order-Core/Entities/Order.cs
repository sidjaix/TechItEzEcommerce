using OrderCore.Enums;
using OrderCore.ValueObjects;
using System;

namespace OrderCore.Entities;

public class Order
{
    public Guid Id { get; private set; }

    // Soft reference to User Domain
    public Guid CustomerId { get; private set; }

    public DateTime OrderDate { get; private set; }
    public OrderStatus Status { get; private set; }

    // The Shipping Address is an immutable Value Object
    public Address ShippingAddress { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    private readonly List<OrderItem> _items = new();

    // Calculated property - guarantees the total is always the sum of its exact items
    public decimal TotalAmount => _items.Sum(i => i.UnitPrice * i.Quantity);

    private Order() { }

    public Order(Guid customerId, Address shippingAddress)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        ShippingAddress = shippingAddress;
        OrderDate = DateTime.UtcNow;
        Status = OrderStatus.PendingValidation;
    }

    public void AddOrderItem(Guid variantId, string productName, decimal unitPrice, int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");

        _items.Add(new OrderItem(this.Id, variantId, productName, unitPrice, quantity));
    }

    public void UpdateStatus(OrderStatus newStatus)
    {
        // Add business logic here (e.g., Cannot ship a cancelled order)
        if (Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Cannot modify a cancelled order.");

        Status = newStatus;
    }
}
