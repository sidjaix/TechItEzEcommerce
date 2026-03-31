using MediatR;
using CartApplication.Interfaces;
using CartCore.Entities;

namespace CartApplication.Commands;

public record AddItemToCartCommand(Guid CustomerId, Guid VariantId, string ProductName, decimal UnitPrice, int Quantity) : IRequest<bool>;

public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommand, bool>
{
    private readonly ICartRepository _repository;
    public AddItemToCartCommandHandler(ICartRepository repository) => _repository = repository;

    public async Task<bool> Handle(AddItemToCartCommand request, CancellationToken ct)
    {
        var cart = await _repository.GetByCustomerIdAsync(request.CustomerId, ct);

        if (cart == null)
        {
            cart = new Cart(request.CustomerId);
            cart.AddOrUpdateItem(request.VariantId, request.ProductName, request.UnitPrice, request.Quantity);
            await _repository.AddAsync(cart, ct);
        }
        else
        {
            cart.AddOrUpdateItem(request.VariantId, request.ProductName, request.UnitPrice, request.Quantity);
            await _repository.UpdateAsync(cart, ct);
        }

        return true;
    }
}