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
                .Where(p => p.ProductCategories!.Any(c => c.CategoryId == categoryId))
                .Include(p => p.ProductImages!)
                    .ThenInclude(i => i.Image)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdForAdminAsync(int? productId)
        {
            return await _context.Products
                .Include(p => p.ProductImages!)
                    .ThenInclude(i => i.Image)
                .Include(p => p.ProductAttributes!)
                    .ThenInclude(a => a.AttributeFeature)
                .Include(p => p.ProductCategories!)
                    .ThenInclude(c => c.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<Product?> GetProductWithDetailsAsync(int? productId)
        {
            return await _context.Products
                .Include(p => p.ProductImages!)
                    .ThenInclude(i => i.Image)
                .Include(p => p.ProductAttributes!)
                    .ThenInclude(a => a.AttributeFeature)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IQueryable<Product>> FilterAsync()
        {
            return await Task.FromResult(_context.Products
                .Include(p => p.ProductImages!)
                    .ThenInclude(i => i.Image)
                .AsQueryable());
        }

        #region Category 
        public async Task SetCategoriesAsync(int productId, IEnumerable<int> categoryIds)
        {
            var existingCategoryLinks = await _context.Rel_Product_Category
                .Where(c => c.ProductId == productId)
                .ToListAsync();

            _context.Rel_Product_Category.RemoveRange(existingCategoryLinks);

            var newCategoryLinks = categoryIds
                .Distinct()
                .Select(categoryId => new Rel_Product_Category
                {
                    ProductId = productId,
                    CategoryId = categoryId
                });

            await _context.Rel_Product_Category.AddRangeAsync(newCategoryLinks);
        }
        #endregion

        #region Image 
        public async Task LinkImageAsync(int productId, int imageId, bool isMain, int displayOrder)
        {
            if (isMain)
            {
                var currentMainImages = await _context.Rel_Image_Product
                    .Where(i => i.ProductId == productId && i.IsMain)
                    .ToListAsync();

                foreach (var image in currentMainImages)
                {
                    image.IsMain = false;
                }
            }

            var existingImage = await _context.Rel_Image_Product
                .FirstOrDefaultAsync(i => i.ProductId == productId && i.ImageId == imageId);

            if (existingImage != null)
            {
                existingImage.IsMain = isMain;
                existingImage.DisplayOrder = displayOrder;
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
            var image = await _context.Rel_Image_Product
                .FirstOrDefaultAsync(i => i.ProductId == productId && i.ImageId == imageId);

            if (image != null)
            {
                _context.Rel_Image_Product.Remove(image);
            }
        }

        public async Task<int> GetNextImageDisplayOrderAsync(int productId)
        {
            var hasAny = await _context.Rel_Image_Product
                .AnyAsync(i => i.ProductId == productId);

            if (!hasAny)
            {
                return 0;
            }

            var maxOrder = await _context.Rel_Image_Product
                .Where(i => i.ProductId == productId)
                .MaxAsync(i => i.DisplayOrder);

            return maxOrder + 1;
        }

        public async Task RemoveAllLinksForImageAsync(int imageId)
        {
            var imageLinks = await _context.Rel_Image_Product
                .Where(i => i.ImageId == imageId)
                .ToListAsync();

            _context.Rel_Image_Product.RemoveRange(imageLinks);
        }
        #endregion

        #region Attribute 
        public async Task ReplaceAttributesAsync(int productId, IEnumerable<int> attributeFeatureIds)
        {
            var existingAttributeLinks = await _context.Rel_AttributesFetures_Product
                .Where(a => a.ProductId == productId)
                .ToListAsync();

            _context.Rel_AttributesFetures_Product.RemoveRange(existingAttributeLinks);

            var newAttributeLinks = attributeFeatureIds
                .Distinct()
                .Select((attributeFeatureId, index) => new Rel_AttributesFetures_Product
                {
                    ProductId = productId,
                    AttributeFeatureId = attributeFeatureId,
                    DisplayOrder = index
                });

            await _context.Rel_AttributesFetures_Product.AddRangeAsync(newAttributeLinks);
        }

        public async Task RemoveAllLinksForAttributeAsync(int attributeFeatureId)
        {
            var attributeLinks = await _context.Rel_AttributesFetures_Product
                .Where(a => a.AttributeFeatureId == attributeFeatureId)
                .ToListAsync();

            _context.Rel_AttributesFetures_Product.RemoveRange(attributeLinks);
        }
        #endregion
    }
}