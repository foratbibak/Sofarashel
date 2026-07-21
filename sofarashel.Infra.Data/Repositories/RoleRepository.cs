using Microsoft.EntityFrameworkCore;
using Sofarashel.Data;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Models.Permission;
using Sofarashel.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Sofarashel.Infra.Data.Repositories
{
    public class RoleRepository(GallaryDbcontext _context) : IRoleRepository
    {
        public async Task AddPermissonToRoleAsync(int roleId, int permissionId)
        {
            await _context.RolePermissionMappings.AddAsync(new RolePermissionMapping()
            {
                RoleId = roleId,
                PermissionId = permissionId

            });
        }

        public async Task CreateRoleAsync(Role role)
        {
            await _context.Role.AddAsync(role);
        }

        public async Task DeleteAllPermissionInRole(int roleId)
        {
            var permissions = await GetAllPermissionInRole(roleId);
            _context.RolePermissionMappings.RemoveRange(permissions);
        }

        public async Task DeleteAsync(Role role)
        {
            role.IsDelete = true;
            role.DeleteDate = DateTime.Now;
            _context.Role.Update(role);
        }

        public async Task DeleteAsync(int roleId)
        {
            var role = await GetbyIdAsync(roleId);
            await DeleteAsync(role);
        }

        public async Task<IEnumerable<RolePermissionMapping>> GetAllPermissionInRole(int roleId)
        {
            return await _context.RolePermissionMappings.Where(r => r.RoleId == roleId).ToListAsync();

        }

        public Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task<Role?> GetbyIdAsync(int roleId)
        {
            return await _context.Role.FindAsync(roleId);
        }

        public async Task<Role> GetRoleByIdForAdminAsync(int? id)
        {
            return await _context.Role.Include(r => r.RolePermissionMappings)
                           .Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(Role role)
        {
            _context.Role.Update(role);
        }

        public async Task UpdateUserInRole(int userId, List<int> selectedroles)
        {
            var rolesUser = _context.UserInRoles.Where(r => r.UserId == userId).ToList();
            foreach (var role in rolesUser)
            {
                _context.Remove(role);
            }
            if (selectedroles != null && selectedroles.Count > 0)
            {
                foreach (int role in selectedroles)
                {
                    _context.UserInRoles.Add(new UserInRoles
                    {
                        UserId = userId,
                        RoleId = role,
                    });
                }
            }
            _context.SaveChanges();
        }
    }
}
