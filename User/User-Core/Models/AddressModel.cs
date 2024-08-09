namespace User_Core.Models;

public partial class AddressModel
{
    public int AddressId { get; set; }
    public int CustomerId { get; set; }
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public bool IsShippingAddress { get; set; }
}
