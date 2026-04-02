using ApiCommon.Contracts;
using MassTransit;
using OrderApplication.DTOs;
using OrderApplication.Interfaces;

namespace OrderApi.Services;

public class OrderEventPublisher : IOrderEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderEventPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishOrderPlacedAsync(Guid orderId, Guid customerId, IEnumerable<OrderItemDto> items, CancellationToken ct)
    {
        // Map Application DTO to the Shared API-Common Contract
        var eventPayload = new OrderPlacedEvent
        {
            OrderId = orderId,
            CustomerId = customerId,
            Timestamp = DateTime.UtcNow,
            Items = items.Select(i => new OrderItemPayload
            {
                VariantId = i.VariantId,
                Quantity = i.Quantity
            }).ToList()
        };

        await _publishEndpoint.Publish(eventPayload, ct);
    }
}
