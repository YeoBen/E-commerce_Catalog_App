using BoostorderCatalogShared;
using System.Net.Http.Json;

namespace BoostorderCatalog.Services;

public class ProductApiService : IProductApiService
{
    private readonly HttpClient _httpClient;

    public ProductApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Product>> GetVariableProductsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<Product>>("api/products");
        return response ?? new();
    }
}