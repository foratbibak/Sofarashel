using Sofarashel.Models.Common;

namespace Sofarashel.Domain.Models.Categories
{
    public class ProductDetail : BaseEntity
    {
        public int CategoryId { get; set; }

        public string? Material { get; set; }
        public string? FabricType { get; set; }
        public string? Color { get; set; }
        public string? Style { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }

        #region Realtions
        public Category? Category { get; set; }
        #endregion
    }
}