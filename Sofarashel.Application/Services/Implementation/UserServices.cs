using Sofarashel.Application.Mapper;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Data.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using User_Login.Enum.User;
using User_Login.ViewModels.Users;

namespace Sofarashel.Application.Services.Implementation
{
    public class UserServices(IUserRepository userRepository) : IUserServices
    {
        public async Task<UserFillterViewModel> AdminFilterAsync(UserFillterViewModel model)
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
                        break;
                    }
                case FilterDeleteStatus.All:
                    {
                        query = query.Where(u => u.IsDelete);
                        break;
                    }
                case FilterDeleteStatus.Deleted:
                    {
                        query = query.Where(u => !u.IsDelete);
                        break;
                    }
            }
            #endregion

            #region Sort
            query = query.OrderByDescending(u => u.CreatDate);
            #endregion

       

            return model;
        }
    }
}
