using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http.Headers;

namespace ApiCommon.Handlers;

public class TokenDelegatingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // 1. Check if we are currently inside an active HTTP context
        if (_httpContextAccessor.HttpContext != null)
        {
            // 2. Extract the token from the incoming request
            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            // 3. If a token exists, attach it to the outgoing request
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // 4. Proceed with the outgoing request
        return await base.SendAsync(request, cancellationToken);
    }

}
