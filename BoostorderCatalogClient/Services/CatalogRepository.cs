using BoostorderCatalogShared;
using Blazored.LocalStorage;

namespace BoostorderCatalog.Services;

public class CatalogRepository : ICatalogRepository
{
    private const string CacheKey = "cached_Products";
    private readonly ILocalStorageService _localStorage;
    private readonly IProductApiService _productApiService;

    public CatalogRepository(ILocalStorageService localStorage, IProductApiService productApiService)
    {
        _localStorage = localStorage;
        _productApiService = productApiService;
    }

    public async Task SaveProductsAsync(List<Product> products)
    {
        await _localStorage.SetItemAsync(CacheKey, products);
    }

    public async Task<List<Product>> GetCachedProductsAsync()
    {
        var cached = await _localStorage.GetItemAsync<List<Product>>(CacheKey);
        return cached ?? new List<Product>();
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        try
        {
            var liveProduct = await _productApiService.GetVariableProductsAsync();
            await SaveProductsAsync(liveProduct);
            return liveProduct;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Live fetch failed, falling back to cache: {ex.Message}");
            return await GetCachedProductsAsync();
        }
    }
}