using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.UnitTests.Services;
public class ProductsTests
{
    [Fact]
    public async void CreateProduct_ShouldCreateProduct_ReturnsTrue()
    {
        //Arrange

        Product productCreateTest = new Product
        {
            Id = 1,
            Name = "Test",
            Description = "Test description",
            CategoryId = 1,
            Price = 1,
            Quantity = 1,
            ImageURL = "TestImage",
            Status = ProductStatus.Inactive
        };    

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var context = new ApplicationDbContext(options);

        var myService = new ProductRepository(context);
        //Act

        var result = await myService.CreateProduct(productCreateTest);

        //Assert

        Assert.True(result);
    }

    [Theory]
    [InlineData(1)]
    public async Task GetProduct_ShouldGetProduct_ReturnsProduct(int id)
    {
        //Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var context = new ApplicationDbContext(options);

        var myService = new ProductRepository (context);

        //Act

        var result = await myService.GetProduct(id);

        //Assert

        Assert.Null(result);
    }
}
