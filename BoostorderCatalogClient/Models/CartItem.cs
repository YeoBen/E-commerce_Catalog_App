namespace BoostorderCatalog.Models;

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public string ProductSku { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public string Uom { get; set; } = "";
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int StockQuantity { get; set; }
    public decimal Total => UnitPrice * Quantity;
}