using System;

namespace ProductApplication.DTOs;

public class ImageDto
{
    public string ImageUrl { get; set; }
    public string AltText { get; set; }
    public bool IsPrimary { get; set; }
}
