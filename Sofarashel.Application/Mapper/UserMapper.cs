using Sofarashel.Application.Extensions;
using Sofarashel.Application.Security;
using Sofarashel.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using Sofarashel.ViewModels.User;
using Sofarashel.ViewModels.Users;
using Sofarashel.Domain.ViewModels.User;

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

        public static void MapToEditUser(User user, EditUserViewModel model)
        {
            user.FirstName = model.FirstName?.Trim();
            user.UserName = model.UserName.FixUserName();
            user.LastName = model.LastName?.Trim();
            user.IsActive = model.IsActive;
            user.IsDelete = model.IsDelete;
            user.UpdateDate = DateTime.Now;
        }

        public static EditUserViewModel MapToEditUser(User user)
        {
            return new EditUserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                IsActive = user.IsActive,
                IsDelete = user.IsDelete,
                UserSelectedRoles = user.UserInRole?.Select(r => r.RoleId).ToList(),

            };

        }
    }
}
