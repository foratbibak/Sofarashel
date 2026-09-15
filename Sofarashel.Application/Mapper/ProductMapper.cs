using Sofarashel.Domain.Constants;
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
            var model = new AdminEditProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                CategoryIds = product.ProductCategories?
                    .Select(c => c.CategoryId)
                    .ToList() ?? new(),
                AttributeIds = product.ProductAttributes?
                    .Select(a => a.AttributeFeatureId)
                    .ToList() ?? new(),
                Images = product.ProductImages?
                    .OrderBy(i => i.DisplayOrder)
                    .Select(i => new ProductImageViewModel
                    {
                        ImageId = i.ImageId,
                        ImageUrl = i.Image.ImageUrl,
                        IsMain = i.IsMain
                    })
                    .ToList() ?? new(),
                MainImageId = product.ProductImages?
                    .FirstOrDefault(i => i.IsMain)?.ImageId,
                ImageIds = product.ProductImages?
                    .Select(i => i.ImageId)
                    .ToList() ?? new(),
            };

            if (!model.Images.Any())
            {
                model.Images.Add(new ProductImageViewModel
                {
                    ImageId = 0,
                    ImageUrl = ImageDefaults.NoPhotoFileName,
                    IsMain = true
                });
            }

            return model;
        }
    }
}