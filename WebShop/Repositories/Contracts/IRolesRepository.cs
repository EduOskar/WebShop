using Microsoft.AspNetCore.Identity;
using WebShop.Api.Entity;

namespace WebShop.Api.Repositories.Contracts;

public interface IRolesRepository
{
    Task<IdentityUserRole<int>> GetUserRole(int userId);

    Task<List<IdentityUserRole<int>>> GetUsersAndRoles();

    Task<List<IdentityRole<int>>> GetRoles();

    Task<bool> UpdateUserToRole(UserRole userRole);

    Task<bool> RoleExist(int RoleId);

    Task<bool> Save();
}
