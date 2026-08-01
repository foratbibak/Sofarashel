using Microsoft.EntityFrameworkCore;
using Sofarashel.Application.Mapper;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Enums.Categories;
using Sofarashel.Domain.Models.Categories;
using Sofarashel.Domain.ViewModels.Categories;

namespace Sofarashel.Application.Services.Implementation
{
    public class CategoryServices(ICategoryRepository _categoryRepository) : ICategoryServices
    {
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<IEnumerable<Category>> GetRootCategoriesAsync()
        {
            return await _categoryRepository.GetRootCategoriesAsync();
        }

        public async Task<IEnumerable<Category>> GetChildrenAsync(int parentId)
        {
            return await _categoryRepository.GetChildrenAsync(parentId);
        }

        public async Task<Category?> GetCategoryByIdForAdmin(int? id)
        {
            return await _categoryRepository.GetByIdForAdminAsync(id);
        }

        public async Task<CreateCategoryResult> CreateCategoryAsync(AdminCreateCategoryViewModel category)
        {
            #region Validations
            if (string.IsNullOrWhiteSpace(category.Title))
            {
                return CreateCategoryResult.Error;
            }

            if (category.ParentId.HasValue)
            {
                var parent = await _categoryRepository.GetByIdAsync(category.ParentId.Value);
                if (parent == null)
                {
                    return CreateCategoryResult.ParentNotFound;
                }
            }
            #endregion

            #region Create Category
            try
            {
                var addCategory = CategoryMapper.MapToCategory(category);

                await _categoryRepository.CreateCategoryAsync(addCategory);
                await _categoryRepository.SaveAsync();
            }
            catch (DbUpdateException)
            {
                return CreateCategoryResult.DatabaseError;
            }
            catch (Exception)
            {
                return CreateCategoryResult.UnknownError;
            }
            #endregion

            return CreateCategoryResult.Success;
        }

        public async Task<AdminEditCategoryResult> EditCategoryAsync(AdminEditCategoryViewModel category)
        {
            #region Validations
            if (string.IsNullOrWhiteSpace(category.Title))
            {
                return AdminEditCategoryResult.Error;
            }

            if (category.ParentId.HasValue)
            {
                var parent = await _categoryRepository.GetByIdAsync(category.ParentId.Value);
                if (parent == null)
                {
                    return AdminEditCategoryResult.ParentNotFound;
                }

                if (await IsCircularReferenceAsync(category.Id, category.ParentId))
                {
                    return AdminEditCategoryResult.CircularReference;
                }
            }
            #endregion

            #region Edit Category
            try
            {
                var editCategory = await _categoryRepository.GetByIdAsync(category.Id);

                if (editCategory == null)
                {
                    return AdminEditCategoryResult.NotFound;
                }

                CategoryMapper.MapToEditCategory(editCategory, category);

                await _categoryRepository.UpdateCategoryAsync(editCategory);
                await _categoryRepository.SaveAsync();
            }
            catch (DbUpdateException)
            {
                return AdminEditCategoryResult.DatabaseError;
            }
            catch (Exception)
            {
                return AdminEditCategoryResult.UnknownError;
            }
            #endregion

            return AdminEditCategoryResult.Success;
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            await _categoryRepository.DeleteAsync(categoryId);
            await _categoryRepository.SaveAsync();
        }

        #region Helpers
        private async Task<bool> IsCircularReferenceAsync(int categoryId, int? newParentId)
        {
            var currentParentId = newParentId;

            while (currentParentId != null)
            {
                if (currentParentId == categoryId)
                {
                    return true;
                }

                var parent = await _categoryRepository.GetByIdAsync(currentParentId.Value);
                currentParentId = parent?.ParentId;
            }

            return false;
        }


        #endregion

        public async Task<IEnumerable<Category>> GetSelectableParentsAsync(int? currentId)
        {
            return await _categoryRepository.GetSelectableParentsAsync(currentId);
        }

    }
}