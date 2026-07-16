using Sofarashel.Domain.Enums.Account;
using Sofarashel.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Text;
using User_Login.Models.User;

namespace Sofarashel.Application.Services.Interfaces
{
    public interface IAccountServices
    {
        Task<LoginUserResult> LoginUserAsync(LoginViewModel model);
        
        Task<User?> GetUserByUserNameAsync(string userName);

    }
}
