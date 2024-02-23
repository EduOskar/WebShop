using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebShop.Api.Entity;

namespace WebShop.Api.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    new public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<OrderStatus> OrderStatus { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<DiscountUsage> DiscountUsages { get; set; }
    public DbSet<ProductsDiscount> ProductDiscounts { get; set; }
    public DbSet<SupportMail> SupportMails { get; set; }
    public DbSet<SupportMessages> SupportMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(customer =>
        {
            customer
                .HasMany(c => c.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            customer
                .HasMany(c => c.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            customer
            .HasMany(sm => sm.SupportMails)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId);
        });

        modelBuilder.Entity<Product>(product =>
        {
            product.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
            product
                .HasMany(p => p.Reviews)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId);
        });

        modelBuilder.Entity<Cart>(cart =>
        {
            cart.HasOne(c => c.User).WithOne();
        });

        modelBuilder.Entity<CartItem>(cartItem =>
        {

            cartItem
                .HasOne(ci => ci.Cart)
                .WithMany(ca => ca.CartItems)
                .HasForeignKey(ci => ci.CartId);
            cartItem.HasOne(ci => ci.Product).WithMany().HasForeignKey(ci => ci.ProductId);
        });

        modelBuilder.Entity<OrderItem>(orderItem =>
        {
            orderItem
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
            orderItem.HasOne(oi => oi.Product).WithMany().HasForeignKey(oi => oi.ProductId);
        });

        modelBuilder.Entity<Order>(order =>
        {
            order
            .HasOne(os => os.OrderStatus)
            .WithMany()
            .HasForeignKey(os => os.OrderStatusId);
        });

        //modelBuilder.Entity<SupportMail>(supportMail =>
        // supportMail.HasOne(u => u.User)
        // .WithMany()
        // .HasForeignKey(u => u.UserId)
        //);

        //modelBuilder.Entity<SupportMail>(supportMail =>
        //supportMail.HasOne(s => s.Support)
        //.WithMany()
        //.HasForeignKey(s => s.SupportId)
        //.IsRequired(false)
        //);

        //modelBuilder.Entity<SupportMail>(supportMail =>
        //{
        //    supportMail.HasKey(sm => sm.Id);

        //    supportMail.HasMany(sm => sm.Messages)
        //    .WithOne(sm => sm.SupportMail)
        //    .HasForeignKey(m => m.SupportMailId);
        //});

        //Products
        //Beauty Category
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 1,
            Name = "Glossier - Beauty Kit",
            Description = "A kit provided by Glossier, containing skin care, hair care and makeup products",
            ImageURL = "/Images/Beauty/Beauty1.png",
            Price = 1000,
            Quantity = 100,
            Status = 0,
            CategoryId = 1

        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 2,
            Name = "Curology - Skin Care Kit",
            Description = "A kit provided by Curology, containing skin care products",
            ImageURL = "/Images/Beauty/Beauty2.png",
            Price = 500,
            Quantity = 45,
            Status = 0,
            CategoryId = 1

        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 3,
            Name = "Cocooil - Organic Coconut Oil",
            Description = "A kit provided by Curology, containing skin care products",
            ImageURL = "/Images/Beauty/Beauty3.png",
            Price = 200,
            Quantity = 30,
            Status = 0,
            CategoryId = 1

        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 4,
            Name = "Schwarzkopf - Hair Care and Skin Care Kit",
            Description = "A kit provided by Schwarzkopf, containing skin care and hair care products",
            ImageURL = "/Images/Beauty/Beauty4.png",
            Price = 500,
            Quantity = 60,
            Status = 0,
            CategoryId = 1

        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 5,
            Name = "Skin Care Kit",
            Description = "Skin Care Kit, containing skin care and hair care products",
            ImageURL = "/Images/Beauty/Beauty5.png",
            Price = 300,
            Quantity = 85,
            Status = 0,
            CategoryId = 1

        });
        //Electronics Category
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 6,
            Name = "Air Pods",
            Description = "Air Pods - in-ear wireless headphones",
            ImageURL = "/Images/Electronic/Electronics1.png",
            Price = 1000,
            Quantity = 120,
            Status = 0,
            CategoryId = 3

        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 7,
            Name = "On-ear Golden Headphones",
            Description = "On-ear Golden Headphones - these headphones are not wireless",
            ImageURL = "/Images/Electronic/Electronics2.png",
            Price = 499,
            Quantity = 200,
            Status = 0,
            CategoryId = 3

        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 8,
            Name = "On-ear Black Headphones",
            Description = "On-ear Black Headphones - these headphones are not wireless",
            ImageURL = "/Images/Electronic/Electronics3.png",
            Price = 499,
            Quantity = 300,
            Status = 0,
            CategoryId = 3

        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 9,
            Name = "Sennheiser Digital Camera with Tripod",
            Description = "Sennheiser Digital Camera - High quality digital camera provided by Sennheiser - includes tripod",
            ImageURL = "/Images/Electronic/Electronics4.png",
            Price = 5999,
            Quantity = 20,
            Status = 0,
            CategoryId = 3

        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 10,
            Name = "Canon Digital Camera",
            Description = "Canon Digital Camera - High quality digital camera provided by Canon",
            ImageURL = "/Images/Electronic/Electronics5.png",
            Price = 6999,
            Quantity = 15,
            Status = 0,
            CategoryId = 3

        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 11,
            Name = "Nintendo Gameboy",
            Description = "Gameboy - Provided by Nintendo",
            ImageURL = "/Images/Electronic/Electronics6.png",
            Price = 1050,
            Quantity = 60,
            Status = 0,
            CategoryId = 3
        });
        //Furniture Category
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 12,
            Name = "Black Leather Office Chair",
            Description = "Very comfortable black leather office chair",
            ImageURL = "/Images/Furniture/Furniture1.png",
            Price = 550,
            Quantity = 212,
            Status = 0,
            CategoryId = 2
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 13,
            Name = "Pink Leather Office Chair",
            Description = "Very comfortable pink leather office chair",
            ImageURL = "/Images/Furniture/Furniture2.png",
            Price = 500,
            Quantity = 112,
            Status = 0,
            CategoryId = 2
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 14,
            Name = "Lounge Chair",
            Description = "Very comfortable lounge chair",
            ImageURL = "/Images/Furniture/Furniture3.png",
            Price = 700,
            Quantity = 90,
            Status = 0,
            CategoryId = 2
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 15,
            Name = "Silver Lounge Chair",
            Description = "Very comfortable Silver lounge chair",
            ImageURL = "/Images/Furniture/Furniture4.png",
            Price = 1200,
            Quantity = 95,
            Status = 0,
            CategoryId = 2
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 16,
            Name = "Porcelain Table Lamp",
            Description = "White and blue Porcelain Table Lamp",
            ImageURL = "/Images/Furniture/Furniture6.png",
            Price = 150,
            Quantity = 100,
            Status = 0,
            CategoryId = 2
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 17,
            Name = "Office Table Lamp",
            Description = "Office Table Lamp",
            ImageURL = "/Images/Furniture/Furniture7.png",
            Price = 200,
            Quantity = 73,
            Status = 0,
            CategoryId = 2
        });
        //Shoes Category
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 18,
            Name = "Puma Sneakers",
            Description = "Comfortable Puma Sneakers in most sizes",
            ImageURL = "/Images/Shoes/Shoes1.png",
            Price = 1000,
            Quantity = 50,
            Status = 0,
            CategoryId = 4
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 19,
            Name = "Colorful Trainers",
            Description = "Colorful trainsers - available in most sizes",
            ImageURL = "/Images/Shoes/Shoes2.png",
            Price = 1500,
            Quantity = 60,
            Status = 0,
            CategoryId = 4
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 20,
            Name = "Blue Nike Trainers",
            Description = "Blue Nike Trainers - available in most sizes",
            ImageURL = "/Images/Shoes/Shoes3.png",
            Price = 2000,
            Quantity = 70,
            Status = 0,
            CategoryId = 4
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 21,
            Name = "Colorful Hummel Trainers",
            Description = "Colorful Hummel Trainers - available in most sizes",
            ImageURL = "/Images/Shoes/Shoes4.png",
            Price = 1200,
            Quantity = 120,
            Status = 0,
            CategoryId = 4
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 22,
            Name = "Red Nike Trainers",
            Description = "Red Nike Trainers - available in most sizes",
            ImageURL = "/Images/Shoes/Shoes5.png",
            Price = 2000,
            Quantity = 100,
            Status = 0,
            CategoryId = 4
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 23,
            Name = "Birkenstock Sandles",
            Description = "Birkenstock Sandles - available in most sizes",
            ImageURL = "/Images/Shoes/Shoes6.png",
            Price = 500,
            Quantity = 150,
            Status = 0,
            CategoryId = 4
        });


        //Add Product Categories
        modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
        {
            Id = 1,
            Name = "Beauty",
            IconCSS = "fas fa-spa"
        });
        modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
        {
            Id = 2,
            Name = "Furniture",
            IconCSS = "fas fa-couch"
        });
        modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
        {
            Id = 3,
            Name = "Electronics",
            IconCSS = "fas fa-headphones"
        });
        modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
        {
            Id = 4,
            Name = "Shoes",
            IconCSS = "fas fa-shoe-prints"
        });

    }
}
