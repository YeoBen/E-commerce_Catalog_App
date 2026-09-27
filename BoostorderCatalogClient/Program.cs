using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BoostorderCatalog.Services;
using BoostorderCatalog;
using System.Text;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var apiUsername = builder.Configuration["BoostorderApi:Username"];
var apiPassword = builder.Configuration["BoostorderApi:Password"];

var credentials = Convert.ToBase64String(
    Encoding.ASCII.GetBytes($"{apiUsername}:{apiPassword}"));

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IProductApiService, ProductApiService>();
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped(sp => new HttpClient {
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

var app = builder.Build();

// Test the ProductApiService
try {
    var catalogRepo = app.Services.GetRequiredService<ICatalogRepository>();
    var products = await catalogRepo.GetProductsAsync();

    Console.WriteLine($"Loaded {products.Count} products (live or cached)");
} 
catch (Exception ex) 
{
    Console.WriteLine($"Catalog load failed: {ex}");
}

await app.RunAsync();
