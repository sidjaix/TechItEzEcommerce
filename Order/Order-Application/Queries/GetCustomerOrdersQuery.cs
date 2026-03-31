using MediatR;
using OrderApplication.DTOs;
using OrderApplication.Interfaces;
using System;

namespace OrderApplication.Queries;

public record GetCustomerOrdersQuery(Guid CustomerId) : IRequest<IEnumerable<OrderDto>>;

public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _repository;
    public GetCustomerOrdersQueryHandler(IOrderRepository repository) => _repository = repository;

    public async Task<IEnumerable<OrderDto>> Handle(GetCustomerOrdersQuery request, CancellationToken ct)
    {
        var orders = await _repository.GetByCustomerIdAsync(request.CustomerId, ct);

        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            CustomerId = o.CustomerId,
            OrderDate = o.OrderDate,
            Status = o.Status.ToString(),
            TotalAmount = o.TotalAmount,
            ShippingAddress = new AddressDto
            {
                Street = o.ShippingAddress.Street,
                City = o.ShippingAddress.City,
                State = o.ShippingAddress.State,
                Country = o.ShippingAddress.Country,
                ZipCode = o.ShippingAddress.ZipCode
            },
            Items = o.Items.Select(i => new OrderItemDto
            {
                VariantId = i.VariantId,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        });
    }
}
