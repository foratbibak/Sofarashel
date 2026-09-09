using Microsoft.EntityFrameworkCore;
using Sofarashel.Application.Mapper;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Enums.Products;
using Sofarashel.Domain.Models.Products;
using Sofarashel.Domain.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sofarashel.Application.Services.Implementation
{
    public class ProductServices(
       IProductRepository _productRepository,
       IGenericRepository<Product> _genericProductRepository,
       IAttributeFeatureServices _attributeFeatureServices,
       ICategoryRepository _categoryRepository) : IProductServices
    {
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _genericProductRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _productRepository.GetProductsByCategoryAsync(categoryId);
        }

        public async Task<Product?> GetSingleProductAsync(int? id)
        {
            return await _productRepository.GetProductWithDetailsAsync(id);
        }

        public async Task<AdminEditProductViewModel?> GetEditViewModelAsync(int? id)
        {
            var product = await _productRepository.GetByIdForAdminAsync(id);

            if (product == null)
            {
                return null;
            }

            var model = ProductMapper.MapToEditProductViewModel(product);
            model.Categories = await _categoryRepository.GetAllCategoriesAsync();

            return model;
        }

        public async Task<CreateProductResult> CreateProductAsync(AdminCreateProductViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                return CreateProductResult.Error;
            }

            if (model.CategoryIds == null || !model.CategoryIds.Any())
            {
                return CreateProductResult.CategoryNotFound;
            }

            foreach (var categoryId in model.CategoryIds)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    return CreateProductResult.CategoryNotFound;
                }
            }

            try
            {
                var addProduct = ProductMapper.MapToProduct(model);

                await _genericProductRepository.AddAsync(addProduct);
                await _genericProductRepository.SaveAsync();

                await _productRepository.SetCategoriesAsync(addProduct.Id, model.CategoryIds);

                var attributeIds = await ResolveAttributeIdsAsync(model.Attributes);
                await _productRepository.ReplaceAttributesAsync(addProduct.Id, attributeIds);

                var displayOrder = 0;
                foreach (var imageId in model.ImageIds.Distinct())
                {
                    var isMain = imageId == model.MainImageId;
                    await _productRepository.LinkImageAsync(addProduct.Id, imageId, isMain, displayOrder++);
                }

                await _genericProductRepository.SaveAsync();
            }
            catch (DbUpdateException)
            {
                return CreateProductResult.DatabaseError;
            }
            catch (Exception)
            {
                return CreateProductResult.UnknownError;
            }

            return CreateProductResult.Success;
        }

        public async Task<AdminEditProductResult> EditProductAsync(AdminEditProductViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                return AdminEditProductResult.Error;
            }

            if (model.CategoryIds == null || !model.CategoryIds.Any())
            {
                return AdminEditProductResult.CategoryNotFound;
            }

            foreach (var categoryId in model.CategoryIds)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    return AdminEditProductResult.CategoryNotFound;
                }
            }

            try
            {
                var editProduct = await _genericProductRepository.GetByIdAsync(model.Id);

                if (editProduct == null)
                {
                    return AdminEditProductResult.NotFound;
                }

                ProductMapper.MapToEditProduct(editProduct, model);
                _genericProductRepository.Update(editProduct);

                await _productRepository.SetCategoriesAsync(editProduct.Id, model.CategoryIds);

                var attributeIds = await ResolveAttributeIdsAsync(model.Attributes);
                await _productRepository.ReplaceAttributesAsync(editProduct.Id, attributeIds);

                var displayOrder = 0;
                foreach (var imageId in model.ImageIds.Distinct())
                {
                    var isMain = imageId == model.MainImageId;
                    await _productRepository.LinkImageAsync(editProduct.Id, imageId, isMain, displayOrder++);
                }

                await _genericProductRepository.SaveAsync();
            }
            catch (DbUpdateException)
            {
                return AdminEditProductResult.DatabaseError;
            }
            catch (Exception)
            {
                return AdminEditProductResult.UnknownError;
            }

            return AdminEditProductResult.Success;
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _genericProductRepository.GetByIdAsync(productId);

            if (product == null)
            {
                return;
            }

            product.IsDelete = true;
            product.DeleteDate = DateTime.Now;

            _genericProductRepository.Update(product);
            await _genericProductRepository.SaveAsync();
        }

        public async Task LinkImageAsync(int productId, int imageId, bool isMain)
        {
            var displayOrder = await _productRepository.GetNextImageDisplayOrderAsync(productId);

            await _productRepository.LinkImageAsync(productId, imageId, isMain, displayOrder);
            await _genericProductRepository.SaveAsync();
        }

        public async Task UnlinkImageAsync(int productId, int imageId)
        {
            await _productRepository.UnlinkImageAsync(productId, imageId);
            await _genericProductRepository.SaveAsync();
        }

        public async Task RemoveImageLinksAsync(int imageId)
        {
            await _productRepository.RemoveAllLinksForImageAsync(imageId);
            await _genericProductRepository.SaveAsync();
        }

        public async Task RemoveAttributeLinksAsync(int attributeFeatureId)
        {
            await _productRepository.RemoveAllLinksForAttributeAsync(attributeFeatureId);
            await _genericProductRepository.SaveAsync();
        }

        #region Helpers
        private async Task<List<int>> ResolveAttributeIdsAsync(List<ProductAttributeViewModel> attributes)
        {
            var attributeIds = new List<int>();

            foreach (var attribute in attributes)
            {
                var resolved = await _attributeFeatureServices.GetOrCreateAsync(attribute.Title, attribute.Value);
                attributeIds.Add(resolved.Id);
            }

            return attributeIds;
        }
        #endregion
    }
}