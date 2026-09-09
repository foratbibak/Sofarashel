using Sofarashel.Domain.Models.Products;
using System.Collections.Generic;

namespace Sofarashel.Domain.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<Product?> GetByIdForAdminAsync(int? productId);

        Task<Product?> GetProductWithDetailsAsync(int? productId);

        Task SetCategoriesAsync(int productId, IEnumerable<int> categoryIds);

        Task LinkImageAsync(int productId, int imageId, bool isMain, int displayOrder);

        Task UnlinkImageAsync(int productId, int imageId);

        Task<int> GetNextImageDisplayOrderAsync(int productId);

        Task RemoveAllLinksForImageAsync(int imageId);

        Task ReplaceAttributesAsync(int productId, IEnumerable<int> attributeFeatureIds);

        Task RemoveAllLinksForAttributeAsync(int attributeFeatureId);
    }
}