namespace Order_Cores.Models;

public partial class ContactUsModel
{
    public int ContactUsId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string MessageDetail { get; set; } = null!;
}
