using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Text.Json.Serialization;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories;
using WebShop.Api.Repositories.Contracts;
using WebShop.Api.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IEmailSenderRepository, EmailSenderRepository>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureSwaggerGen(c => c.CustomSchemaIds(c => c.FullName));

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseSqlServer(builder
    .Configuration.GetConnectionString("WebShop")));

builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
opt.TokenLifespan = TimeSpan.FromHours(2));


//Todo: Update password reqirements for prouction
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

builder.Services.AddControllers()
   .AddJsonOptions(options =>
   {
       options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
   });


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRolesRepository, UserRolesRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<ISupportrepository, SupportRepository>();        
builder.Services.AddScoped<CartOrderTransferService>();
builder.Services.AddScoped<OrderWorkerService>();
builder.Services.AddScoped<ApplyDiscountToProductServices>();
builder.Services.AddScoped<SupportEmailService>();
builder.Services.AddScoped<SupportMessageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(policy =>
    policy.WithOrigins("http://localhost:7104", "https://localhost:7104")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
    .AllowCredentials()
);

app.UseHttpsRedirection();

app.UseAuthentication().UseCookiePolicy();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var dbContext = services.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.EnsureDeleted();

    dbContext.Database.EnsureCreated();

    var roles = new[] { "Admin", "User", "Warehouse Worker", "Support" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
    }

    string email = "admin@admin.com";
    string userName = "Admin";
    string password = "Hejsan123!";

    var user = new User();

    if (await userManager.FindByNameAsync(userName) == null)
    {

        user.Email = email;
        user.UserName = userName;
        user.PasswordHash = password;
        user.FirstName = "Adi";
        user.LastName = "Administratum";
        user.Adress = "Adminstreet";
        user.PhoneNumber = "+46 - 333 333 33";

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");
    }

    string userName2 = "WareHouse";
    string password2 = "Hejsan123!";
    string email2 = "Ware@House.com";

    var user2 = await userManager.FindByNameAsync(userName2);
    if (user2 == null)
    {
        user2 = new User
        {
            Email = email2,
            UserName = userName2,
            FirstName = "Ware",
            LastName = "House",
            Adress = "Warehouse Street 1",
            PhoneNumber = "+46 - 333 333 33"
        };

        var createUserResult = await userManager.CreateAsync(user2, password2);

        await userManager.AddToRoleAsync(user2, "Warehouse Worker");



        string userName3 = "Support";
        string password3 = "Hejsan123!";
        string email3 = "Support@Support.com";

        var user3 = await userManager.FindByNameAsync(userName3);
        if (user3 == null)
        {
            user3 = new User
            {
                Email = email3,
                UserName = userName3,
                FirstName = "Support",
                LastName = "Supportium",
                Adress = "Support Street 1",
                PhoneNumber = "+46 - 332 332 32"
            };

            var createUserResult2 = await userManager.CreateAsync(user3, password3);

            await userManager.AddToRoleAsync(user3, "Support");

            if (!dbContext.Carts.Any(c => c.UserId == user.Id))
            {
                var cart = new Cart
                {
                    UserId = user.Id
                };

                dbContext.Carts.Add(cart);
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.Carts.Any(c => c.UserId == user3.Id))
            {
                var cart3 = new Cart
                {
                    UserId = user3.Id
                };

                dbContext.Carts.Add(cart3);
                await dbContext.SaveChangesAsync();
            }
        }

    }
}

app.Run();
