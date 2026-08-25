
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Data.Contract;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Application.Services.Implementation
{
    public class PermissionService(IUserRepository userRepository, IPermissionRepository permissionRepository) : IPermissionService
    {
        public async Task<bool> CheckUserPermission(int userId, string permissionName)
        {
            var user = await userRepository.GetUserFullDataAsync(userId);
            if (user == null) return false;

            var permission = await permissionRepository.GetPermissionByName(permissionName);
            if (permission == null) return false;
            return user.UserInRole.Any(
                s => permission.RolePermissionMappings.Any(p => p.RoleId == s.RoleId)
                );


        }

        public Task<bool> CheckUserPermission(int userId, IEnumerable<string> permissionNames)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionAsync()
        {
            return await permissionRepository.GetAllPermission();
        }

     
    }
}
