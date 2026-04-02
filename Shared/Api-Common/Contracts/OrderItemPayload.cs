namespace ApiCommon.Contracts;

public record class OrderItemPayload
{
    public Guid VariantId { get; init; }
    public int Quantity { get; init; }
}
