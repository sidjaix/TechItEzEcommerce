using Newtonsoft.Json;
using User_Core.Models;

namespace ApiServices.User;

public class UserService : IUserService
{
    private readonly HttpClient httpClient;

    public UserService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<UserModel>> GetUsersAsync()
    {
        var url = "user";
        var users = new List<UserModel>();
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            users = JsonConvert.DeserializeObject<List<UserModel>>(jsonString);
            return users;
        }
        return users;
    }
}
