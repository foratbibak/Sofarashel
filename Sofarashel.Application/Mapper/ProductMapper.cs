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
                CreatDate = DateTime.Now,
                IsDelete = false,
            };
        }

        public static void MapToEditProduct(Product product, AdminEditProductViewModel model)
        {
            product.Title = model.Title;
            product.Description = model.Description;
            product.UpdateDate = DateTime.Now;
        }

        public static AdminEditProductViewModel MapToEditProductViewModel(Product product)
        {
            return new AdminEditProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                CategoryIds = product.ProductCategories?
                    .Select(pc => pc.CategoryId)
                    .ToList() ?? new(),
                Attributes = product.ProductAttributes?
                    .Select(pa => new ProductAttributeViewModel
                    {
                        Title = pa.AttributeFeature.AttributTitle,
                        Value = pa.AttributeFeature.AttributValue
                    })
                    .ToList() ?? new(),
                ExistingImages = product.ProductImages?
                    .OrderBy(pi => pi.DisplayOrder)
                    .Select(pi => new ProductImageViewModel
                    {
                        ImageId = pi.ImageId,
                        ImageUrl = pi.Image.ImageUrl,
                        IsMain = pi.IsMain
                    })
                    .ToList() ?? new(),
                MainImageId = product.ProductImages?
                    .FirstOrDefault(pi => pi.IsMain)?.ImageId,
                ImageIds = product.ProductImages?
                    .Select(pi => pi.ImageId)
                    .ToList() ?? new(),
            };
        }
    }
}