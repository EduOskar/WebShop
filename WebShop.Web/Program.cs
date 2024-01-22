
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using WebShop.Web.Components;
using WebShop.Web.Services;
using WebShop.Web.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddServerSideBlazor();

builder.Services.AddOptions();
builder.Services.AddAuthenticationCore();
builder.Services.AddAuthorizationCore();


var cookieContainer = new System.Net.CookieContainer();
var handler = new HttpClientHandler
{
    UseCookies = true,
    CookieContainer = cookieContainer
   
    
};

builder.Services.AddSingleton(cookieContainer);
builder.Services.AddSingleton(handler);

builder.Services.AddHttpClient("WebShop.Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7066/");
})

.ConfigurePrimaryHttpMessageHandler(() => handler);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7066/") });
builder.Services.AddHttpClient<IProductsService, ProductsService>().ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7066/"));
builder.Services.AddHttpClient<IUsersAndRoleService, UsersAndRoleService>().ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7066/"));
builder.Services.AddHttpClient<IProductsCategoryService, ProductCategoryService>().ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7066/"));
builder.Services.AddHttpClient<ICartsService, CartsService>().ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7066/"));
builder.Services.AddHttpClient<ICartItemsService, CartItemsService>().ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7066/"));
builder.Services.AddHttpClient<IUsersService, UsersServices>().ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7066/"));
builder.Services.AddHttpClient<IOrdersService, OrdersService>().ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7066/"));
builder.Services.AddHttpClient<IOrderItemsService, OrderItemsService>().ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7066/"));
builder.Services.AddHttpClient<ICartOrderTransferService, CartOrderTransferService>().ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7066/"));
builder.Services.AddHttpClient<IReviewServices, ReviewService>().ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:7066/"));
builder.Services.AddSingleton<CategoryStateService>();

builder.Services.AddScoped<CustomStateProvider>(); 
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddBlazorBootstrap();
builder.Services.AddRadzenComponents();
//builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication().UseCookiePolicy();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
