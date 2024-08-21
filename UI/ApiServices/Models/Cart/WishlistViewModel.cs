namespace ApiServices.Models.Cart;

public class WishlistViewModel
{
    public int WishlistIdId { get; set; }
    public string UserId { get; set; }
    public List<WishlistItemViewModel> WishlistItems { get; set; }
}
