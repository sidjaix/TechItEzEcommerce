using System;

namespace ProductApplication.DTOs;

public class CategoryNodeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<CategoryNodeDto> SubCategories { get; set; } = new();
}
