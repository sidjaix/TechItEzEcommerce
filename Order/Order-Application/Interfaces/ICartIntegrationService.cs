using System;

namespace OrderApplication.Interfaces;

public class CartIntegrationDto
{
    public Guid CustomerId { get; set; }
    public List<CartItemIntegrationDto> Items { get; set; } = new();
}

public class CartItemIntegrationDto
{
    public Guid VariantId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}

public interface ICartIntegrationService
{
    Task<CartIntegrationDto> GetActiveCartAsync(Guid customerId, CancellationToken cancellationToken);
}
