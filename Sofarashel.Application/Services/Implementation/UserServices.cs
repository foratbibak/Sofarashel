using Microsoft.EntityFrameworkCore;
using Sofarashel.Application.Extensions;
using Sofarashel.Application.Mapper;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Data.Contract;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Enums.User;
using Sofarashel.Domain.ViewModels.User;
using Sofarashel.Enum.User;
using Sofarashel.Models.User;
using Sofarashel.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Application.Services.Implementation
{
    public class UserServices(IUserRepository userRepository,IRoleRepository roleRepository) : IUserServices
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

        public async Task<AdminEditUserResult> EditUserAsync(EditUserViewModel model)
        {
            #region Validations
            try
            {
                if (string.IsNullOrEmpty(model.UserName))
                {
                    return AdminEditUserResult.Error;

                }
                if (await userRepository.IsExsitUserNameForEditAsync(model.UserName,model.Id))
                {
                    return AdminEditUserResult.UserNameDuplicated;
                }
            }
            catch (DbUpdateException)
            {
                return AdminEditUserResult.DatabaseError;
            }
            catch (Exception)
            {

                return AdminEditUserResult.UnknownError;
            }
            #endregion

            #region Edit User
            var user = await userRepository.GetUserFullDataAsync(model.Id);

            if (user == null)
            {
                return AdminEditUserResult.Error;
            }

            UserMapper.MapToEditUser(user, model);

            user.UpdateDate = DateTime.Now;

            await userRepository.UpdateAsync(user);
            await userRepository.SaveAsync();
            #endregion

            #region Edit Role
            await roleRepository.UpdateUserInRole(user.Id, model.UserSelectedRoles);
            #endregion
            return AdminEditUserResult.Success;

        }

        public async Task<EditUserViewModel> GetUserForEditAsync(int userId)
        {
            var user = await userRepository.GetUserFullDataAsync(userId);
            if (user == null)
            {
                throw new Exception("کاربر یافت نشد");
            }
            var edituser = UserMapper.MapToEditUser(user);
            edituser.Roles = await roleRepository.GetAllRolesAsync();
            return edituser;
        }
    }
}
