using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Repositories;

public class UserRolesRepository : IUserRolesRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly RoleManager<IdentityRole<int>> _rolemanager;

    public UserRolesRepository(ApplicationDbContext dbContext, RoleManager<IdentityRole<int>> Rolemanager)
    {
        _dbContext = dbContext;
        _rolemanager = Rolemanager;
    }
    public async Task<IdentityUserRole<int>> GetUserRole(int userId)
    {
        var role = await _dbContext.UserRoles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.UserId == userId);

        if (role != null)
        {
            return role;
        }

        throw new Exception($"Role {userId} does not exists.");
    }

    public async Task<IdentityRole<int>> GetRole(int roleId)
    {
        var role = await _dbContext.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == roleId);

        if (role != null)
        {
            return role;
        }

        throw new Exception($"Role {roleId} does not exists.");
    }

    public async Task<List<IdentityRole<int>>> GetRoles()
    {
        var roles = await _rolemanager.Roles
            .AsNoTracking()
            .ToListAsync();

        if (roles != null)
        {
            return roles;
        }

        throw new Exception($"Error fetching roles");
    }

    public async Task<bool> UpdateUserToRole(UserRole userRole)
    {
        _dbContext.UserRoles.Update(userRole);

        return await Save();
    }

    public async Task<bool> RoleExist(int RoleId)
    {
        var roleExist = await _dbContext.Roles.AnyAsync(u => u.Id == RoleId);

        return roleExist;
    }

    public async Task<bool> Save()
    {
        var save = await _dbContext.SaveChangesAsync();

        return save > 0;
    }

    public Task<List<IdentityUserRole<int>>> GetUsersAndRoles()
    {
        var roles = _dbContext.UserRoles
            .AsNoTracking()
            .ToListAsync();

        if (roles != null)
        {
            return roles;
        }

        throw new Exception();
    }

    public async Task<bool> DeleteUserRoles(int UserId)
    {
        var deleteRoleUser = await _dbContext.UserRoles.Where(ur => ur.UserId == UserId).FirstOrDefaultAsync();

        if (deleteRoleUser == null)
        {
            return false;
        }

        _dbContext.UserRoles.Remove(deleteRoleUser);

        await _dbContext.SaveChangesAsync();

        return true;
    }   

    public async Task<IdentityResult> CreateRole(Role role)
    {
        var createRole = await _rolemanager.CreateAsync(role);

        if (createRole != null)
        {
            return createRole;
        }

        throw new Exception();
    }
}
