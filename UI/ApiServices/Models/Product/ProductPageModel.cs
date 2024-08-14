namespace ApiServices.Models.Product;

public class ProductPageModel
{
    public List<ProductModel> Products { get; set; }
    public List<CategoryModel> Categories { get; set; }
    public List<ProductModel> LatestProducts { get; set; }
    public List<ProductModel> SaleOfProducts { get; set; }
    public List<string> Colors { get; set; } = new List<string>(){
        "White",
        "Yellow",
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
