﻿using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.Repositories;

public class RolesRepository : IRolesRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly RoleManager<IdentityRole<int>> _rolemanager;

    public RolesRepository(ApplicationDbContext dbContext, RoleManager<IdentityRole<int>> Rolemanager)
    {
        _dbContext = dbContext;
        _rolemanager = Rolemanager;
    }
    public async Task<IdentityUserRole<int>> GetUserRole(int userId)
    {
        var role = await _dbContext.UserRoles.FirstOrDefaultAsync(r => r.UserId == userId);

        if (role != null)
        {
            return role;
        }

        throw new Exception($"Role {userId} does not exists.");
    }

    public async Task<List<IdentityRole<int>>> GetRoles()
    {
        var roles = await _rolemanager.Roles.ToListAsync();

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
        var roles = _dbContext.UserRoles.ToListAsync();

        if (roles != null)
        {
            return roles;
        }

        throw new Exception();
    }
}