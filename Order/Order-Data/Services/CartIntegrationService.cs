using OrderApplication.Interfaces;
using System.Net.Http.Json;

namespace OrderData.Services;

public class CartIntegrationService : ICartIntegrationService
{
	private readonly HttpClient _httpClient;

	public CartIntegrationService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<CartIntegrationDto> GetActiveCartAsync(Guid customerId, CancellationToken cancellationToken)
	{
		// Making the HTTP GET request to the Cart API via the Gateway
		var response = await _httpClient.GetAsync($"/api/Cart/GetCart", cancellationToken);

		if (!response.IsSuccessStatusCode)
		{
			return null; // Cart not found or error
		}

		// Deserialize the JSON response into our local DTO
		return await response.Content.ReadFromJsonAsync<CartIntegrationDto>(cancellationToken: cancellationToken);
	}

}
