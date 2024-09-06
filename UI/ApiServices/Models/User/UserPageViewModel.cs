using ApiServices.Models.Order;

namespace ApiServices.Models.User;

public class UserPageViewModel
{
    public UserViewModel PersonalInfo { get; set; }
    public List<AddressViewModel> Addresses { get; set; } = [];
    public AddressViewModel Address { get; set; } = new AddressViewModel();
    // public List<PaymentViewModel> PaymentMethods { get; set; } = [];
    // public PaymentViewModel Paymentmethod { get; set; }
}
