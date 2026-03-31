using MediatR;
using OrderApplication.DTOs;
using OrderApplication.Interfaces;
using OrderCore.Entities;
using OrderCore.ValueObjects;

public record CreateOrderCommand(Guid CustomerId, AddressDto ShippingAddress, List<OrderItemDto> Items) : IRequest<Guid>;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repository;
    public CreateOrderCommandHandler(IOrderRepository repository) => _repository = repository;

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken ct)
    {
        var address = new Address(
            request.ShippingAddress.Street,
            request.ShippingAddress.City,
            request.ShippingAddress.State,
            request.ShippingAddress.Country,
            request.ShippingAddress.ZipCode);

        var order = new Order(request.CustomerId, address);

        foreach (var item in request.Items)
        {
            order.AddOrderItem(item.VariantId, item.ProductName, item.UnitPrice, item.Quantity);
        }

        await _repository.AddAsync(order, ct);
        return order.Id;
    }
}