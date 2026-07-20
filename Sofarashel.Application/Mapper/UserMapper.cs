using Sofarashel.Application.Extensions;
using Sofarashel.Application.Security;
using Sofarashel.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using Sofarashel.ViewModels.User;
using Sofarashel.ViewModels.Users;

namespace Sofarashel.Application.Mapper
{
    public static class UserMapper
    {
        public static IQueryable<UserViewModel> MapToUserViewModel(IQueryable<User> query)
        {
            return query.Select(user => new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.Password,
                CreatDate = user.CreatDate,
                //IsDelete=user.IsDelete,
                IsActive = user.IsActive,


            });
        }

        public static User MapToUser(CreateUserViewModel model)
        {
            return new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName.FixUserName(),
                Password = PasswordHelper.EncodePasswordMd5(model.Password),
                IsActive = true,
                CreatDate = DateTime.Now,
                IsDelete = false
            };
        }
    }
}
