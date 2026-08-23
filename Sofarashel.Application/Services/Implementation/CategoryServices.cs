using Microsoft.EntityFrameworkCore;
using Sofarashel.Application.Mapper;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Enums.Categories;
using Sofarashel.Domain.Models.Categories;
using Sofarashel.Domain.ViewModels.Categories;
using System;

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

        public async Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentId)
        {
            return await _categoryRepository.GetSubCategoriesAsync(parentId);
        }

        public async Task<IEnumerable<Category>> GetParentCategoryOptionsAsync(int? currentId)
        {
            return await _categoryRepository.GetParentCategoryOptionsAsync(currentId);
        }

        public async Task<AdminEditCategoryViewModel?> GetEditViewModelAsync(int? id)
        {
            var category = await _categoryRepository.GetByIdForAdminAsync(id);

            if (category == null)
            {
                return null;
            }

            var model = CategoryMapper.MapToEditCategoryViewModel(category);
            model.ParentCategories = await _categoryRepository.GetParentCategoryOptionsAsync(category.Id);

            return model;
        }

        public async Task<CreateCategoryResult> CreateCategoryAsync(AdminCreateCategoryViewModel category)
        {
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

            return CreateCategoryResult.Success;
        }

        public async Task<AdminEditCategoryResult> EditCategoryAsync(AdminEditCategoryViewModel category)
        {
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

            return AdminEditCategoryResult.Success;
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            await _categoryRepository.DeleteAsync(categoryId);
            await _categoryRepository.SaveAsync();
        }

        public async Task UpdateMainImageAsync(int categoryId, string imageFileName)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
            {
                return;
            }

            category.MainImage = imageFileName;
            category.UpdateDate = DateTime.Now;

            await _categoryRepository.UpdateCategoryAsync(category);
            await _categoryRepository.SaveAsync();
        }

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
    }
}