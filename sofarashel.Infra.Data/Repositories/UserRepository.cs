using Microsoft.EntityFrameworkCore;
using Sofarashel.Data.Contract;
using Sofarashel.Models.User;


namespace Sofarashel.Data.Implementation
{
    public class UserRepository(DBConnection context) : IUserRepository
    {
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

        public Task<User?> GetUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsExsitUserNameAsync(string userName)
        {
            return await context.Users.AnyAsync(u => u.UserName == userName);
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
