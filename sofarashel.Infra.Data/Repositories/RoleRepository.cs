using Microsoft.EntityFrameworkCore;
using Sofarashel.Data;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Sofarashel.Infra.Data.Repositories
{
    public class RoleRepository(GallaryDbcontext _context) : IRoleRepository
    {
        public Task AddPermissonToRoleAsync(int roleId, int permissionId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateRoleAsync(Role role)
        {
            await _context.Role.AddAsync(role);
        }

        public Task DeleteAllPermissionInRole(int roleId)
        {
            throw new NotImplementedException();
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

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Role.ToListAsync();
        }

        public async Task<Role?> GetbyIdAsync(int roleId)
        {
            return await _context.Role.FindAsync(roleId);
        }

        public Task<Role> GetRoleByIdForAdminAsync(int? id)
        {
            throw new NotImplementedException();
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
