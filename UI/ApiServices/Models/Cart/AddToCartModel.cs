namespace ApiServices.Models.Cart;

public class AddToCartViewModel
{
    public int CartId { get; set; }
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
