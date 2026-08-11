using Sofarashel.Domain.Enums.Categories;
using Sofarashel.Domain.Models.Categories;
using Sofarashel.Domain.ViewModels.Categories;

namespace Sofarashel.Application.Services.Interfaces
{
    public interface ICategoryServices
    {
        #region Read
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<IEnumerable<Category>> GetRootCategoriesAsync();

        Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentId);

        Task<Category?> GetCategoryByIdForAdmin(int? id);

        Task DeleteImageAsync(int imageId);
        #endregion

        #region Write
        Task<CreateCategoryResult> CreateCategoryAsync(AdminCreateCategoryViewModel category);

        Task<AdminEditCategoryResult> EditCategoryAsync(AdminEditCategoryViewModel category);

        Task DeleteCategoryAsync(int categoryId);
        #endregion
    }
}