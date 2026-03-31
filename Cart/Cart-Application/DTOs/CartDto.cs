using System;

namespace CartApplication.DTOs;

public class CartDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public List<CartItemDto> Items { get; set; } = new();

    // Calculated field for the UI
    public decimal TotalPrice => Items.Sum(i => i.UnitPrice * i.Quantity);
}
