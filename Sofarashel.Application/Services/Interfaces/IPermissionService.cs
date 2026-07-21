using Sofarashel.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Application.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<bool> CheckUserPermission(int userId, string permissionName);

        Task<bool> CheckUserPermission(int userId, IEnumerable<string> permissionNames);

        Task<IEnumerable<Permission>> GetAllPermissionAsync();

    }
}
