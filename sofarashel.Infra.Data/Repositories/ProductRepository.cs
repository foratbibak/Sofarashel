using Microsoft.EntityFrameworkCore;
using Sofarashel.Data;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Models.Products;
using Sofarashel.Domain.Models.Relations;
using System;
using System.Collections.Generic;

namespace Sofarashel.Infra.Data.Repositories
{
    public class ProductRepository(GallaryDbcontext _context) : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            var items = await _context.Products
                .Where(p => p.ProductCategories!.Any(pc => pc.CategoryId == categoryId))
                .Include(p => p.Images)
                .ToListAsync();

            foreach (var item in items)
            {
                if (item.Images != null)
                {
                    foreach (var image in item.Images)
                    {
                        image.Product = null;
                    }
                }
            }

            return items;
        }

        public async Task<Product?> GetByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<Product?> GetByIdForAdminAsync(int? productId)
        {
            var product = await _context.Products
                .Include(p => p.Images)
                .Include(p => p.ProductCategories!)
                    .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product?.Images != null)
            {
                foreach (var image in product.Images)
                {
                    image.Product = null;
                }
            }

            if (product?.ProductCategories != null)
            {
                foreach (var link in product.ProductCategories)
                {
                    link.Product = null;
                }
            }

            return product;
        }

        public async Task<Product?> GetProductWithDetailsAsync(int? productId)
        {
            var product = await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product?.Images != null)
            {
                foreach (var image in product.Images)
                {
                    image.Product = null;
                }
            }

            return product;
        }

        public async Task CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task DeleteAsync(Product product)
        {
            product.IsDelete = true;
            product.DeleteDate = DateTime.Now;
            _context.Products.Update(product);
        }

        public async Task DeleteAsync(int productId)
        {
            var product = await GetByIdAsync(productId);
            await DeleteAsync(product);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #region Images
        public async Task AddImageAsync(ProductImage image)
        {
            await _context.ProductImages.AddAsync(image);
        }

        public async Task<ProductImage?> GetImageByIdAsync(int imageId)
        {
            return await _context.ProductImages.FindAsync(imageId);
        }

        public async Task DeleteImageAsync(ProductImage image)
        {
            image.IsDelete = true;
            image.DeleteDate = DateTime.Now;
            _context.ProductImages.Update(image);
        }
        #endregion

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