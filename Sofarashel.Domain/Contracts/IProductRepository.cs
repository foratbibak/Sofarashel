using Sofarashel.Domain.Models.Products;
using Sofarashel.Domain.Models.Relations;
using System.Collections.Generic;

namespace Sofarashel.Domain.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<Product?> GetByIdAsync(int productId);
        Task<Product?> GetByIdForAdminAsync(int? productId);
        Task<Product?> GetProductWithDetailsAsync(int? productId);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteAsync(Product product);
        Task DeleteAsync(int productId);
        Task SaveAsync();

        #region Images
        Task AddImageAsync(ProductImage image);
        Task<ProductImage?> GetImageByIdAsync(int imageId);
        Task DeleteImageAsync(ProductImage image);
        #endregion

        #region Category 
        Task SetCategoriesAsync(int productId, IEnumerable<int> categoryIds);
        #endregion
    }
}