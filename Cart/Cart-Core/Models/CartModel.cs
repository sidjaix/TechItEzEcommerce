namespace Cart_Core.Models;
public partial class CartModel
{
    public int CartId { get; set; }
    public string UserId { get; set; }
    public int CartItemCount { get; set; }
    public int CartItemsTotalPrice { get; set; }
    public List<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();
}
