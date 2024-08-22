namespace ApiServices.Models.Cart;
public partial class WishlistItemViewModel
{
    public int WishlistId { get; set; }
    public int WishlistItemId { get; set; }
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string ProductName { get; set; }
    public string ImageUrl { get; set; }
    public int SellingPrice { get; set; }

}
