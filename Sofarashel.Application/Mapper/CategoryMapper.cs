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

        public static void MapToEditCategory(Category category, AdminEditCategoryViewModel model)
        {
            category.Title = model.Title;
            category.Description = model.Description;
            category.IsCategory = model.IsCategory;
            category.ParentId = model.ParentId;
            category.Material = model.Material;
            category.FabricType = model.FabricType;
            category.Color = model.Color;
            category.Style = model.Style;
            category.Length = model.Length;
            category.Width = model.Width;
            category.Height = model.Height;
            category.UpdateDate = DateTime.Now;
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
                Material = category.Material,
                FabricType = category.FabricType,
                Color = category.Color,
                Style = category.Style,
                Length = category.Length,
                Width = category.Width,
                Height = category.Height,
                Images = category.Images,
            };
        }
    }
}