using System;

namespace CartApplication.DTOs;

public class CartItemDto
{
    public Guid VariantId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}
