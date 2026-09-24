using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BoostorderCatalog;
using BoostorderCatalog.Services;
using System.Text;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var credentials = Convert.ToBase64String(
    Encoding.ASCII.GetBytes("YOUR_CONSUMER_KEY:YOUR_CONSUMER_SECRET"));

builder.Services.AddHttpClient("BoostorderApi", client =>
{
    client.BaseAddress = new Uri("https://cloud.boostorder.com/bo-mart/api/v1/wp-json/wc/v1/bo/");
    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
});

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IProductApiService, ProductApiService>();
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<ICartService, CartService>();

await builder.Build().RunAsync();
