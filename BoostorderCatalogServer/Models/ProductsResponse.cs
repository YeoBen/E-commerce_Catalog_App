using System.Text.Json.Serialization;
using BoostorderCatalogShared;

namespace BoostorderCatalogServer.Models;

public class ProductsResponse
{
    [JsonPropertyName("products")]
    public List<Product> Products { get; set; } = new();
}