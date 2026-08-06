using Microsoft.EntityFrameworkCore;
using Sofarashel.Data;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Models.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Infra.Data.Repositories
{
    public class CategoryRepository(GallaryDbcontext _context) : ICategoryRepository
    {
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetRootCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => c.ParentId == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentId)
        {
            return await _context.Categories
                .Where(c => c.ParentId == parentId && c.IsCategory==false)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        public async Task<Category?> GetByIdForAdminAsync(int? categoryId)
        {
            var category = await _context.Categories
                .Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category?.Images != null)
            {
                foreach (var image in category.Images)
                {
                    image.Category = null;
                }
            }

            return category;
        }

        public async Task CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
        }

        public async Task DeleteAsync(Category category)
        {
            category.IsDelete = true;
            category.DeleteDate = DateTime.Now;
            _context.Categories.Update(category);
        }

        public async Task DeleteAsync(int categoryId)
        {
            var category = await GetByIdAsync(categoryId);
            await DeleteAsync(category);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetSelectableParentsAsync(int? currentId)
        {
            var query = _context.Categories.Where(c => c.IsCategory);

            if (currentId.HasValue)
            {
                query = query.Where(c => c.Id != currentId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetSubCategoriesProductAsync(int parentId)
        {
            return await _context.Categories
                 .Where(c => c.ParentId == parentId && c.IsCategory == true)
                 .ToListAsync();
        }
    }
}
