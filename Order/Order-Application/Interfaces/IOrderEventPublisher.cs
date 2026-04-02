using OrderApplication.DTOs;
using System;

namespace OrderApplication.Interfaces;

public interface IOrderEventPublisher
{
    Task PublishOrderPlacedAsync(Guid orderId, Guid customerId, IEnumerable<OrderItemDto> items, CancellationToken ct);
}
