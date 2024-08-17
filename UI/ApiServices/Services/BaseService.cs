using ApiServices.Models;
using ApiServices.Services.IService;
using ApiServices.Utility.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace ApiServices.Services;

public class BaseService : IBaseService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<BaseService> _logger;
    private readonly ITokenProvider _tokenProvider;

    public BaseService(HttpClient client, ILogger<BaseService> logger, ITokenProvider tokenProvider)
    {
        _logger = logger;
        _httpClient = client;
        _tokenProvider = tokenProvider;
    }

    public async Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer = true)
    {
        try
        {
            HttpRequestMessage requestMessage = new();
            if (requestDto.ContentType == ContentType.MultipartFormData)
            {
                requestMessage.Headers.Add("Accept", "*/*");
            }
            else
            {
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            //token
            if (withBearer)
            {
                var token = _tokenProvider.GetToken();
                requestMessage.Headers.Add("Authorization", $"Bearer {token}");
            }

            requestMessage.RequestUri = new Uri(requestDto.Url);

            if (requestDto.ContentType == ContentType.MultipartFormData)
            {
                var content = new MultipartFormDataContent();

                foreach (var prop in requestDto.Data.GetType().GetProperties())
                {
                    var value = prop.GetValue(requestDto.Data);
                    if (value is FormFile)
                    {
                        var file = (FormFile)value;
                        if (file != null)
                        {
                            content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                        }
                    }
                    else
                    {
                        content.Add(new StringContent(value == null ? string.Empty : value.ToString()), prop.Name);
                    }
                }
                requestMessage.Content = content;
            }
            else
            {
                if (requestDto.Data != null)
                {
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }
            }

            HttpResponseMessage apiResponse = null;

            switch (requestDto.ApiMethod)
            {
                case ApiMethod.POST:
                    requestMessage.Method = HttpMethod.Post;
                    break;
                case ApiMethod.DELETE:
                    requestMessage.Method = HttpMethod.Delete;
                    break;
                case ApiMethod.PUT:
                    requestMessage.Method = HttpMethod.Put;
                    break;
                default:
                    requestMessage.Method = HttpMethod.Get;
                    break;
            }

            apiResponse = await _httpClient.SendAsync(requestMessage);

            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new() { IsSuccess = false, Message = "Not Found" };
                case HttpStatusCode.Forbidden:
                    return new() { IsSuccess = false, Message = "Access Denied" };
                case HttpStatusCode.Unauthorized:
                    return new() { IsSuccess = false, Message = "Unauthorized" };
                case HttpStatusCode.InternalServerError:
                    return new() { IsSuccess = false, Message = "Internal Server Error" };
                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                    return apiResponseDto;
            }
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, ex.Message.ToString());
            var dto = new ResponseDto
            {
                Message = ex.Message.ToString(),
                IsSuccess = false
            };
            return dto;
        }
    }
}
