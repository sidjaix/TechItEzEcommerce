namespace ApiServices.Models.Product;

public class ProductPageModel
{
    public List<ProductViewModel> Products { get; set; }
    public List<CategoryViewModel> Categories { get; set; }
    public List<ProductViewModel> LatestProducts { get; set; }
    public List<ProductViewModel> SaleOfProducts { get; set; }
    public List<string> Colors { get; set; } = new List<string>(){
        "White",
        "Gray",
        "Red",
        "Black",
        "Blue",
        "Green",
    };
    public List<string> PopularSizes { get; set; } = new List<string>(){
        "L",
        "M",
        "S",
        "XS",
        "XL",
        "XXL",
    };
}
