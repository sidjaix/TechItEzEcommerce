using System;

namespace ProductApplication.DTOs;

public class ReviewDto
{
    public string ReviewerAlias { get; set; }
    public int Rating { get; set; }
    public string ReviewText { get; set; }
    public DateTime CreatedAt { get; set; }
}
