using Sofarashel.Application.Security;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Data.Contract;
using Sofarashel.Domain.Enums.Account;
using Sofarashel.Models.User;
using Sofarashel.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Application.Services.Implementation
{
    public class AccountServices(IUserRepository _userRepository) : IAccountServices
    {
        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            return await _userRepository.GetUserByUserName(userName);  
        }

        public async Task<LoginUserResult> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userRepository.GetUserByUserName(model.UserName);
            if (user == null)
                return LoginUserResult.NotFound;

            if (!PasswordHelper.VerifyPassword(model.Password, user.Password))
                return LoginUserResult.NotFound;

            return LoginUserResult.Success;
        }
    }
}
