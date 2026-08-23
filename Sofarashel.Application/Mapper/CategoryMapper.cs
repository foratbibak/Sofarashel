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

        public static AdminEditCategoryViewModel MapToEditCategoryViewModel(Category category)
        {
            return new AdminEditCategoryViewModel
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description,
                IsCategory = category.IsCategory,
                ParentId = category.ParentId,
                MainImage = category.MainImage,
            };
        }
    }
}