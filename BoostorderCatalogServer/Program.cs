var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseBlazorFrameworkFiles(); // serves the Client's compiled WASM files
app.UseStaticFiles();

app.MapControllers();
app.MapFallbackToFile("index.html"); // Client's index.html for client-side routing

app.Run();
