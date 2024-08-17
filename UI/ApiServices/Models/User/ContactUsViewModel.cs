namespace ApiServices.Models.User;

public partial class ContactUsViewModel
{
    public int ContactUsId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string MessageDetail { get; set; } = null!;
}
