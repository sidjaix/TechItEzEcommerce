using System;

namespace ProductApplication.DTOs;

public class DeductInventoryItemDto
{
    public Guid VariantId { get; set; }
    public int Quantity { get; set; }
}
