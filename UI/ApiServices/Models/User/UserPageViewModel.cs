using ApiServices.Models.Cart;
using ApiServices.Models.Order;

namespace ApiServices.Models.User;

public class UserPageViewModel
{
    public UserViewModel PersonalInfo { get; set; }
    public List<OrderViewModel> Orders { get; set; }
    public WishlistViewModel Wishlist { get; set; }
    public List<AddressViewModel> Addresses { get; set; } = [];
    public AddressViewModel Address { get; set; } = new AddressViewModel();
}
