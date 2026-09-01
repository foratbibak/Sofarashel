using Sofarashel.Domain.Models.Products;
using System.Collections.Generic;

namespace Sofarashel.Domain.Contracts
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<Product?> GetByIdForAdminAsync(int? productId);
        Task<Product?> GetProductWithDetailsAsync(int? productId);

        Task SetCategoriesAsync(int productId, IEnumerable<int> categoryIds);
    }
}