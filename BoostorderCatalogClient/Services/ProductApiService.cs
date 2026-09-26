using BoostorderCatalog.Models;
using System.Net.Http.Json;

namespace BoostorderCatalog.Services;

public class ProductApiService : IProductApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public ProductApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<List<Product>> GetVariableProductsAsync()
    {
        var client = _httpClientFactory.CreateClient("BoostorderApi");
        var consumerKey = _configuration["BoostorderApi:Username"];
        var consumerSecret = _configuration["BoostorderApi:Password"];

        Console.WriteLine($"Using consumer key: {consumerKey}");
        Console.WriteLine($"Using consumer secret: {consumerSecret}");

        var allProducts = new List<Product>();
        int page = 1;
        int totalPages = 1;

        do
        {
            var response = await client.GetAsync(
                $"products?page={page}&consumer_key={consumerKey}&consumer_secret={consumerSecret}");
            response.EnsureSuccessStatusCode();

            if (response.Headers.TryGetValues("X-WP-TotalPages", out var values))
            {
                totalPages = int.Parse(values.First());
            }

            var pageProducts = await response.Content.ReadFromJsonAsync<List<Product>>()
                                ?? new List<Product>();
            allProducts.AddRange(pageProducts);

            page++;
        }
        while (page <= totalPages);

        return allProducts.Where(p => p.Type == "variable").ToList();
    }
}