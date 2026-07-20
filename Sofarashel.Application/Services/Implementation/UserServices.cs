using Microsoft.EntityFrameworkCore;
using Sofarashel.Application.Mapper;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Data.Contract;
using Sofarashel.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using Sofarashel.Enum.User;
using Sofarashel.ViewModels.Users;
using Sofarashel.Domain.ViewModels.User;

namespace Sofarashel.Application.Services.Implementation
{
    public class UserServices(IUserRepository userRepository) : IUserServices
    {
        public async Task<AdminUserFillterViewModel> AdminFilterAsync(AdminUserFillterViewModel model)
        {
            #region Query
            var query = await userRepository.FilterAsync();
            #endregion

            #region Filter
            if (!string.IsNullOrEmpty(model.FirstName))
            {
                query = query.Where(u => u.FirstName.Contains(model.FirstName));

            }
            if (!string.IsNullOrEmpty(model.LastName))
            {
                query = query.Where(u => u.LastName.Contains(model.LastName));

            }
            if (!string.IsNullOrEmpty(model.UserName))
            {
                query = query.Where(u => u.UserName.Contains(model.UserName));

            }

         
            switch (model.DeleteStatus)
            {
                case FilterDeleteStatus.NotDeleted:
                    {
                        query = query.Where(u => !u.IsDelete);
                        break;
                    }
                case FilterDeleteStatus.All:
                    {
                        query = query.Where(u => u.IsDelete);
                        break;
                    }
                case FilterDeleteStatus.Deleted:
                    {
                        break;
                    }
            }
            #endregion

            #region Sort
            query = query.OrderByDescending(u => u.CreatDate);
            #endregion

            model.Users = await UserMapper
            .MapToUserViewModel(query).ToListAsync();

            return model;
        }

        public async Task<CreateUserResult> CreateUserInAdminAsync(CreateUserViewModel model)
        {
            #region Validations
            try
            {
                if (
                string.IsNullOrEmpty(model.UserName) &&
                string.IsNullOrEmpty(model.Password))
                {
                    return CreateUserResult.Error;

                }
                if (await userRepository.IsExsitUserNameAsync(model.UserName))
                {
                    return CreateUserResult.UserNameDuplicated;
                }
            }
            catch (DbUpdateException)
            {
                return CreateUserResult.DatabaseError;
            }
            catch (Exception)
            {

                return CreateUserResult.UnknownError;
            }
            #endregion

            #region CreateUser
            User user=UserMapper.MapToUser(model);

            await userRepository.CreateAsync(user);
            await userRepository.SaveAsync();
            if (model.UserSelectedRoles != null && model.UserSelectedRoles.Any())
            {
                await userRepository.AddUserToRole(user.Id, model.UserSelectedRoles);
                await userRepository.SaveAsync();
            }
            #endregion
            return CreateUserResult.Success;

        }

        public Task<EditUserViewModel> GetUserForEditAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
