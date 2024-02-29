using Microsoft.AspNetCore.ResponseCompression;
using WebShop.Web.Hubs;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using WebShop.Web.Components;
using WebShop.Web.Services;
using WebShop.Web.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
});

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

builder.Services.AddHttpClient("WebShop.Api", x => x.BaseAddress = new Uri("https://localhost:7066/"))
    .ConfigurePrimaryHttpMessageHandler(() => handler);

builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IUsersAndRoleService, UsersAndRoleService>();
builder.Services.AddScoped<IProductsCategoryService, ProductCategoryService>();
builder.Services.AddScoped<ICartsService, CartsService>();
builder.Services.AddScoped<ICartItemsService, CartItemsService>();
builder.Services.AddScoped<IUsersService, UsersServices>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IOrderItemsService, OrderItemsService>();
builder.Services.AddScoped<ICartOrderTransferService, CartOrderTransferService>();
builder.Services.AddScoped<IReviewServices, ReviewService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<ISupportService, SupportService>();
builder.Services.AddSingleton<CategoryStateService>();
builder.Services.AddTransient<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<ISupportService, SupportService>();
builder.Services.AddScoped<DiscountCodeState>();
builder.Services.AddScoped<CustomStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddBlazorBootstrap();
builder.Services.AddRadzenComponents();

//builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

app.UseResponseCompression();

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

app.MapHub<ChatHub>("/chathub");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
