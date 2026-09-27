using BoostorderCatalogShared;

namespace BoostorderCatalog.Services;

public interface ICatalogRepository
{
    Task SaveProductsAsync(List<Product> products);
    Task<List<Product>> GetCachedProductsAsync();
    Task<List<Product>> GetProductsAsync(); // Tries to get from api first, if fail get cache
}