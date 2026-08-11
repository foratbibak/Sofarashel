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

        Task<IEnumerable<Category>> GetProductsByParentAsync(int parentId);

        Task<Category?> GetSingleProductAsync(int? id);

        Task<IEnumerable<Category>> GetParentCategoryOptionsAsync(int? currentId);

        Task<AdminEditCategoryViewModel?> GetEditViewModelAsync(int? id);

        Task<CategoryImage?> GetImageByIdAsync(int imageId);
        #endregion

        #region Images
        Task AddImageAsync(int categoryId, string imageFileName);

        Task DeleteImageAsync(int imageId);
        #endregion

        #region Write
        Task<CreateCategoryResult> CreateCategoryAsync(AdminCreateCategoryViewModel category);

        Task<AdminEditCategoryResult> EditCategoryAsync(AdminEditCategoryViewModel category);

        Task DeleteCategoryAsync(int categoryId);
        #endregion
    }
}