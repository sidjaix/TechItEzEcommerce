using ApiCommon.Contracts;
using MassTransit;
using MediatR;
using ProductApplication.Command;
using ProductApplication.DTOs;

namespace ProductApi.Consumers;

public class OrderPlacedEventConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly IMediator _mediator;

    public OrderPlacedEventConsumer(IMediator mediator) => _mediator = mediator;

    public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        // Map the Api-Common payload to a Product-specific Application DTO
        var localItems = context.Message.Items.Select(i => new DeductInventoryItemDto
        {
            VariantId = i.VariantId,
            Quantity = i.Quantity
        }).ToList();

        // Dispatch to the Application layer
        await _mediator.Send(new DeductInventoryCommand(localItems));
    }
}
