
using System;
using System.Collections.Generic;
using System.Text;
using User_Login.ViewModels.Users;

namespace Sofarashel.Application.Services.Interfaces
{
    public interface IUserServices
    {
        Task<UserFillterViewModel> AdminFilterAsync(UserFillterViewModel model);

    }
}
