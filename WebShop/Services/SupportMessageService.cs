using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;

namespace WebShop.Api.Services;

public class SupportMessageService
{
    private readonly ApplicationDbContext _dbContext;

    public SupportMessageService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AssignSupportToTicket(int messageTicketId, int supportId)
    {
        var userRole = await _dbContext.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == supportId && ur.RoleId == 4);
        if (userRole != null)
        {
            var messageTicket = await _dbContext.MessageTickets.FindAsync(messageTicketId);

            if (messageTicket != null && userRole.RoleId == 4)
            {
                messageTicket.SupportId = userRole.UserId;

                await _dbContext.SaveChangesAsync();

                return true;
            }
        }

        return false;
    }
}
