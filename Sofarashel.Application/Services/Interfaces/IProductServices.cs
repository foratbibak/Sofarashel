using Sofarashel.Domain.Enums.Products;
using Sofarashel.Domain.Models.Products;
using Sofarashel.Domain.ViewModels.Products;

namespace Sofarashel.Application.Services.Interfaces
{
    public interface IProductServices
    {

        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);

        Task<Product?> GetSingleProductAsync(int? id);

        Task<AdminEditProductViewModel?> GetEditViewModelAsync(int? id);

        Task<AdminProductFilterViewModel> AdminFilterAsync(AdminProductFilterViewModel model);

        Task<CreateProductResult> CreateProductAsync(AdminCreateProductViewModel product);

        Task<AdminEditProductResult> EditProductAsync(AdminEditProductViewModel product);

        Task DeleteProductAsync(int productId);

        Task LinkImageAsync(int productId, int imageId, bool isMain);

        Task UnlinkImageAsync(int productId, int imageId);

        Task RemoveImageLinksAsync(int imageId);


        Task RemoveAttributeLinksAsync(int attributeFeatureId);
    }
}