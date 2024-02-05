using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;

namespace WebShop.Api.Services;

public class OrderWorkerService
{
    private readonly ApplicationDbContext _dbContext;

    public OrderWorkerService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AssignOrderToWarehouseWorker(int orderId, int userId)
    {
        var userRole = await _dbContext.UserRoles
             .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == 3);

        if (userRole != null)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);

            if (order != null && userRole.RoleId == 3)
            {
                order.WareHouseWorkerId = userRole.UserId;

                await _dbContext.SaveChangesAsync();

                return true;
            }
        }

        return false;
    }
}
