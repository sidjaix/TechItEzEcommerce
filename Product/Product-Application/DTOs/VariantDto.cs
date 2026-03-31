using System;

namespace ProductApplication.DTOs;

public class VariantDto
{
    public Guid Id { get; set; }
    public string Sku { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string AttributesJson { get; set; }
    public List<ImageDto> Images { get; set; } = new();
}
