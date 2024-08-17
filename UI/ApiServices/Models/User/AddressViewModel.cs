namespace ApiServices.Models.User;

public partial class AddressViewModel
{
    public int AddressId { get; set; }
    public string UserId { get; set; }
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public bool IsShippingAddress { get; set; }
}
