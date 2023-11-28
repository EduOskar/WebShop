using Microsoft.Net.Http.Headers;
using WebShop.Web.Components;
using WebShop.Web.Services;
using WebShop.Web.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7066/") });
builder.Services.AddHttpClient<IProductsService, ProductsService>().ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7066/"));
builder.Services.AddHttpClient<ICartItemsService, CartItemsService>().ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7066/"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()   
    .AddInteractiveServerRenderMode();

app.Run();
