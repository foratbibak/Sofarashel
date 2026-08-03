using Sofarashel.Domain.Models.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Domain.Contracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<IEnumerable<Category>> GetRootCategoriesAsync();

        Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentId);

        Task<Category?> GetByIdAsync(int categoryId);

        Task<Category?> GetByIdForAdminAsync(int? categoryId);

        Task<IEnumerable<Category>> GetSelectableParentsAsync(int? currentId);

        Task CreateCategoryAsync(Category category);

        Task UpdateCategoryAsync(Category category);

        Task DeleteAsync(Category category);

        Task DeleteAsync(int categoryId);

        Task SaveAsync();
    }
}
