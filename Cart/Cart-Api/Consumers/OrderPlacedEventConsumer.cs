using ApiCommon.Contracts;
using CartApplication.Commands;
using MassTransit;
using MediatR;

namespace CartApi.Consumers;

public class OrderPlacedEventConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly IMediator _mediator;

    public OrderPlacedEventConsumer(IMediator mediator) => _mediator = mediator;

    public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        // Dispatches the pure Application command
        await _mediator.Send(new ClearCartCommand(context.Message.CustomerId));
    }
}
