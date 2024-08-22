namespace ApiServices.Services.IService;

public interface ITokenProvider
{
    public void SetToken(string token);
    public string GetToken();
    public void ClearToken();

    public void SetCartItemsCountAndTotalPrice(string token);
    public string GetCartItemsCountAndTotalPrice();

    string GetWishlistItemsCount();
    void SetWishlistItemsCount(string itemCount);
}
