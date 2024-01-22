using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IUsersAndRoleService
{
    Task<UserRoleDto> GetUserRole(int userId);

    Task<List<RolesDto>> GetRoles();

    Task<RolesDto> GetRole(int roleId);

    Task<List<UserRoleDto>> GetUsersAndRoles();

    Task<bool> UpdateUserToRole(UserRoleDto userRole);

    Task<bool> DeleteUserRoles(int userId);
}
