using BoostorderCatalog.Models;

namespace BoostorderCatalog.Services;

public class CatalogRepository : ICatalogRepository
{
    public CatalogRepository()
    {
        throw new NotImplementedException();
    }

    public Task<List<Product>> GetCachedProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveProductsAsync(List<Product> products)
    {
        throw new NotImplementedException();
    }
}