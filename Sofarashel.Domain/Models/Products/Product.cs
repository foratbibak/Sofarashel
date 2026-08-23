using Sofarashel.Domain.Models.Relations;
using Sofarashel.Models.Common;
using System.Collections.Generic;

namespace Sofarashel.Domain.Models.Products
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? MainImage { get; set; }

        public string? Material { get; set; }
        public string? FabricType { get; set; }
        public string? Color { get; set; }
        public string? Style { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }

        #region Relations
        public ICollection<ProductImage>? Images { get; set; }

        public ICollection<Rel_Product_Category>? ProductCategories { get; set; }
        #endregion
    }
}