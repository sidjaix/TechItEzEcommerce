using System;

namespace OrderApplication.DTOs;

public class OrderItemDto
{
    public Guid VariantId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}
