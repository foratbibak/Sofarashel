using Microsoft.EntityFrameworkCore;
using Sofarashel.Data.Contract;
using Sofarashel.Models.User;


namespace Sofarashel.Data.Implementation
{
    public class UserRepository(GallaryDbcontext context) : IUserRepository
    {
        public async Task AddUserToRole(int UserId, List<int> roleIds)
        {
            foreach (int roleId in roleIds)
            {
                context.UserInRoles.Add(new Domain.Models.Roles.UserInRoles()
                {
                    RoleId = roleId,
                    UserId = UserId,
                });
            }
        }

        public async Task CreateAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

        public async Task DeleteAsync(User user)
        {
            user.IsDelete = true;
            user.IsActive = false;
            user.DeleteDate = DateTime.Now;
            await UpdateAsync(user);
        }

        public async Task DeleteAsync(int UserId)
        {
            var user =await GetUserbyIdAsync(UserId);
            if (user != null)
            {
                await DeleteAsync(user);
            }
        }

        public async Task<IQueryable<User>> FilterAsync()
        {
            return await Task.FromResult(context.Users.AsQueryable());

        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return  context.Users.ToList();
        }

        public async Task<User?> GetUserbyIdAsync(int userId)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.Id == userId);

        }

        public async Task<User?> GetUserByUserName(string userName)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.UserName == userName);  
        }

        public async Task<User?> GetUserFullDataAsync(int userId)
        {
            return await context.Users.IgnoreQueryFilters().Include(u => u.UserInRole)
               .ThenInclude(u => u.Role).SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> IsExsitUserNameAsync(string userName)
        {
            return await context.Users.AnyAsync(u => u.UserName == userName);
        }

        public async Task<bool> IsExsitUserNameForEditAsync(string userName, int userId)
        {
            return await context.Users.AnyAsync(u => u.UserName == userName && u.Id != userId);
        }

        public async Task ReturnUserDeAcitve(User user)
        {
            user.IsDelete = false;
            user.IsActive = true;
            user.UpdateDate = DateTime.Now;
            await UpdateAsync(user);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            context.Users.Update(user);
        }

        public async Task UserDeAcitve(int userId)
        {
            var user = await GetUserbyIdAsync(userId);
            if (user != null)
            {
                await ReturnUserDeAcitve(user);
            }
        }
    }
}
