using Sofarashel.Domain.Enums.Products;
using Sofarashel.Domain.Models.Products;
using Sofarashel.Domain.ViewModels.Products;

namespace Sofarashel.Application.Services.Interfaces
{
    public interface IProductServices
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);

        Task<Product?> GetSingleProductAsync(int? id);

        Task<AdminEditProductViewModel?> GetEditViewModelAsync(int? id);

        Task<CreateProductResult> CreateProductAsync(AdminCreateProductViewModel product);

        Task<AdminEditProductResult> EditProductAsync(AdminEditProductViewModel product);

        Task DeleteProductAsync(int productId);

        Task UpdateMainImageAsync(int productId, string imageFileName);


        #region Gallery images
        Task<ProductImage?> GetImageByIdAsync(int imageId);

        Task AddImageAsync(int productId, string imageFileName);

        Task DeleteImageAsync(int imageId);

        #endregion
    }
}