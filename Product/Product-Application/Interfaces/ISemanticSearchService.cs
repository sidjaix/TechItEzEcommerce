namespace ProductApplication.Interfaces;

public interface ISemanticSearchService
{
	/// <summary>
	/// Searches for products using natural language intent and returns the best matching Product IDs.
	/// </summary>
	/// <param name="query">The natural language search query.</param>
	/// <param name="top">The maximum number of matches to return.</param>
	/// <param name="cancellationToken">Cancellation token.</param>
	/// <returns>A collection of matching Product IDs.</returns>
	Task<IEnumerable<Guid>> SearchProductIdsAsync(string query, int top = 5, CancellationToken cancellationToken = default);
}