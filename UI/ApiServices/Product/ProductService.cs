using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace ApiServices.Product;

public class ProductService : IProductService
{
    private readonly HttpClient httpClient;
    public ProductService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Product_Core.Entities.Product> CreateNewProductAsync(Product_Core.Entities.Product productModel)
    {
        var url = $"product";
        var product = new Product_Core.Entities.Product();
        // Convert the data to JSON
        var jsonData = JsonConvert.SerializeObject(product);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responce = await httpClient.PostAsJsonAsync(url, content);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            product = JsonConvert.DeserializeObject<Product_Core.Entities.Product>(jsonString);
            return product;
        }
        return product;
    }

    public async Task<Product_Core.Entities.Product> UpdateExistingProductAsync(Product_Core.Entities.Product productModel)
    {
        var url = $"product/update";
        var product = new Product_Core.Entities.Product();
        // Convert the data to JSON
        var jsonData = JsonConvert.SerializeObject(product);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responce = await httpClient.PostAsJsonAsync(url, content);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            product = JsonConvert.DeserializeObject<Product_Core.Entities.Product>(jsonString);
            return product;
        }
        return product;
    }

    public async Task<List<Product_Core.Entities.Product>> GetProductsAsync()
    {
        var url = "product";
        var products = new List<Product_Core.Entities.Product>();
        var responce = await httpClient.GetAsync(url);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            products = JsonConvert.DeserializeObject<List<Product_Core.Entities.Product>>(jsonString);
            return products;
        }
        return products;
    }

    public async Task<Product_Core.Entities.Product> GetProductDetailAsync(int productId)
    {
        var url = $"product/{productId}";
        var product = new Product_Core.Entities.Product();
        var responce = await httpClient.GetAsync(url);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            product = JsonConvert.DeserializeObject<Product_Core.Entities.Product>(jsonString);
            return product;
        }
        return product;
    }

    public async Task<List<Product_Core.Entities.Product>> GetProductsByCategoryAsync(int categoriesId)
    {
        var url = $"product/GetProductByCategory/{categoriesId}";
        var products = new List<Product_Core.Entities.Product>();
        var responce = await httpClient.GetAsync(url);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            products = JsonConvert.DeserializeObject<List<Product_Core.Entities.Product>>(jsonString);
            return products;
        }
        return products;
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
        var url = $"product/{productId}";
        var responce = await httpClient.DeleteAsync(url);
        return responce.IsSuccessStatusCode;
    }
}
