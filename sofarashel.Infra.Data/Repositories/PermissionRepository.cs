using Microsoft.EntityFrameworkCore;
using Sofarashel.Data;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Application.Services.Implementation
{
    public class PermissionRepository(GallaryDbcontext context) : IPermissionRepository
    {
        public async Task CreateAsync(Permission permission)
        {
            await context.AddAsync(permission);
        }
        public async Task DeletePermissionAsync(Permission permission)
        {
            permission.IsDelete = true;
            permission.DeleteDate = DateTime.Now;
            context.Update(permission);
        }
        public async Task DeleteAsync(int PermissionId)
        {
            var permission = await GetbyIdAsync(PermissionId);
            await DeletePermissionAsync(permission);
        }

        public async Task<IEnumerable<Permission>> GetAllPermission()
        {
            return await context.Permissions.ToListAsync();
        }

        public async Task<Permission?> GetbyIdAsync(int permissionId)
        {
            return await context.Permissions.FindAsync(permissionId);
        }

        public async Task<Permission?> GetPermissionByIdAsync(int permissionId)
        {
            return await context.Permissions.SingleOrDefaultAsync(p => p.Id == permissionId);
        }

        public async Task<Permission?> GetPermissionByName(string PermissionName)
        {
            return await context.Permissions.Include(p => p.RolePermissionMappings).SingleOrDefaultAsync(p => p.UniqName == PermissionName);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Permission permission)
        {
            context.Update(permission);
        }
    }
}
