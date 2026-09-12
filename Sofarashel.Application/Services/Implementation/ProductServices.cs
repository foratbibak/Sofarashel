using Microsoft.EntityFrameworkCore;
using Sofarashel.Application.Mapper;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Enums.Products;
using Sofarashel.Domain.Models.Products;
using Sofarashel.Domain.ViewModels.Products;
using Sofarashel.Domain.ViewModels.Products.Sofarashel.Domain.ViewModels.Products;
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

        public async Task<AdminProductFilterViewModel> AdminFilterAsync(AdminProductFilterViewModel model)
        {
            #region Query
            var query = await _productRepository.FilterAsync();
            #endregion

            #region Filter
            if (!string.IsNullOrEmpty(model.Title))
            {
                query = query.Where(product => product.Title.Contains(model.Title));
            }

            if (model.CategoryId.HasValue)
            {
                query = query.Where(product => product.ProductCategories!
                    .Any(link => link.CategoryId == model.CategoryId));
            }
            #endregion

            #region Sort
            query = query.OrderByDescending(product => product.CreatDate);
            #endregion

            var projected = query.Select(product => new ProductListItemViewModel
            {
                Id = product.Id,
                Title = product.Title,
                MainImageUrl = product.ProductImages!
                    .Where(link => link.IsMain)
                    .Select(link => link.Image.ImageUrl)
                    .FirstOrDefault()
            });

            model.Result.PageNumber = model.PageNumber;
            model.Result.PageSize = model.PageSize;
            await model.Result.PagingAsync(projected);

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

            foreach (var attributeId in model.AttributeIds)
            {
                var attribute = await _attributeFeatureServices.GetByIdAsync(attributeId);
                if (attribute == null)
                {
                    return CreateProductResult.AttributeNotFound;
                }
            }

            try
            {
                var addProduct = ProductMapper.MapToProduct(model);

                await _genericProductRepository.AddAsync(addProduct);
                await _genericProductRepository.SaveAsync();

                await _productRepository.SetCategoriesAsync(addProduct.Id, model.CategoryIds);
                await _productRepository.ReplaceAttributesAsync(addProduct.Id, model.AttributeIds);

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

            foreach (var attributeId in model.AttributeIds)
            {
                var attribute = await _attributeFeatureServices.GetByIdAsync(attributeId);
                if (attribute == null)
                {
                    return AdminEditProductResult.AttributeNotFound;
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
                await _productRepository.ReplaceAttributesAsync(editProduct.Id, model.AttributeIds);

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
    }
}