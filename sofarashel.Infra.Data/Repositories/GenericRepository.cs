using Microsoft.EntityFrameworkCore;
using Sofarashel.Data;
using Sofarashel.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sofarashel.Infra.Data.Repositories
{
    public class GenericRepository<T>(GallaryDbcontext context) : IGenericRepository<T> where T : class
    {
        private readonly GallaryDbcontext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> Where)
        {
            return await _dbSet.Where(Where).ToListAsync();
        }

        public async Task<T?> SelectAsync(Expression<Func<T, bool>> Where)
        {
            return await _dbSet.FirstOrDefaultAsync(Where);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}