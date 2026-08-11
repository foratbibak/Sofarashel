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

        Task<IEnumerable<Category>> GetProductsByParentAsync(int parentId);

        Task<Category?> GetByIdAsync(int categoryId);

        Task<Category?> GetByIdForAdminAsync(int? categoryId);

        Task<Category?> GetProductWithDetailsAsync(int? categoryId);

        Task<IEnumerable<Category>> GetParentCategoryOptionsAsync(int? currentId);


        Task<IEnumerable<Category>> GetSubCategoriesProductAsync(int parentId);

        Task CreateCategoryAsync(Category category);

        Task UpdateCategoryAsync(Category category);

        Task DeleteAsync(Category category);

        Task DeleteAsync(int categoryId);

        Task SaveAsync();

        Task AddProductDetailAsync(ProductDetail productDetail);

        Task UpdateProductDetailAsync(ProductDetail productDetail);

        Task<ProductDetail?> GetProductDetailByCategoryIdAsync(int categoryId);

        Task AddImageAsync(CategoryImage image);

        Task<CategoryImage?> GetImageByIdAsync(int imageId);

        Task DeleteImageAsync(CategoryImage image);
    }
}