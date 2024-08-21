namespace Cart_Core.Models;

public class CartItemModel
{
    public int CartId { get; set; }
    public int CartItemId { get; set; }
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string ProductName { get; set; }
    public int SellingPrice { get; set; }
    public string ImageUrl { get; set; }
    public int Quantity { get; set; }

    // Read-only property to get the total price of item
    public int ItemTotal => SellingPrice * Quantity;
}
