using Microsoft.EntityFrameworkCore;
using Sofarashel.Application.Mapper;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.Contracts;
using Sofarashel.Domain.Enums.Products;
using Sofarashel.Domain.Models.Products;
using Sofarashel.Domain.ViewModels.Products;
using System;
using System.Linq;

namespace Sofarashel.Application.Services.Implementation
{
    public class ProductServices(
        IProductRepository _productRepository,
        ICategoryRepository _categoryRepository) : IProductServices
    {
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
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
            model.CategoryOptions = await _categoryRepository.GetAllCategoriesAsync();

            return model;
        }

        public async Task<CreateProductResult> CreateProductAsync(AdminCreateProductViewModel product)
        {
            if (string.IsNullOrWhiteSpace(product.Title))
            {
                return CreateProductResult.Error;
            }

            if (product.CategoryIds == null || !product.CategoryIds.Any())
            {
                return CreateProductResult.CategoryNotFound;
            }

            foreach (var categoryId in product.CategoryIds)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    return CreateProductResult.CategoryNotFound;
                }
            }

            try
            {
                var addProduct = ProductMapper.MapToProduct(product);

                await _productRepository.CreateProductAsync(addProduct);
                await _productRepository.SaveAsync();

                await _productRepository.SetCategoriesAsync(addProduct.Id, product.CategoryIds);
                await _productRepository.SaveAsync();
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

        public async Task<AdminEditProductResult> EditProductAsync(AdminEditProductViewModel product)
        {
            if (string.IsNullOrWhiteSpace(product.Title))
            {
                return AdminEditProductResult.Error;
            }

            if (product.CategoryIds == null || !product.CategoryIds.Any())
            {
                return AdminEditProductResult.CategoryNotFound;
            }

            foreach (var categoryId in product.CategoryIds)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    return AdminEditProductResult.CategoryNotFound;
                }
            }

            try
            {
                var editProduct = await _productRepository.GetByIdAsync(product.Id);

                if (editProduct == null)
                {
                    return AdminEditProductResult.NotFound;
                }

                ProductMapper.MapToEditProduct(editProduct, product);
                await _productRepository.UpdateProductAsync(editProduct);

                await _productRepository.SetCategoriesAsync(editProduct.Id, product.CategoryIds);

                await _productRepository.SaveAsync();
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
            await _productRepository.DeleteAsync(productId);
            await _productRepository.SaveAsync();
        }

        public async Task UpdateMainImageAsync(int productId, string imageFileName)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
            {
                return;
            }

            product.MainImage = imageFileName;
            product.UpdateDate = DateTime.Now;

            await _productRepository.UpdateProductAsync(product);
            await _productRepository.SaveAsync();
        }

        public async Task<ProductImage?> GetImageByIdAsync(int imageId)
        {
            return await _productRepository.GetImageByIdAsync(imageId);
        }

        public async Task AddImageAsync(int productId, string imageFileName)
        {
            var image = ProductMapper.MapToImage(productId, imageFileName);
            await _productRepository.AddImageAsync(image);
            await _productRepository.SaveAsync();
        }

        public async Task DeleteImageAsync(int imageId)
        {
            var image = await _productRepository.GetImageByIdAsync(imageId);

            if (image != null)
            {
                await _productRepository.DeleteImageAsync(image);
                await _productRepository.SaveAsync();
            }
        }
    }
}