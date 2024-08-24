using System.ComponentModel.DataAnnotations;

namespace Order_Core.Models;

public partial class OrderDetailModel
{
    public int OrderDetialId { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductThumbImageUrl { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
}
