using System;

namespace ProductApplication.DTOs;

public class CatalogItemDetailDto : CatalogItemDto
{
    public string SemanticDescription { get; set; }
    public List<VariantDto> Variants { get; set; } = new();
    public List<ReviewDto> Reviews { get; set; } = new();
    public List<string> Tags { get; set; } = new();
}
