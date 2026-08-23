using Sofarashel.Domain.Models.Products;
using Sofarashel.Domain.ViewModels.Products;
using System;
using System.Linq;

namespace Sofarashel.Application.Mapper
{
    public static class ProductMapper
    {
        public static Product MapToProduct(AdminCreateProductViewModel model)
        {
            return new Product
            {
                Title = model.Title,
                Description = model.Description,
                Material = model.Material,
                FabricType = model.FabricType,
                Color = model.Color,
                Style = model.Style,
                Length = model.Length,
                Width = model.Width,
                Height = model.Height,
                CreatDate = DateTime.Now,
                IsDelete = false,
            };
        }

        public static void MapToEditProduct(Product product, AdminEditProductViewModel model)
        {
            product.Title = model.Title;
            product.Description = model.Description;
            product.Material = model.Material;
            product.FabricType = model.FabricType;
            product.Color = model.Color;
            product.Style = model.Style;
            product.Length = model.Length;
            product.Width = model.Width;
            product.Height = model.Height;
            product.UpdateDate = DateTime.Now;
        }

        public static AdminEditProductViewModel MapToEditProductViewModel(Product product)
        {
            return new AdminEditProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Material = product.Material,
                FabricType = product.FabricType,
                Color = product.Color,
                Style = product.Style,
                Length = product.Length,
                Width = product.Width,
                Height = product.Height,
                MainImage = product.MainImage,
                Images = product.Images,
                CategoryIds = product.ProductCategories?
                    .Select(pc => pc.CategoryId)
                    .ToList() ?? new(),
            };
        }

        public static ProductImage MapToImage(int productId, string fileName)
        {
            return new ProductImage
            {
                ProductId = productId,
                ImageUrl = fileName,
                IsMain = false,
                CreatDate = DateTime.Now,
                IsDelete = false
            };
        }
    }
}