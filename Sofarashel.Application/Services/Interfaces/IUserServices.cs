
using System;
using System.Collections.Generic;
using System.Text;
using Sofarashel.Domain.ViewModels.User;
using Sofarashel.Enum.User;
using Sofarashel.ViewModels.Users;

namespace Sofarashel.Application.Services.Interfaces
{
    public interface IUserServices
    {
        Task<AdminUserFillterViewModel> AdminFilterAsync(AdminUserFillterViewModel model);

        Task<CreateUserResult> CreateUserInAdminAsync(CreateUserViewModel model);

        Task<EditUserViewModel> GetUserForEditAsync(int userId);



    }
}
