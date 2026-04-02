using MediatR;
using OrderApplication.DTOs;
using OrderApplication.Interfaces;
using OrderCore.Entities;
using OrderCore.ValueObjects;

public record CreateOrderCommand(Guid CustomerId, AddressDto ShippingAddress) : IRequest<Guid>;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartIntegrationService _cartService;
    private readonly IOrderEventPublisher _eventPublisher;
    public CreateOrderCommandHandler(IOrderRepository orderRepository, ICartIntegrationService cartService, IOrderEventPublisher eventPublisher)
    {
        _orderRepository = orderRepository;
        _cartService = cartService;
        _eventPublisher = eventPublisher;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken ct)
    {
        // 1. Cross-Service Call: Fetch the cart
        var cart = await _cartService.GetActiveCartAsync(request.CustomerId, ct);

        if (cart == null || cart.Items.Count == 0)
        {
            throw new InvalidOperationException("Cannot create an order from an empty or non-existent cart.");
        }

        // 2. Map Address and Create Order Entity
        var address = new Address(
            request.ShippingAddress.Street,
            request.ShippingAddress.City,
            request.ShippingAddress.State,
            request.ShippingAddress.Country,
            request.ShippingAddress.ZipCode);

        var order = new Order(request.CustomerId, address);

        // 3. Add items from the fetched cart
        foreach (var item in cart.Items)
        {
            order.AddOrderItem(item.VariantId, item.ProductName, item.UnitPrice, item.Quantity);
        }

        // 4. Save to Database
        await _orderRepository.AddAsync(order, ct);

        // Convert the domain items to the local Application DTO
        var applicationItems = order.Items.Select(i => new OrderItemDto
        {
            VariantId = i.VariantId,
            Quantity = i.Quantity
        }).ToList();

        // Publish using the abstraction
        await _eventPublisher.PublishOrderPlacedAsync(order.Id, order.CustomerId, applicationItems, ct);

        return order.Id;
    }
}