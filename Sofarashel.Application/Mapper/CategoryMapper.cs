using Sofarashel.Domain.Models.Categories;
using Sofarashel.Domain.ViewModels.Categories;
using System;

namespace Sofarashel.Application.Mapper
{
    public static class CategoryMapper
    {
        public static Category MapToCategory(AdminCreateCategoryViewModel model)
        {
            return new Category
            {
                Title = model.Title,
                Description = model.Description,
                IsCategory = model.IsCategory,
                ParentId = model.ParentId,
                CreatDate = DateTime.Now,
                IsDelete = false,
            };
        }

        public static void MapToEditCategory(Category category, AdminEditCategoryViewModel model)
        {
            category.Title = model.Title;
            category.Description = model.Description;
            category.IsCategory = model.IsCategory;
            category.ParentId = model.ParentId;
            category.UpdateDate = DateTime.Now;
        }

        public static ProductDetail MapToProductDetail(AdminCreateCategoryViewModel model, int categoryId)
        {
            return new ProductDetail
            {
                CategoryId = categoryId,
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

        public static void MapToEditProductDetail(ProductDetail productDetail, AdminEditCategoryViewModel model)
        {
            productDetail.Material = model.Material;
            productDetail.FabricType = model.FabricType;
            productDetail.Color = model.Color;
            productDetail.Style = model.Style;
            productDetail.Length = model.Length;
            productDetail.Width = model.Width;
            productDetail.Height = model.Height;
            productDetail.UpdateDate = DateTime.Now;
        }

        public static ProductDetail MapToProductDetailFromEdit(AdminEditCategoryViewModel model, int categoryId)
        {
            return new ProductDetail
            {
                CategoryId = categoryId,
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

        public static AdminEditCategoryViewModel MapToEditCategoryViewModel(Category category)
        {
            return new AdminEditCategoryViewModel
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description,
                IsCategory = category.IsCategory,
                ParentId = category.ParentId,
                Material = category.ProductDetail?.Material,
                FabricType = category.ProductDetail?.FabricType,
                Color = category.ProductDetail?.Color,
                Style = category.ProductDetail?.Style,
                Length = category.ProductDetail?.Length,
                Width = category.ProductDetail?.Width,
                Height = category.ProductDetail?.Height,
                Images = category.Images,
            };
        }
    }
}