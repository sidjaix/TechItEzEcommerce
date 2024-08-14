using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Core.Entities;

[Table("ProductItem")]
public class ProductItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string SKU { get; set; }
    public int QuantityInStock { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }
}
