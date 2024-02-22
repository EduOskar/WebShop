using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;
using WebShop.Api.Entity;

namespace WebShop.Api.Services;

public class SupportEmailService
{
    private readonly ApplicationDbContext _dbContext;

    public SupportEmailService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AssignSupportToTicket(int supportMailId, int supportId)
    {
        var userRole = await _dbContext.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == supportId && ur.RoleId == 4);
        if (userRole != null)
        {
            var supportMail = await _dbContext.SupportMails.FindAsync(supportMailId);

            if (supportMail != null && userRole.RoleId == 4)
            {
                supportMail.SupportId = userRole.UserId;

                await _dbContext.SaveChangesAsync();

                return true;
            }
        }

        return false;
    }
}
