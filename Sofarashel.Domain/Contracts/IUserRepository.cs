

using Sofarashel.Models.User;

namespace Sofarashel.Data.Contract
{
    public interface IUserRepository
    {

        Task<IQueryable<User>> FilterAsync();

        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User?> GetUserbyIdAsync(int userId);

        Task<User?>GetUserByUserName(string userName);

        Task CreateAsync(User user);

        Task UpdateAsync(User user);

        Task DeleteAsync(User user);

        Task DeleteAsync(int UserId);

        Task ReturnUserDeAcitve(User user);

        Task UserDeAcitve(int userId);

        Task<bool> IsExsitUserNameAsync(string userName);

        Task SaveAsync();
    }
}
