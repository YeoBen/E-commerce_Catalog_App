using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BoostorderCatalog.Services;
using BoostorderCatalog;
using System.Text;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var apiUsername = builder.Configuration["BoostorderApi:Username"];
var apiPassword = builder.Configuration["BoostorderApi:Password"];

var credentials = Convert.ToBase64String(
    Encoding.ASCII.GetBytes($"{apiUsername}:{apiPassword}"));

builder.Services.AddHttpClient("BoostorderApi", client =>
{
    client.BaseAddress = new Uri("https://cloud.boostorder.com/bo-mart/api/v1/wp-json/wc/v1/bo/");
});

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IProductApiService, ProductApiService>();
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();

// Test the ProductApiService
try {
    var testService = app.Services.GetRequiredService<IProductApiService>();
    var testProducts = await testService.GetVariableProductsAsync();

    Console.WriteLine($"Fetched {testProducts.Count} variable products");
} 
catch (Exception ex) 
{
    Console.WriteLine($"API call failed: {ex.GetType().Name}: {ex.Message}");
    Console.WriteLine($"API call failed: {ex}");
}

await app.RunAsync();
