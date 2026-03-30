using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace CartApi.Utility;

public class AuthenticationDelegateHandler(IHttpContextAccessor accessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await accessor.HttpContext.GetTokenAsync("access_token");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}
