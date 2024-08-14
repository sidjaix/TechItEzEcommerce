namespace ApiServices.Services.IService;

public interface ITokenProvider
{
    public void SetToken(string token);
    public string GetToken();
    public void ClearToken();
}
