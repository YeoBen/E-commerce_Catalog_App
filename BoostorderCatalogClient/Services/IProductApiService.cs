using BoostorderCatalogShared;

namespace BoostorderCatalog.Services;

public interface IProductApiService
{
    Task<List<Product>> GetVariableProductsAsync();
}