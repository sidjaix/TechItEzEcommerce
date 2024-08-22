namespace Cart_Core.Models;

public class AddToWishlistModel
{
    public int WishlistId { get; set; }
    public string UserId { get; set; }
    public int ProductId { get; set; }
}
