namespace ApiServices.Models.Cart;
public partial class CartViewModel
{
    public int CartId { get; set; }
    public string UserId { get; set; }
    public List<CartItemViewModel> CartItems { get; set; } = [];
}
