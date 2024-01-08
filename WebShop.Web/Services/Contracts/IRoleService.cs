﻿using Microsoft.AspNetCore.Identity;
using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IRoleService
{
    Task<UserRoleDto> GetUserRole(int userId);

    Task<List<RolesDto>> GetRoles();

    Task<List<UserRoleDto>> GetUsersAndRoles();

    Task<bool> UpdateUserToRole(UserRoleDto userRole);
}
