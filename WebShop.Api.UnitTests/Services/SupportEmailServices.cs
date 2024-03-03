using Microsoft.EntityFrameworkCore;
using Moq;
using WebShop.Api.Data;
using WebShop.Api.Services;
using Xunit.Sdk;

namespace WebShop.Api.UnitTests.Services;

public class SupportEmailServices
{
    [Fact]
    public async void AssignSupportToTicket_WithAEmptyDatabase_ReturnFalse()
    {
        //Arrange 
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;

        var context = new ApplicationDbContext(options);

        var myService = new SupportEmailService(context);


        //Act
        var result = await myService.AssignSupportToTicket(1, 2);

        //Assert
        Assert.False(result);
        Assert.Contains(context.SupportMails.);

    }

}