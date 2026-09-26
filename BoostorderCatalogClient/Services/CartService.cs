using BoostorderCatalog.Models;

namespace BoostorderCatalog.Services;

public class CartService : ICartService
{
    public CartService(ICatalogRepository catalogRepository)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<CartItem> Items => throw new NotImplementedException();

    public decimal GrandTotal => throw new NotImplementedException();

    public int LineItemCount => throw new NotImplementedException();

    public event Action? OnChange;

    public void AddOrUpdate(Product product, string uom, int quantity)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public void Remove(int productId, string uom)
    {
        throw new NotImplementedException();
    }
}