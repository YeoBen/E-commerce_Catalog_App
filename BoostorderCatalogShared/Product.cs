using System.Text.Json.Serialization;

namespace BoostorderCatalogShared;

public class Product
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("type")] public string Type { get; set; } = "";
    [JsonPropertyName("sku")] public string Sku { get; set; } = "";
    [JsonPropertyName("stock_quantity")] public int StockQuantity { get; set; }
    [JsonPropertyName("images")] public List<ProductImage> Images { get; set; } = new();
    [JsonPropertyName("variations")] public List<Variation> Variations { get; set; } = new();
}