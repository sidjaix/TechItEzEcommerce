using ApiServices.Utility.Enums;

namespace ApiServices.Models;

public class RequestDto
{
    public string Url { get; set; }
    public object Data { get; set; }
    public string AccessToken { get; set; }
    public ApiMethod ApiMethod { get; set; } = ApiMethod.GET;
    public ContentType ContentType { get; set; } = ContentType.Json;
}
