using Microsoft.EntityFrameworkCore;
using Moq;
using WebShop.Api.Data;
using WebShop.Api.Services;
using Xunit.Sdk;

namespace WebShop.Api.UnitTests.Services;

public class SupportEmailServices
{
    [Fact]
    public async Task AssignSupportToTicket_WithAEmptyDatabase_ReturnFalse()
    {
        //Arrange - Go get your variables = Classes, functions
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;

        var context = new ApplicationDbContext(options);

        var myService = new SupportEmailService(context);


        //Act - Execute this function
        var result = await myService.AssignSupportToTicket(1, 2);

        //Assert - Whatever is returned is what you want
        Assert.False(result);
        //Assert.Contains(context.SupportMails);

    }

}