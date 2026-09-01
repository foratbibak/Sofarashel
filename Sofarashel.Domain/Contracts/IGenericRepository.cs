using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sofarashel.Domain.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();


        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> Where);

        Task<T?> SelectAsync(Expression<Func<T, bool>> Where);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);


        void Update(T entity);


        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);


        Task SaveAsync();
    }
}