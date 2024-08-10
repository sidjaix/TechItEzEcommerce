using ApiServices.Models;

namespace ApiServices.Services.IService;

public interface IBaseService
{
    Task<ResponseDto> SendAsync(RequestDto requestDto);//, bool withBearer = true);
    public string BaseAddress { get; set; }
}
