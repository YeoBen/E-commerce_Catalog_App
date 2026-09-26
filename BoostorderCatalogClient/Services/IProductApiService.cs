namespace BoostorderCatalog.Services;

public interface IProductApiService
{
    Task<List<Models.Product>> GetVariableProductsAsync();
}