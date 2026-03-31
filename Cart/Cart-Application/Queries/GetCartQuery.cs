using MediatR;
using CartApplication.DTOs;
using CartApplication.Interfaces;

public record GetCartQuery(Guid CustomerId) : IRequest<CartDto>;

public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartDto>
{
    private readonly ICartRepository _repository;
    public GetCartQueryHandler(ICartRepository repository) => _repository = repository;

    public async Task<CartDto> Handle(GetCartQuery request, CancellationToken ct)
    {
        var cart = await _repository.GetByCustomerIdAsync(request.CustomerId, ct);

        // If no cart exists, return an empty one for the UI instead of null
        if (cart == null) return new CartDto { CustomerId = request.CustomerId };

        return new CartDto
        {
            Id = cart.Id,
            CustomerId = cart.CustomerId,
            Items = cart.Items.Select(i => new CartItemDto
            {
                VariantId = i.VariantId,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };
    }
}