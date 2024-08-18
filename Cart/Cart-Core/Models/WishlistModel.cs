
namespace Cart_Core.Models;
public partial class WishlistModel
{
    public int WishlistId { get; set; }
    public string UserId { get; set; }
    public List<WishlistItemModel> WishlistItems { get; set; }
}
