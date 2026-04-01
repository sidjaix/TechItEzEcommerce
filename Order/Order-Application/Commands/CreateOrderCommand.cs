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
    public CreateOrderCommandHandler(IOrderRepository orderRepository, ICartIntegrationService cartService)
    {
        _orderRepository = orderRepository;
        _cartService = cartService;
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

        return order.Id;
    }
}