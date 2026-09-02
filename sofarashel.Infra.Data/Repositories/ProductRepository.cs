using Microsoft.EntityFrameworkCore;
using Sofarashel.Data;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Models.Products;
using Sofarashel.Domain.Models.Relations;
using System.Collections.Generic;

namespace Sofarashel.Infra.Data.Repositories
{
    public class ProductRepository(GallaryDbcontext _context) : IProductRepository
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

        #region Image 
        public async Task LinkImageAsync(int productId, int imageId, bool isMain, int displayOrder)
        {
            if (isMain)
            {
                var currentMain = await _context.Rel_Image_Product
                    .Where(rip => rip.ProductId == productId && rip.IsMain)
                    .ToListAsync();

                foreach (var link in currentMain)
                {
                    link.IsMain = false;
                }
            }

            var existingLink = await _context.Rel_Image_Product
                .FirstOrDefaultAsync(rip => rip.ProductId == productId && rip.ImageId == imageId);

            if (existingLink != null)
            {
                existingLink.IsMain = isMain;
                existingLink.DisplayOrder = displayOrder;
                return;
            }

            await _context.Rel_Image_Product.AddAsync(new Rel_Image_Product
            {
                ProductId = productId,
                ImageId = imageId,
                IsMain = isMain,
                DisplayOrder = displayOrder
            });
        }

        public async Task UnlinkImageAsync(int productId, int imageId)
        {
            var link = await _context.Rel_Image_Product
                .FirstOrDefaultAsync(rip => rip.ProductId == productId && rip.ImageId == imageId);

            if (link != null)
            {
                _context.Rel_Image_Product.Remove(link);
            }
        }
        #endregion

        #region Attribute 
        public async Task ReplaceAttributesAsync(int productId, IEnumerable<int> attributeFeatureIds)
        {
            var existingLinks = await _context.Rel_AttributesFetures_Product
                .Where(ra => ra.ProductId == productId)
                .ToListAsync();

            _context.Rel_AttributesFetures_Product.RemoveRange(existingLinks);

            var newLinks = attributeFeatureIds
                .Distinct()
                .Select((attributeFeatureId, index) => new Rel_AttributesFetures_Product
                {
                    ProductId = productId,
                    AttributeFeatureId = attributeFeatureId,
                    DisplayOrder = index
                });

            await _context.Rel_AttributesFetures_Product.AddRangeAsync(newLinks);
        }
        #endregion
    }
}