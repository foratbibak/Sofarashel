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

        public async Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentId)
        {
            return await _categoryRepository.GetSubCategoriesAsync(parentId);
        }

        public async Task<IEnumerable<Category>> GetProductsByParentAsync(int parentId)
        {
            return await _categoryRepository.GetProductsByParentAsync(parentId);
        }

        public async Task<Category?> GetSingleProductAsync(int? id)
        {
            return await _categoryRepository.GetProductWithDetailsAsync(id);
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

        public async Task<CategoryImage?> GetImageByIdAsync(int imageId)
        {
            return await _categoryRepository.GetImageByIdAsync(imageId);
        }

        #region Images
        public async Task AddImageAsync(int categoryId, string imageFileName)
        {
            var image = new CategoryImage
            {
                CategoryId = categoryId,
                ImageUrl = imageFileName,
                IsMain = false,
                CreatDate = DateTime.Now,
                IsDelete = false
            };

            await _categoryRepository.AddImageAsync(image);
            await _categoryRepository.SaveAsync();
        }

        public async Task DeleteImageAsync(int imageId)
        {
            var image = await _categoryRepository.GetImageByIdAsync(imageId);

            if (image != null)
            {
                await _categoryRepository.DeleteImageAsync(image);
                await _categoryRepository.SaveAsync();
            }
        }
        #endregion

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

            #region Create Category  ProductDetail
            try
            {
                var addCategory = CategoryMapper.MapToCategory(category);

                await _categoryRepository.CreateCategoryAsync(addCategory);
                await _categoryRepository.SaveAsync();

                var productDetail = CategoryMapper.MapToProductDetail(category, addCategory.Id);

                await _categoryRepository.AddProductDetailAsync(productDetail);
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

            #region Edit Category ProductDetail
            try
            {
                var editCategory = await _categoryRepository.GetByIdAsync(category.Id);

                if (editCategory == null)
                {
                    return AdminEditCategoryResult.NotFound;
                }

                CategoryMapper.MapToEditCategory(editCategory, category);
                await _categoryRepository.UpdateCategoryAsync(editCategory);

                var productDetail = await _categoryRepository.GetProductDetailByCategoryIdAsync(category.Id);

                if (productDetail == null)
                {
                    productDetail = CategoryMapper.MapToProductDetailFromEdit(category, category.Id);
                    await _categoryRepository.AddProductDetailAsync(productDetail);
                }
                else
                {
                    CategoryMapper.MapToEditProductDetail(productDetail, category);
                    await _categoryRepository.UpdateProductDetailAsync(productDetail);
                }

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




    }
}