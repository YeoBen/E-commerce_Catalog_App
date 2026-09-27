using BoostorderCatalog.Models;
using BoostorderCatalogShared;

namespace BoostorderCatalog.Services;

public class CartService : ICartService
{
    private readonly List<CartItem> _items = new();

    public IReadOnlyList<CartItem> Items => _items;

    public event Action? OnChange;

    public void AddOrUpdate(Product product, string uom, int quantity)
    {
        var variation = product.Variations.FirstOrDefault(v => v.Uom == uom);
        if (variation is null) return;

        var existing = _items.FirstOrDefault(i => i.ProductId == product.Id && i.Uom == uom);
        if (existing is not null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            _items.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductSku = product.Sku,
                ImageUrl = product.Images.FirstOrDefault()?.Src ?? "",
                Uom = uom,
                Quantity = quantity,
                UnitPrice = variation.Price,
                StockQuantity = variation.StockQuantity
            });
        }

        OnChange?.Invoke();
    }

    public void Remove(int productId, string uom)
    {
        _items.RemoveAll(i => i.ProductId == productId && i.Uom == uom);
        OnChange?.Invoke();
    }

    public void Clear()
    {
        _items.Clear();
        OnChange?.Invoke();
    }

    public decimal GrandTotal => _items.Sum(i => i.Total);

    public int LineItemCount => _items.Count;
}