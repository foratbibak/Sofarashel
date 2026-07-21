
using Sofarashel.Domain.Models.Permission;
using Sofarashel.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Domain.Contracts
{
    public interface IRoleRepository
    {


        Task<IEnumerable<Role>> GetAllRolesAsync();

        Task<Role> GetRoleByIdForAdminAsync(int? id);

        Task DeleteAllPermissionInRole(int roleId);

        Task<IEnumerable<RolePermissionMapping>>GetAllPermissionInRole(int roleId);

        Task<Role?>GetbyIdAsync(int roleId);
        Task CreateRoleAsync(Role role);
        Task UpdateRoleAsync(Role role);
        Task DeleteAsync(Role role);
        Task DeleteAsync(int roleId);

        Task<IEnumerable<Permission>> GetAllPermissionsAsync();

        Task AddPermissonToRoleAsync(int roleId, int permissionId);

        Task SaveAsync();

        Task UpdateUserInRole(int  userId, List<int> selectedroles);
    }
}
