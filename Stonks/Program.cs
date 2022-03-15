using Stonks.Configuration;
using Stonks.Contracts;
using Stonks.Mapping;
using Stonks.Models;
using Stonks.Services;
using ConfigurationSection = Stonks.Configuration.ConfigurationSection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IStockDataProvider, PolygonService>();
builder.Services.AddTransient<IStockPriceService, StockPriceService>();
builder.Services.AddSingleton<ICache<IEnumerable<StockPrice>>, InMemoryStockPriceCache>();

var polygonConfiguration = builder.Configuration.GetSection(ConfigurationSection.Polygon).Get<PolygonConfiguration>();
builder.Services.AddSingleton(polygonConfiguration);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
