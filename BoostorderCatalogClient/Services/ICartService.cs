namespace BoostorderCatalog.Services;

public interface ICartService
{
    IReadOnlyList<Models.CartItem> Items { get; }
    event Action? OnChange;
    void AddOrUpdate(Models.Product product, string uom, int quantity);
    void Remove(int productId, string uom);
    void Clear();
    decimal GrandTotal { get; }
    int LineItemCount { get; }
}

