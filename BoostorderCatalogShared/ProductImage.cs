using System.Text.Json.Serialization;

namespace BoostorderCatalogShared;

public class ProductImage
{
    [JsonPropertyName("src")] public string Src { get; set; } = "";
}