using Sofarashel.Domain.ViewModels.Common;

namespace Sofarashel.Domain.ViewModels.Products
{
    public class AdminProductFilterViewModel
    {
        public string? Title { get; set; }
        public int? CategoryId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public PagedResult<ProductListItemViewModel> Result { get; set; } = new();
    }
}