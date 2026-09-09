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
                .Where(product => product.ProductCategories!.Any(link => link.CategoryId == categoryId))
                .Include(product => product.ProductImages!)
                    .ThenInclude(link => link.Image)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdForAdminAsync(int? productId)
        {
            return await _context.Products
                .Include(product => product.ProductImages!)
                    .ThenInclude(link => link.Image)
                .Include(product => product.ProductAttributes!)
                    .ThenInclude(link => link.AttributeFeature)
                .Include(product => product.ProductCategories!)
                    .ThenInclude(link => link.Category)
                .FirstOrDefaultAsync(product => product.Id == productId);
        }

        public async Task<Product?> GetProductWithDetailsAsync(int? productId)
        {
            return await _context.Products
                .Include(product => product.ProductImages!)
                    .ThenInclude(link => link.Image)
                .Include(product => product.ProductAttributes!)
                    .ThenInclude(link => link.AttributeFeature)
                .FirstOrDefaultAsync(product => product.Id == productId);
        }

        #region Category 
        public async Task SetCategoriesAsync(int productId, IEnumerable<int> categoryIds)
        {
            var existingLinks = await _context.Rel_Product_Category
                .Where(link => link.ProductId == productId)
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
                var currentMainLinks = await _context.Rel_Image_Product
                    .Where(link => link.ProductId == productId && link.IsMain)
                    .ToListAsync();

                foreach (var link in currentMainLinks)
                {
                    link.IsMain = false;
                }
            }

            var existingLink = await _context.Rel_Image_Product
                .FirstOrDefaultAsync(link => link.ProductId == productId && link.ImageId == imageId);

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
                .FirstOrDefaultAsync(link => link.ProductId == productId && link.ImageId == imageId);

            if (link != null)
            {
                _context.Rel_Image_Product.Remove(link);
            }
        }

        public async Task<int> GetNextImageDisplayOrderAsync(int productId)
        {
            var hasAny = await _context.Rel_Image_Product
                .AnyAsync(link => link.ProductId == productId);

            if (!hasAny)
            {
                return 0;
            }

            var maxOrder = await _context.Rel_Image_Product
                .Where(link => link.ProductId == productId)
                .MaxAsync(link => link.DisplayOrder);

            return maxOrder + 1;
        }

        public async Task RemoveAllLinksForImageAsync(int imageId)
        {
            var links = await _context.Rel_Image_Product
                .Where(link => link.ImageId == imageId)
                .ToListAsync();

            _context.Rel_Image_Product.RemoveRange(links);
        }
        #endregion

        #region Attribute 
        public async Task ReplaceAttributesAsync(int productId, IEnumerable<int> attributeFeatureIds)
        {
            var existingLinks = await _context.Rel_AttributesFetures_Product
                .Where(link => link.ProductId == productId)
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
        public async Task RemoveAllLinksForAttributeAsync(int attributeFeatureId)
        {
            var links = await _context.Rel_AttributesFetures_Product
                .Where(link => link.AttributeFeatureId == attributeFeatureId)
                .ToListAsync();

            _context.Rel_AttributesFetures_Product.RemoveRange(links);
        }
        #endregion
    }
}