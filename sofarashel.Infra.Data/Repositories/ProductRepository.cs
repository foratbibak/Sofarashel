using Microsoft.EntityFrameworkCore;
using Sofarashel.Data;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Models.Products;
using Sofarashel.Domain.Models.Relations;
using System.Collections.Generic;

namespace Sofarashel.Infra.Data.Repositories
{
    public class ProductRepository(GallaryDbcontext _context)
        : GenericRepository<Product>(_context), IProductRepository
    {
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.ProductCategories!.Any(pc => pc.CategoryId == categoryId))
                .Include(p => p.ProductImages!)
                    .ThenInclude(pi => pi.Image)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdForAdminAsync(int? productId)
        {
            return await _context.Products
                .Include(p => p.ProductImages!)
                    .ThenInclude(pi => pi.Image)
                .Include(p => p.ProductAttributes!)
                    .ThenInclude(pa => pa.AttributeFeature)
                .Include(p => p.ProductCategories!)
                    .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<Product?> GetProductWithDetailsAsync(int? productId)
        {
            return await _context.Products
                .Include(p => p.ProductImages!)
                    .ThenInclude(pi => pi.Image)
                .Include(p => p.ProductAttributes!)
                    .ThenInclude(pa => pa.AttributeFeature)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        #region Category 
        public async Task SetCategoriesAsync(int productId, IEnumerable<int> categoryIds)
        {
            var existingLinks = await _context.Rel_Product_Category
                .Where(rpc => rpc.ProductId == productId)
                .ToListAsync();

            _context.Rel_Product_Category.RemoveRange(existingLinks);

            var newLinks = categoryIds
                .Distinct()
                .Select(categoryId => new Rel_Product_Category
                {
                    ProductId = productId,
                    CategoryId = categoryId
                });

            await _context.Rel_Product_Category.AddRangeAsync(newLinks);
        }
        #endregion
    }
}