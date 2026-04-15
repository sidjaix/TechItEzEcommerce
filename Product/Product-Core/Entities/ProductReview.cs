using System;

namespace ProductCore.Entities;

public class ProductReview
{
	public Guid Id { get; private set; }

	/// <summary>Foreign Key linking the review to the overarching product concept.</summary>
	public Guid CatalogItemId { get; private set; }

	/// <summary>A "Soft Reference" (just a Guid string) pointing to the external User Domain. No hard SQL relationship.</summary>
	public Guid CustomerId { get; private set; }

	/// <summary>Numerical score, typically 1-5.</summary>
	public int Rating { get; private set; }

	/// <summary>The display name of the person leaving the review.</summary>
	public string ReviewerAlias { get; private set; }

	/// <summary>The actual user sentiment. The most valuable field for AI recommendations.</summary>
	public string ReviewText { get; private set; }

	public DateTime CreatedAt { get; private set; }

	public ProductReview()
	{

	}
	public ProductReview(Guid catalogItemId, Guid customerId, int rating, string reviewerAlias, string reviewText)
	{
		CatalogItemId = catalogItemId;
		CustomerId = customerId;
		Rating = rating;
		ReviewerAlias = reviewerAlias;
		ReviewText = reviewText;
		CreatedAt = DateTime.UtcNow;
	}
}
