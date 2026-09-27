using System.Text.Json.Serialization;

namespace BoostorderCatalogShared;

public class Variation
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("sku")] public string Sku { get; set; } = "";
    [JsonPropertyName("uom")] public string Uom { get; set; } = "";
    [JsonPropertyName("stock_quantity")] public int StockQuantity { get; set; }

    [JsonPropertyName("regular_price")]
    public string RegularPriceRaw { get; set; } = "0";

    // Convenience property — not mapped directly, parsed from the string above
    [JsonIgnore]
    public decimal Price => decimal.TryParse(RegularPriceRaw, out var p) ? p : 0m;
}