using Cart_Core.Models;

namespace Cart_Core.EventModel;

public class OrderCreateEvent
{
    public int CartId { get; set; }
    public string UserId { get; set; }
    public int CartItemCount { get; set; }
    public int CartItemsTotalPrice { get; set; }
    public List<CartItemModel> CartItems { get; set; } = [];
}
