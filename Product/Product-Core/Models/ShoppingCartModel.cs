namespace Product_Core.Models;
public partial class ShoppingCartModel
{
    public int CartId { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
