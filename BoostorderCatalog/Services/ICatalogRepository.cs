namespace BoostorderCatalog.Services;

public interface ICatalogRepository
{
    Task SaveProductsAsync(List<Models.Product> products);
    Task<List<Models.Product>> GetCachedProductsAsync();
}