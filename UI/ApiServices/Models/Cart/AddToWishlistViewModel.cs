namespace ApiServices.Models.Cart;

public class AddToWishlistViewModel
{
    public int WishlistId { get; set; }
    public string UserId { get; set; }
    public int ProductId { get; set; }
}
