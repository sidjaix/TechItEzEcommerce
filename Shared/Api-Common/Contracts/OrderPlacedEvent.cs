namespace ApiCommon.Contracts;

public record class OrderPlacedEvent
{
    public Guid OrderId { get; init; }
    public Guid CustomerId { get; init; }
    public DateTime Timestamp { get; init; }
    public List<OrderItemPayload> Items { get; init; } = new();
}
