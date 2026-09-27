using Microsoft.AspNetCore.Mvc;
using BoostorderCatalogServer.Models;
using BoostorderCatalogShared;

namespace BoostorderCatalogServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;


    public ProductsController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> GetVariableProducts()
    {
        var client = _httpClientFactory.CreateClient();
        var username = _configuration["BoostorderApi:Username"];
        var password = _configuration["BoostorderApi:Password"];
        var credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
        client.DefaultRequestHeaders.Authorization = new("Basic", credentials);

        var allProducts = new List<Product>();
        int page = 1, totalPages = 1;

        do
        {
            var response = await client.GetAsync(
                $"https://cloud.boostorder.com/bo-mart/api/v1/wp-json/wc/v1/bo/products?page={page}");
            response.EnsureSuccessStatusCode();

            if (response.Headers.TryGetValues("X-WP-TotalPages", out var values))
            {
                totalPages = int.Parse(values.First());
            }

            var pageResult = await response.Content.ReadFromJsonAsync<ProductsResponse>();
            allProducts.AddRange(pageResult?.Products ?? new());
            page++;
        }
        while (page <= totalPages);

        return Ok(allProducts.Where(p => p.Type == "variable").ToList());
    }
}