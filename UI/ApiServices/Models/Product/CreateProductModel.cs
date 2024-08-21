using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ApiServices.Models.Product;

public class CreateProductViewModel
{
    public int ProductId { get; set; }

    [Required]
    public string ProductName { get; set; }

    [Required]
    public string Description { get; set; }

    [Required(ErrorMessage = "Please select a category.")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category.")]
    public int? CategoryId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Selling Price must be greater than zero.")]
    public int SellingPrice { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Original Price must be greater than zero.")]
    public int OriginalPrice { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity in Stock must be a non-negative integer.")]
    public int QuantityInStock { get; set; }
    public string ImageUrl { get; set; }

    public List<CategoryViewModel> Categories { get; set; } = [];

    [Display(Name = "Upload Product Image")]
    public IFormFile ProductImage { get; set; }
}