
using Sofarashel.Domain.Models.Roles;
using Sofarashel.Domain.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Services.Interfaces
{
    public interface IRoleServices
    {
        Task<IEnumerable<Role>> GetAllRoleAsync();
        Task<Role> GetRoleByIdForAdmin(int? id);

        Task CreateRole(AdminCreateRoleViewModel role);

        Task EditRoleAsync(AdminEditRoleViewModel role);

        Task DeleteRoleAsync(int roleId);

    }
}
